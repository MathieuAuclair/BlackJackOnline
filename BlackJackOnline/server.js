var express = require("express");
var bodyParser = require("body-parser");

var app = express();

app.use(bodyParser.urlencoded({ extended: false}));
app.use(bodyParser.json());

app.get("/", function(request, response){ 
    response.send("Hello!!");
});

app.post("/build", function(request, response){ 

    response.send("This is the post method");
});

app.listen(8080, "localhost");

//render msg in server console
console.log("server is running on port 8080...");
