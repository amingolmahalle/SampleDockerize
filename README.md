Sample Dockerize Asp.net Core 3.1 Project.

First Create Network to watch 2 Container(means redis and project) on Docker:

<code>sudo docker create network NetworkName</code>

then run redis along with join to created network and persist data:

* before run below command.you need to create redis folder in <b>/tmp/data/redis</b> (path and folder name is optional.you can change their name as well as create folders path).

<code>sudo docker run --rm --name redis -p6380:6379 -v /tmp/data/redis:/data --network NetworkName redis redis-server --appendonly yes </code>

then Create Image by dockerFile:

<code>sudo docker build -t projectname:1.0</code>
* your project name have to be lowercase.

then after create image we have to run container from created image and join to network:

<code>sudo docker run --rm --name ContainerName -p80:80 --network NetworkName projectname:1.0</code>

finally call below url on browser:

<b>localhost:80</b>
