using System.Threading;
using System.Threading.Tasks;
using Helper.Random;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Web.Extensions;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        private readonly IDistributedCache _distributedCache;

        public HomeController(
            ILogger<HomeController> logger,
             IConfiguration configuration,
             IDistributedCache distributedCache)
        {
            _logger = logger;
            _configuration = configuration;
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
        FetchData:
            var result = await _distributedCache.GetCacheValueAsync<string>("code");

            if (string.IsNullOrEmpty(result))
            {
                var generatedCode = RandomHelper.GenerateRandom(_configuration.GetValue<int>("Length"));

                await _distributedCache.SetCacheValueAsync("code", generatedCode, cancellationToken);

                goto FetchData;
            }

            ViewBag.Result = $"Your Code is: {result}";
            _logger.LogInformation($"your code is: {result}");

            return View();
        }
    }
}
