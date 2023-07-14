using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App.Entities.Models
{
    public class Post
    {
        public int id { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public int LikesCount { get; set; } = 0;
        public string VideoOrPhotoLink { get; set; }
        public bool IsVideo { get; set; }
        public virtual CustomIdentityUser Me { get; set; }

        private async Task CheckTags()
        {
            if (Tags == null)
                Tags = new();
        }
        public async Task AddLike() => LikesCount++;
        public async Task AddTag(Tag tag)
        {
            await CheckTags();
            Tags.Add(tag);
        }

        public async Task AddTagList(List<Tag> tags)
        {
            await CheckTags();
            foreach (var tag in tags)
                Tags.Add(tag);
        }

        public Post(string description,string link,CustomIdentityUser user, bool isphotoorvideo)
        {
            Description = description; Tags = new();
            LikesCount = 0; VideoOrPhotoLink = link; IsVideo = isphotoorvideo;
            Me = user;
        }

        
        public Post() {
            Tags = new();
        }
    }
}
