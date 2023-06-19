using App.Core.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace App.Entities.Models
{
    public class CustomIdentityUser:IdentityUser,IEntity
    {
    }
}
