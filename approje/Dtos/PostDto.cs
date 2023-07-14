using App.Entities.Models;

namespace approje.Dtos
{
    public class PostDto
    {
        public string Description { get; set; }
        public int LikesCount { get; set; } = 0;
        public string VideoOrPhotoLink { get; set; }
        public bool IsVideo { get; set; }
        public PostDto() { }

        public PostDto(string description, int likesCount, string videoOrPhotoLink, bool ısVideo)
        {
            Description = description;
            LikesCount = likesCount;
            VideoOrPhotoLink = videoOrPhotoLink;
            IsVideo = ısVideo;
        }
    }
}
