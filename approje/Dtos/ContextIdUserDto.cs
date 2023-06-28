using App.Entities.Models;

namespace approje.Dtos
{
    public class ContextIdUserDto {

        public string ContextId { get; set; }
        public CustomIdentityUser CustomIdentityUser { get; set; }
        public ContextIdUserDto() { }

        public ContextIdUserDto(string contextId, CustomIdentityUser customIdentityUser)
        {
            ContextId = contextId;
            CustomIdentityUser = customIdentityUser;
        }
    }
}
