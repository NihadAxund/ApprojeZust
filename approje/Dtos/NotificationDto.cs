using approje.Enums;

namespace approje.Dtos
{
    public class NotificationDto
    {
        public NotificationEnum notificationEnum { get; set; } = NotificationEnum.FriendRequest; 
        public string UserName { get; set; } = string.Empty;
       
    }
}
