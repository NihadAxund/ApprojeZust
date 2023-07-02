var FriendRequestList = document.querySelector(".friend-requests-body");
var FrendRequestCountDiv = document.querySelector(".friend-requests-btn");



async function ControlFriendRequstList() {
    $.ajax({
        url: "/Home/GetAllMeFriendRequest",
        method: "GET",
        success: function (data) {
            FriendRequestList.innerHTML = " ";
            var text = " ";
          
            var count = $(`.friend-requests-btn #UserFriendsCount`).length;
            var spanelement = document.querySelector("#UserFriendsCount")
            if (data.length > 0) {

                if (count > 0) {
                    spanelement.innerHTML = data.length;
                }
                else {
                    
                    var text = `<span id="UserFriendsCount" >${data.length}</span>`;
                    FrendRequestCountDiv.innerHTML += text;
                }
            }
            else if (count>0) {
                spanelement = document.querySelector("#UserFriendsCount")
                spanelement.remove();
             
            }
            for (var i = 0; i < data.length; i++) {
                if (i >= 10) {
                    text += `
                      <div class="view-all-requests-btn">
                         <a href="/home/friends" class="default-btn">View All Requests</a>
                      </div>
                    `
                    break;
                }
                else {
                    var item = data[i];
                    alert(item.ownCustomIdentityUser.id+"Nihad bud");
                    text +=`
                    <div class="item d-flex align-items-center">
                        <div class="figure">
                            <a href="#"><img 
                            src="https://w7.pngwing.com/pngs/744/940/png-transparent-anonym-avatar-default-head-person-unknown-user-user-pictures-icon.png" 
                            class="rounded-circle" alt="image"></a>
                        </div>

                        <div class="content d-flex justify-content-between align-items-center">
                            <div class="text">
                                <h4><a href="#">${item.meName}</a></h4>

                            </div>
                            <div class="btn-box d-flex align-items-center">
                                <button id="${item.ownid}" class="delete-btn d-inline-block me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" type="button"><i class="ri-close-line"></i></button>

                                <button id="${item.ownid}" onclick="AddFriend(id)" class="confirm-btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Confirm" type="button"><i class="ri-check-line"></i></button>
                            </div>
                        </div>
                    </div>`
                }
            }
            FriendRequestList.innerHTML = text;
        }
    });
}

function AddFriend(id) {
    alert(id);
    $.ajax({
        url: "/Home/AddFriends/"+id,
        method: "GET",
        success: function (data) {
            alert('Hda')
            ControlFriendRequstList();
        }

    });
}
