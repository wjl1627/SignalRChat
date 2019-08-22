"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//1、与服务器建议连接
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    //在建立连接的后，请求后端告诉自己的ID是什么
    connection.invoke("GetCurrentUserId").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});
//后端服务器返回上面GetCurrentUserId请求的结果过来
connection.on("SenUserId", function (userId) {
    //给发消息的录入框赋值
    document.getElementById("userInput").value = userId;
});

//发送消息事件
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var reUser = document.getElementById("reUserInput").value;
    var message = document.getElementById("messageInput").value;
    //请求服务器发消息
    connection.invoke("SendMessage", user, reUser, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

//服务器返回消息
connection.on("ReceiveMessage", function (user, reUser, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " to " + reUser + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});