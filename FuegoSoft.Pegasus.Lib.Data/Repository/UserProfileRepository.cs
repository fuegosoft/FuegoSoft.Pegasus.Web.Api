using FuegoSoft.Pegasus.Lib.Core.Utilities;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using FuegoSoft.Pegasus.Lib.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Data.Repository
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DbContext context) : base(context) { }

        public AyudaContext AyudaContext
        {
            get { return Context as AyudaContext; }
        }

        public UserProfile GetUserProfileByUserId(int userId) => AyudaContext.UserProfile.Where(w => w.UserId == userId).FirstOrDefault();
    }
}
