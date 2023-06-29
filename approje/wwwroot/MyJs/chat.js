"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
connection.start().then(function () {

  //  setTimeout(function () { GetOnlineUsersFunction2(); }, 1500);
}).catch(function (err) {
    alert(err);
    console.log(err.toString())
})

connection.on("Notification", function (info) { 

    alert(info)
    AddFriendRequstList();
});

async function SendNotificationFunction(id) {

    alert("zz");
    connection.invoke("SendNotification", id,1);
    alert("Nihad Dusdu buraya ");
}
//connection.on("Notificatio", function (Username) {
//    alert(userName);
//})

