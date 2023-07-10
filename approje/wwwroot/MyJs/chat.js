"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
connection.start().then(function () {

  //  setTimeout(function () { GetOnlineUsersFunction2(); }, 1500);
}).catch(function (err) {
    console.log(err.toString())
})

connection.on("Notification", function (info,Notfication) { 

    if (Notfication == 1 || Notfication == 2) 
        ControlFriendRequstList();
    
});



async function SendNotificationFunction(id,Notfication) {

    connection.invoke("SendNotification", id, Notfication);
}



function SendChatMessageFunction(id,sendid, Messagetxt) {
    try {
        connection.invoke("SendMessageUser", id,sendid, Messagetxt);

    } catch (error) {
        console.error("SendChatMessage error:", error);
    }
}


connection.on("ExistingError",function(){
    window.location.href = "/home/errorview";
    alert("Same user cannot be used again")


});


