var express = require("express");
var app = express();
var server = require("http").createServer(app);

//node js server setup

server.listen(process.env.PORT || 8080);
console.log("Server is running on localhost:8080\n*\n*\n*");

app.post('/test', function(request, response){
	console.log("testing!");
	response.end("server say it's working");
});
