"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start().then(function () {
    console.log("Connected");
    //GetAllOnlineUsers();
    //alert("Conection");
   GetAllOnlineUsersFunction();
}).catch(function (err) {
    console.log(err.toString())
})

connection.on("Connect", function (info,id) {
    var div = document.querySelector("#OnlineUsersDiv");
    var data = `<article class="item animate__animated animate__slideInRight" id ="${id}" >
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

   div.innerHTML += data
  ////  alert(div.innerHTML)
  //  setTimeout(function () {
  //      $("#OnlineUsersDiv").find(`article#${id}`).removeClass("animate__animated");
  //  }, 600);
});

connection.on("Disconnect", function (info) {
   
 
    $(`#OnlineUsersDiv > article#${info}`).remove();
});