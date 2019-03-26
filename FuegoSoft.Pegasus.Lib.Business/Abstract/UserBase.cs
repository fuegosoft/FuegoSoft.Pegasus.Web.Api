﻿using System;
using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Lib.Business.Abstract
{
    public abstract class UserBase
    {
        public abstract UserCredential GetUserCredentialByUsernameAndPassword();
        public abstract int GetUserIdByUsernameAndPassword();
        public abstract Guid GetUserKeyByUsernameAndPassword();
        public abstract bool IsUserHasBeenBanned();
        public abstract bool IsUserHasBeenDeleted();
    }
}