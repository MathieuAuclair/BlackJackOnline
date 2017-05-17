var express = require("express");
var app = express();
var server = require("http").createServer(app);
var bodyParser = require('body-parser');

/* TODO
	* Add a socket.io (!!!!)
	* Refactoring (!!)
*/


//parse application/x-www-form-urlencoded 
app.use(bodyParser.urlencoded({ extended: false }));
  
// parse application/json 
app.use(bodyParser.json());

//node js server setup

server.listen(process.env.PORT || 8080);
console.log("Server is running on localhost:8080\n*\n*\n*");


var inGameMember = [];
var onlineUser = []; //list of all sessionId
var currentTurnPlayer;
var searching = true;

function member(newName){
this.name = newName;
this.point = 0;
}

app.post('/login', function(request, response){
	console.log(JSON.stringify(onlineUser));
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
		if(searching){
		console.log("start a new game!");
		response.send("start");
		}
		else{
		response.send("false");
		}
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
		response.send("true");//should send back other player object!
	}
	else{
		response.send("false");
	}
});

app.post('/play', function(request, response){
	changePlayerTurn(currentTurnPlayer);
	
});

function changePlayerTurn(idSession){
	for(i=0;i<onlineUser.length;i++){
		if(onlineUser[i].name != idSession){
			currentTurnPlayer = onlineUser[i].name;
		}
	}
}




