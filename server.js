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
this.handOfCard = [];
this.point = 0;
}

app.post('/login', function(request, response){
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
	response.send("start");
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

function checkForWinner(){

}
