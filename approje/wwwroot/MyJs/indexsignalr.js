connection.on("Connect", function (info, id) {

    setTimeout(GetOnlineUsersFunction2(), 1000)
    //GetOnlineUsersFunction2();
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
