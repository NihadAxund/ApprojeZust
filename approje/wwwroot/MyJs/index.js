
var div = document.querySelector("#OnlineUsersDiv");
function GetAllOnlineUsersFunction() {
    
    $.ajax({
        url: "/Home/GetAllOnlineUsers",
        method: "GET",
        success: function (data) {
          
            for (var i = 0; i < data.length; i++) {
                // alert(data[i].userName)
                var text = `<article class="item" id="${data[i].id}" >
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
                if (text != null && div != null) {
                    div.innerHTML += text
                }

            }

        }
    });
}


function SendRequest(id) {
    alert(id);
    $.ajax({
        url: "/Home/SendFollow/" + id,
        method: "GET",
        success: function (data) {
            var jsonData = JSON.stringify(data);
            alert(jsonData)
        },
        error: function (err) {
            console.log(err)
        }

    })
}
