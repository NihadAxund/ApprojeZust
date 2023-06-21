"use strict"

//alert("aa");
var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start().then(function () {
    //alert("Conection");
    console.log("Connected");
}).catch(function (err) {
    console.log(err.toString())
    return console.error(err.toString());
})

connection.on("Connect", function (info,id) {
    var div = document.querySelector("#OnlineUsersDiv");
    var data = `<article class="item" id ="${id}" >
                                <a href="#" class="thumb">
                                    <span class="fullimage bg1" role="img">A</span>
                                </a>

                                <div class="info">
                                    <h4 class="title">
                                        <a href="#">${info}</a>
                                    </h4>
                                    <span>Today</span>
                                </div>
                            </article>`;

    div.innerHTML += data
  //  alert(div.innerHTML)
});

connection.on("Disconnect", function (info) {
   
  //  alert(info)
    $(`#OnlineUsersDiv > article#${info}`).remove();
    //$(document).ready(function () {
    //    $("#OnlineUsersDiv").find("#d).remove();
    //});
   // alert("So");
});