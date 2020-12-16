Sample Dockerize Asp.net Core 3.1 Project.

First Create Network to see 2 Container(means redis and my project) on Docker:

<code>sudo docker create network YourNetworkName</code>

then run redis along with join to created network and persist data:

* before run below command.you should create redis folder in /tmp/data (path and folder name is optional.you can change their name as well as create folders path).

<code>sudo docker run --rm --name redis -p6380:6379 -v /tmp/data/redis:/data --network YourNetworkName redis redis-server --appendonly yes </code>

then Create Image by dockerFile:

<code>sudo docker build -t yourprojectname:1.0</code>
* your project name should be lowercase.

then after create image we should run container from created image and join to network:

<code>sudo docker run --rm --name YourContainerName -p80:80 --network YourNetworkName yourprojectname:1.0</code>

finally call below url on browser:

<b>localhost</b>
