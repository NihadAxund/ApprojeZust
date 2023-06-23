
var div = document.querySelector("#OnlineUsersDiv");
function GetAllOnlineUsersFunction() {;
    $.ajax({
        url: "/Home/GetAllOnlineUsers",
        method: "GET",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
               // alert(data[i].userName)
                var dat2a = `<article class="item" id ="${data[i].id}" >
                                <a href="#" class="thumb">
                                    <span class="fullimage bg1" role="img"></span>
                                </a>

                                <div class="info">
                                    <h4 class="title">
                                        <a href="/home/userProfile/${data[i].id}">${data[i].userName}</a>
                                    </h4>
                                    <span>Today</span>
                                </div>
                            </article>`;

                div.innerHTML += dat2a
  
            }

        }
    });
}
