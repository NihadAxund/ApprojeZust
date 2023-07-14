using App.Entities.Models;

namespace approje.Dtos
{
    public class PostDto
    {
        public string Description { get; set; }

        public string VideoOrPhotoLink { get; set; }
        public bool IsVideo { get; set; }
        public PostDto() { }

        public PostDto(string description,string videoOrPhotoLink, bool ısVideo)
        {
            Description = description;
            VideoOrPhotoLink = videoOrPhotoLink;
            IsVideo = ısVideo;
        }
    }
}
