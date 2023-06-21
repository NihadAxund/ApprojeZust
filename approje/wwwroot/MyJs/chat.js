"use strict"

//alert("aa");
var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start().then(function () {
    //alert("Conection");
    
}).catch(function (err) {
    console.log(err.toString())
    return console.error(err.toString());
})

connection.on("Connect", function (info) {
    var div = document.querySelector("#OnlineUsersDiv");
    var data = `<article class="item">
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
    alert(div.innerHTML)
});