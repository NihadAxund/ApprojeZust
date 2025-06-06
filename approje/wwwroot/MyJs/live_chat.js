﻿var div2 = document.querySelector("#live_chat_online_users");
var chatbody = document.querySelector(".live-chat-body");
connection.on("Connect", function (info, id) {

    
    GetAllLiveChatUsers();

});

connection.on("Disconnect", function (id) {
    GetAllLiveChatUsers();
});

connection.on("SendChatUser", function (id, sendid, mes) {

    LiveMessageUser(sendid);
});



 function chatSendMessage(receiverId, senderId) {

    let content_txt = document.querySelector('#content_txt_chat');
    let obj = {
        receiverId: receiverId,
        senderId: senderId,
        content: content_txt.value
    };

    $.ajax({
        url: `/Home/AddMessage`,
        method: "POST",
        data: obj,
        success: function (data) {
            SendChatMessageFunction(receiverId, senderId, content_txt.value);
            content_txt.value = "";
            LiveMessageUser(receiverId);

        },
        error: function (err) {
            alert(err);
            console.log(err);
        }
    })
}
async function LiveMessageUser(id) {
    $.ajax({
        url: `/Home/MyChat/${id}`,
      
        method: "GET",
        success: function (chatdata) {
            var me = chatdata.user;
            var Ownuser = chatdata.ownuser;
            chatbody.innerHTML = " ";
            //alert(me.userName + "|" + Ownuser.userName)
            var text = ` <div class="live-chat-header d-flex justify-content-between align-items-center">
                     <div class="live-chat-info">
                         <a href="#"><img style="height:50px; width:50px;" src="${Ownuser.imageUrl}" class="rounded-circle" alt="image"></a>
                         <h3>
                             <a href="#">${Ownuser.userName}</a>
                         </h3>
                     </div>

                     <ul class="live-chat-right">
                         <li>
                             <button class="btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Phone" type="button"><i class="ri-phone-fill"></i></button>
                         </li>
                         <li>
                             <button class="btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Live" type="button"><i class="ri-live-fill"></i></button>
                         </li>
                         <li>
                             <button class="btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" type="button"><i class="ri-delete-bin-line"></i></button>
                         </li>
                     </ul>
                 </div>

                 <div class="live-chat-container">
                     <div class="chat-content">
                          
                     </div>

                     <div class="chat-list-footer">
                         <div class="d-flex align-items-center">
                             <div class="btn-box d-flex align-items-center me-3">
                                 <button class="file-attachment-btn d-inline-block me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="File Attachment" type="button"><i class="ri-attachment-2"></i></button>

                                 <button class="emoji-btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Emoji" type="button"><i class="ri-user-smile-line"></i></button>
                             </div>

                                <input type="text" id="content_txt_chat" class="form-control" placeholder="Type your message...">
                                <button onclick="chatSendMessage('${Ownuser.id}', '${chatdata.meId}')" class="btn btn-primary">Send</button>
                         </div>
                     </div>
                 </div>`
            chatbody.innerHTML += text;
            var chatcontent = document.querySelector('.chat-content');
            var texttxt = " "
            for (var i = 0; i < chatdata.chat.messages.length; i++) {
                var item = chatdata.chat.messages[i];
                if (item.senderId == me.id) {
                    texttxt += `<div class="chat mt-1 mb-1">
                                 <div class="chat-avatar">
                                     <a routerLink="/profile" class="d-inline-block">
                                         <img src="${me.imageUrl}" style="height:50px; width:50px;" class="rounded-circle" alt="image">
                                     </a>
                                 </div>

                                 <div class="chat-body">
                                     <div class="chat-message bg-success">
                                         <p class="text-white">${item.content}</p>
                                         <span class="time d-block">${item.dateTime}</span>
                                     </div>
                                 </div>
                             </div>`
                        ;

                }
                else {
                    texttxt += `<div class="chat chat-left mt-1 mb-1">
                                 <div class="chat-avatar">
                                     <a routerLink="/profile" class="d-inline-block">
                                         <img src="${Ownuser.imageUrl}" style="height:50px; width:50px;" class="rounded-circle" alt="image">
                                     </a>
                                 </div>

                                 <div class="chat-body">
                                     <div class="chat-message bg-primary">
                                         <p class="text-white">${item.content}</p>
                                         <span class="time d-block">${item.dateTime}</span>
                                     </div>
                                 </div>
                             </div>`
                        ;
                }
            }
            chatcontent.innerHTML = texttxt;
            //var text2 = `<div class="chat">
            //                 <div class="chat-avatar">
            //                     <a routerLink="/profile" class="d-inline-block">
            //                         <img src="" width="50" height="50" class="rounded-circle" alt="image">
            //                     </a>
            //                 </div>

            //                 <div class="chat-body">
            //                     <div class="chat-message">
            //                         <p>Hello, dear I want talk to you?</p>
            //                         <span class="time d-block">7:45 AM</span>
            //                     </div>
            //                 </div>
            //             </div>

            //             <div class="chat chat-left">
            //                 <div class="chat-avatar">
            //                     <a routerLink="/profile" class="d-inline-block">
            //                         <img src="${Ownuser.imageUrl}" width="50" height="50" class="rounded-circle" alt="image">
            //                     </a>
            //                 </div>

            //                 <div class="chat-body">
            //                     <div class="chat-message">
            //                         <p>Said how c</p>
            //                         <span class="time d-block">7:45 AM</span>
            //                     </div>
            //                 </div>
            //             </div>`;
        }
    });
    
}

async function GetAllLiveChatUsers() {
    $.ajax({
        url: "/Home/GetAllOnlineUsers",
        method: "GET",
        success: function (data) {
            div2.innerHTML = "";
            var text = " ";
            for (var i = 0; i < data.length; i++) {

                if ($(`#live_chat_online_users #${data[i].id}`).length <= 0) {

                    div2.innerHTML+=`<div class="chat-box" id="${data[i].id}" onclick="LiveMessageUser(id)">
                        <div class="image" style="overflow:hidden; margin-left:8px; margin-right:8px;">
                            <a href="#"><img style="border:solid 3px green; border-radius:50px; height:85px; width:85px; min-width:85px; min-height:85px;" src="${data[i].imageUrl}" class="rounded-circle" alt="image"></a>
                        </div>
                        <h3>
                            <a href="#">${data[i].userName}</a>
                        </h3>
                    </div>`
                }
            }
            if (div2 != null) {
                if (data.length == 1) {
                    LiveMessageUser(data[0].id);
                }
            }
        }
    });
}
setTimeout(GetAllLiveChatUsers, 800);
