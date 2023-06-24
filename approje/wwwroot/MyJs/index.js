var div = document.querySelector("#OnlineUsersDiv");
var FollowButoon = document.querySelector(".follow-button");
//GetAllOnlineUsersFunction();
setTimeout(function () {

    GetOnlineUsersFunction();

}, 1500)
function GetOnlineUsersFunction() {
   
    $.ajax({
        url: "/Home/GetAllOnlineUsers",
        method: "GET",
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                //alert(data[i].userName)
                if ($(`#OnlineUsersDiv #${data[i].id}`).length <= 0) {
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

        }
    });
}



function SendRequest(id) {
    $.ajax({
        url: "/Home/SendFollow/" + id,
        method: "GET",
        success: function (data) {
            var jsonData = JSON.stringify(data);
            $('.follow-button').removeClass('btn-primary').addClass('btn-secondary');
           
            FollowButoon.textContent = "CANCEL";
            FollowButoon.style.fontSize = "1em";
            FollowButoon.onclick = function () {
                deleteRequest(FollowButoon.id);
            }

        },
        error: function (err) {
            console.log(err)
        }
    })
    
}


function deleteRequest(id) {
   // alert(id);
    $.ajax({
        url: "/Home/CancelFollow/" + id,
        method: "GET",
        success: function (data) {
            var jsonData = JSON.stringify(data);
            if (jsonData==`"Done"`) {
                $('.follow-button').removeClass('btn-secondary').addClass('btn-primary'); 
                FollowButoon.textContent = "+ Follow";
                FollowButoon.style.fontSize = "35px";
                FollowButoon.onclick = function () {
                    SendRequest(FollowButoon.id);
                }
            }
            

        },
        error: function (err) {
            console.log(err)
        }
    })
}