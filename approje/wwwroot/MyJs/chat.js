"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
connection.start().then(function () {

    setTimeout(function () { GetOnlineUsersFunction2(); }, 1500);
}).catch(function (err) {
    alert(err);
    console.log(err.toString())
})

connection.on("Connect", function (info, id) {
    GetOnlineUsersFunction2();
    //var div = document.querySelector("#OnlineUsersDiv");
    //var data = " ";
    //data = `<article class="item" id ="${id}" >
    //                                <a href="#" class="thumb">
    //                                    <span class="fullimage bg1" role="img">A</span>
    //                                </a>

    //                                <div class="info">
    //                                    <h4 class="title">
    //                                        <a href="/home/userProfile/${id}">${info}</a>
    //                                    </h4>
    //                                    <span>Today</span>
    //                                </div>
    //                            </article>`;
    //div.innerHTML += data;
   

});

connection.on("Disconnect", function (id) {
    GetOnlineUsersFunction2();
});

connection.on("Notification", function (info) {

    alert("|");
});

async function SendNotification(id) {
    alert(Z);
    alert("Nihad Dusdu buraya ");
    await connection.invoke("SendNotification", id);
}
//connection.on("Notificatio", function (Username) {
//    alert(userName);
//})

