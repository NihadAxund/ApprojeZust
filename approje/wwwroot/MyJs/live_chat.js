var div2 = document.querySelector(".live-chat-slides");
var chatbody = document.querySelector(".live-chat-body");
connection.on("Connect", function (info, id) {

    
    GetAllLiveChatUsers();

});


connection.on("SendChatUser", function (id, mes) {

    alert(mes);

});



 function chatSendMessage(receiverId, senderId) {

    let content_txt = document.querySelector('#content_txt_chat');
    alert(content_txt.value);
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
            SendChatMessageFunction(receiverId, content_txt.value);

            //GetMessageCall(receiverId, senderId);
            content_txt.value = "";
        },
        error: function (err) {
            alert("xetta" + err);
        }
    })
}
async function LiveMessageUser(id) {
    $.ajax({
        url: `/Home/MyChat/${id}`,
      
        method: "GET",
        success: function (chatdata) {
            var me = chatdata.user;
            var Ownuser = chatdata.chat.receiver;
            chatbody.innerHTML = " ";
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
                         <form class="d-flex align-items-center">
                             <div class="btn-box d-flex align-items-center me-3">
                                 <button class="file-attachment-btn d-inline-block me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="File Attachment" type="button"><i class="ri-attachment-2"></i></button>

                                 <button class="emoji-btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Emoji" type="button"><i class="ri-user-smile-line"></i></button>
                             </div>

                                <input type="text" id="content_txt_chat" class="form-control" placeholder="Type your message...">

                          
                         </form>
                         <button onclick="chatSendMessage('${Ownuser.id}', '${chatdata.meId}')" class="send-btn d-inline-block">Send</button>
                     </div>
                 </div>`
            chatbody.innerHTML += text;
            var chatcontent = document.querySelector('.chat-content');
            var texttxt = " "
            alert("Count:" + chatdata.chat.messages.length);
            for (var i = 0; i < chatdata.chat.messages.length; i++) {
                var item = chatdata.chat.messages[i];
                if (item.senderId == me.id) {
                    texttxt += `<div class="chat">
                                 <div class="chat-avatar">
                                     <a routerLink="/profile" class="d-inline-block">
                                         <img src="${me.imageUrl}" width="50" height="50" class="rounded-circle" alt="image">
                                     </a>
                                 </div>

                                 <div class="chat-body">
                                     <div class="chat-message">
                                         <p>${chatdata.chat.messages[i].content}</p>
                                         <span class="time d-block">Nihad saat</span>
                                     </div>
                                 </div>
                             </div>`
                        ;

                }
                else {
                    texttxt += `<div class="chat chat-left">
                                 <div class="chat-avatar">
                                     <a routerLink="/profile" class="d-inline-block">
                                         <img src="${Ownuser.imageUrl}" width="50" height="50" class="rounded-circle" alt="image">
                                     </a>
                                 </div>

                                 <div class="chat-body">
                                     <div class="chat-message">
                                         <p>${chatdata.chat.messages[i].content}</p>
                                         <span class="time d-block">Nihad saat</span>
                                     </div>
                                 </div>
                             </div>`
                        ;
                }
            }
            chatcontent.innerHTML = texttxt;
            var text2 = `<div class="chat">
                             <div class="chat-avatar">
                                 <a routerLink="/profile" class="d-inline-block">
                                     <img src="" width="50" height="50" class="rounded-circle" alt="image">
                                 </a>
                             </div>

                             <div class="chat-body">
                                 <div class="chat-message">
                                     <p>Hello, dear I want talk to you?</p>
                                     <span class="time d-block">7:45 AM</span>
                                 </div>
                             </div>
                         </div>

                         <div class="chat chat-left">
                             <div class="chat-avatar">
                                 <a routerLink="/profile" class="d-inline-block">
                                     <img src="${Ownuser.imageUrl}" width="50" height="50" class="rounded-circle" alt="image">
                                 </a>
                             </div>

                             <div class="chat-body">
                                 <div class="chat-message">
                                     <p>Said how c</p>
                                     <span class="time d-block">7:45 AM</span>
                                 </div>
                             </div>
                         </div>`;
        }
    });
    
}

async function GetAllLiveChatUsers() {
    $.ajax({
        url: "/Home/GetAllOnlineUsers",
        method: "GET",
        success: function (data) {
            div2.innerHTML = " ";
            var text = " ";
            for (var i = 0; i < data.length; i++) {

                if ($(`#live_chat_online_users #${data[i].id}`).length <= 0) {

                     text += `<div class="chat-box" id="${data[i].id}" onclick="LiveMessageUser(id)">
                        <div class="image">
                            <a href="#"><img style="height:85px; width:85px;" src="${data[i].imageUrl}" class="rounded-circle" alt="image"></a>
                            <span class="status-online"></span>
                        </div>
                        <h3>
                            <a href="#">${data[i].userName}</a>
                        </h3>
                    </div>`

                }

            }
            if (text != null && div2 != null) {
                div2.innerHTML = text
                if (data.length == 1) {
                    LiveMessageUser(data[0].id);
                }
            }
        }
    });
}
setTimeout(GetAllLiveChatUsers, 800);
