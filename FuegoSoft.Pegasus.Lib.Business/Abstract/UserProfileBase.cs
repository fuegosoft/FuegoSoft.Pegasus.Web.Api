using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Abstract
{
    public abstract class UserProfileBase
    {
        public abstract UserProfile GetUserProfileByUserId();
        public abstract bool CreateUserProfile();
    }
}
