/*
 	* this is like the using space in csharp
 	* but for javascript server
*/
var express = require("express");
var app = express();
var server = require("http").createServer(app);
var bodyParser = require('body-parser');


/* TODO
	* Add a socket.io (!!!!)
	* Refactoring (!!)
*/

/*
 	* saying to express API that we want to use body parser
	* body-parser is an extension that convert automatically object to 
	* JSON string during http request
*/

//parse application/x-www-form-urlencoded 
app.use(bodyParser.urlencoded({ extended: false }));
  
// parse application/json 
app.use(bodyParser.json());

//node js server setup

//starting the server on the port 8080
server.listen(process.env.PORT || 8080);
//notify the console that the server is ready
console.log("Server is running on localhost:8080\n*\n*\n*");


var onlineUser = []; // list of all client that have logged into this server
var currentTurnPlayer;
var searching = true;

function member(newName){ 
	/* 
	 	* this object is a holder for new connection to the server, 
		* each client recive a name, a list for thier card and a score
		* so with thier names we can separate them and assign the
	*/
this.name = newName;
this.handOfCard = [];
this.point = 0;
}

/*
	* app.post is a function from express that take the http request from the client,
	* treat them, then anwser back with response.end or response.send just like return
	* in a c# function, it's really important to end the connection or else the server will
	* crash after a few connection only
*/
app.post('/login', function(request, response){ //catch login client request here the treat them with the function
	if(findIdSession(request.body.data) && onlineUser.length < 2){//still waiting for other player
		response.send("true");
	}
	else if(onlineUser.length < 2){// still some room in the game! create a userSession
		var newUserSessionId = "user" + onlineUser.length;
		console.log("new session created! : " + newUserSessionId);
		onlineUser.push(new member(newUserSessionId));
		currentTurnPlayer = onlineUser[0].name;
		response.send(newUserSessionId);

	}
	else if(findIdSession(request.body.data) && onlineUser.length >= 2){ //can't create new session id if game is full
	console.log("start a new game!");
	response.send("start"); // send the string start to the client so he know that the game can start!
	}
	else{//something is wrong... ?
		response.send("error");
	}
});

function findIdSession(idSession){ //scan onlineUser with a certain name
	for(i=0;i<onlineUser.length;i++){
		if(onlineUser[i].name === idSession){
			return true;	
		}
	}
	return false;
}

app.post('/turn', function(request, response){
	if(request.body.data === currentTurnPlayer){
		response.send("");//need to send back the opponent card
	}
	else{
		response.send("false");
	}
});

app.post('/play', function(request, response){
	console.log("user draw : " + request.body.data);
	var index = changePlayerTurn(currentTurnPlayer);
	onlineUser[index].handOfCard.push(parseInt(request.body.data));
	onlineUser[index].point += getplayerScore(parseInt(request.body.data));
	console.log("user score is: " + onlineUser[index].point);
	response.send(onlineUser[index].point.toString());
});

function changePlayerTurn(idSession){
	for(i=0;i<onlineUser.length;i++){
		if(onlineUser[i].name != idSession){
			currentTurnPlayer = onlineUser[i].name;
			return i;
		}
	}
}

function getplayerScore(card){
	//this will convert card id into point
	if(card % 13 == 0){
	return 11;
	}
	else if(card % 13 >= 10){
	return 10;
	}
	else{
	return ((card % 13)+1);
	}
}


function checkForWinner(){// not ready yet!

}
