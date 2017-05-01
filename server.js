var express = require("express");
var app = express();
var server = require("http").createServer(app);
var bodyParser = require('body-parser');

//parse application/x-www-form-urlencoded 
app.use(bodyParser.urlencoded({ extended: false }));
  
// parse application/json 
app.use(bodyParser.json());

//node js server setup

server.listen(process.env.PORT || 8080);
console.log("Server is running on localhost:8080\n*\n*\n*");

app.post('/test', function(request, response){
	console.log(request.body.user);
	response.end("server say " + request.body.user);
});
