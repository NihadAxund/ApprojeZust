var FollowButoon = document.querySelector(".follow-button");
function SendRequest(id) {
 
    $.ajax({
        url: "/Home/SendFollow/" + id,
        method: "GET",
        success: function (data) {
   
            var jsonData = JSON.stringify(data);
            $('.follow-button').removeClass('btn-primary').addClass('btn-secondary');

            SendNotificationFunction(id,1);
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
    $.ajax({
        url: "/Home/CancelFollow/" + id,
        method: "GET",
        success: function (data) {
            var jsonData = JSON.stringify(data);
            if (jsonData == `"Done"`) {
                $('.follow-button').removeClass('btn-secondary').addClass('btn-primary');
                FollowButoon.textContent = "+";
                FollowButoon.style.padding = "1px 10px 1px 10px";
                FollowButoon.style.fontSize = "35px";
                SendNotificationFunction(id, 2);
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

async function AddFriendList(id) {
    $.ajax({
        url:""
    })
}
