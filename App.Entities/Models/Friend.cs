using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    [Index(nameof(YourFriendId), IsUnique = true)]
    public class Friend
    {
        public int Id { get; set; }
        public string MyId { get; set; }
        public string YourFriendId { get; set; }
        public Friend() { }
        public Friend(string myid,string yourfriendid)
        {
            MyId = myid;
            YourFriendId = yourfriendid;
        }

    }
}
