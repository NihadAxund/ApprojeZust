"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start().then(function () {
    console.log("Connected");
    //GetAllOnlineUsers();
    //alert("Conection");
    setTimeout(function () {
        GetAllOnlineUsersFunction();
    }, 1000);
}).catch(function (err) {
    console.log(err.toString())
})

connection.on("Connect", function (info,id) {
    var div = document.querySelector("#OnlineUsersDiv");
    var data = `<article class="item" id ="${id}" >
                                <a href="#" class="thumb">
                                    <span class="fullimage bg1" role="img">A</span>
                                </a>

                                <div class="info">
                                    <h4 class="title">
                                        <a href="/home/userProfile/${id}">${info}</a>
                                    </h4>
                                    <span>Today</span>
                                </div>
                            </article>`;

    div.innerHTML += data;
});

connection.on("Disconnect", function (info) {
   
 
    $(`#OnlineUsersDiv > article#${info}`).remove();
});