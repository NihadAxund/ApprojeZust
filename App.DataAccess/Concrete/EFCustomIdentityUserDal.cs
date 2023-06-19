using App.Core.Abstraction;
using App.Entities.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Core.DataAccess.EntityFramework
{
    public class EFCustomIdentityUserDal : EfEntityRepositoryBase<CustomIdentityUser, IdentityDbContext<IdentityUser>>
    {
        public EFCustomIdentityUserDal(IdentityDbContext<IdentityUser> context) : base(context)
        {
        }

        // Özel kullanıcıya özgü metotlar buraya eklenebilir
    }
}
