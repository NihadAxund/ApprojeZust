var myModal = document.querySelector('#exampleModalToggleImage');

var ImageSrc = null;
function openModal() {
    myModal.style.display = "flex";

}

function closeModal() {
    myModal.style.display = "none";

}



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
    let imagelink = document.querySelector("#container_Imagesavetextbox")
    let link = imagelink.value;
    $.ajax({
        url: `/Home/NewProfileImage/` + encodeURIComponent(link),
        method: "GET",
        success: function (data) {
         //   let jsonData = JSON.stringify(data);
            location.reload();
        },
        error: function (err) {
            console.log(err)
        }
    })
}
async function LoadingImageBtn() {
    let imagelink = document.querySelector("#Loading_Image_Link").value;
    //alert(imagelink)
    if (imagelink != null && imagelink != " ") {
        document.querySelector("#Loadingphotobox").src = imagelink;
        ImageSrc = imagelink;
    }

}

function getSelectedMediaType() {
    var photoRadio = document.getElementById("radioPhoto");
    var videoRadio = document.getElementById("radioVideo");

    if (photoRadio.checked) {
        return false;
    } else if (videoRadio.checked) {
        return true;
    } else {
        return null;
    }
}


async function AddPostApi() {
    if (ImageSrc != null && ImageSrc != " " && getSelectedMediaType()!=null) {
        var ischekc = getSelectedMediaType();
        
        var textarea = document.getElementById("TextAreaPost");
        let obj = {
            description: textarea.value,
            videoOrPhotoLink: ImageSrc,
            isVideo: ischekc
        };
       // console.log(obj);
        //alert(ImageSrc);

        $.ajax({
            url: `/Home/AddNewPost`,
            method: "POST",
            data: obj,
            success: function (data) {
               
                location.reload();
            },
            error: function (err) {
                alert(err);
                console.log(err);
            }
        })

    }
}


//////////////////////////////////////////////////
