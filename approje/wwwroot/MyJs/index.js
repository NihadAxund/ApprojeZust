
function GetOnlineUsersFunction2() {

    var div = document.querySelector("#OnlineUsersDiv");
    $.ajax({
        url: "/Home/GetAllOnlineUsers",
        method: "GET",
        success: function (data) {
            div.innerHTML = " ";
            for (var i = 0; i < data.length; i++) {
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



function Logout() {
    alert("Logout");
    $.ajax({
        url: "/Home//" + id,
        method: "GET",
        success: function (data) {
            var jsonData = JSON.stringify(data);
            $('.follow-button').removeClass('btn-primary').addClass('btn-secondary');
            alert("Bu 3cudu logoutdaki")
/*            SendNotification(id);*/
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

setTimeout(function () {GetOnlineUsersFunction2(); }, 1500);