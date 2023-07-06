async function EditProfileImage() {
    var inputElement = document.querySelector(".container_section");
    if (inputElement.style.display == 'flex') {
        inputElement.style.display = 'none';
    }
    else {
        inputElement.style.display = 'flex';

    }

}

async function SaveUserImage() {
    var imagelink = document.querySelector("#container_Imagesavetextbox")
    var link = imagelink.value;
    alert(link);
    alert("Yeni0.1");
    $.ajax({
        url: `/Home/NewProfileImage/` + encodeURIComponent(link),
        method: "GET",
        success: function (data) {

            var jsonData = JSON.stringify(data);
            alert(jsonData);
            location.replace();
        },
        error: function (err) {
            console.log(err)
        }
    })
}