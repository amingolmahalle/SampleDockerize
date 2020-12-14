using System;
using System.Text;

namespace Helper.Random
{
    public static class RandomHelper
    {
        public static string GenerateRandom(int length = 7)
        {
            var generateNumber = new StringBuilder();
            var random = new System.Random(DateTime.Now.Millisecond);

            for (var i = 0; i < length; i++)
            {
                generateNumber.Append(random.Next(1, 10));
            }
            return generateNumber.ToString();

        }
    }
}