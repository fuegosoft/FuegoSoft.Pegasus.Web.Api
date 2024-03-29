﻿using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class UserPlanner
    {
        private UserBase _userBase;

        public void SetUserStrategy(UserBase userBase)
        {
            this._userBase = userBase;
        }

        public UserCredential GetUserCredential() => _userBase.GetUserCredentialByUsernameAndPassword();

        public int GetUserId() => _userBase.GetUserIdByUsernameAndPassword();

        public Guid GetUserKey() => _userBase.GetUserKeyByUsernameAndPassword();

        public bool IsUserHasBeenBanned() => _userBase.IsUserHasBeenBanned();

        public bool IsUserHasBeenDeleted() => _userBase.IsUserHasBeenDeleted();

        public UserCredential GetUserCredentialByUserKey() => _userBase.GetUserCredentialByUserKey();

        public int CreateUser() => _userBase.CreateUserAndRetrieveUserId();

        public bool IsUsernameIsAlreadyTaken() => _userBase.IsUsernameIsAlreadyTaken();

        public bool UpdateUserPassword() => _userBase.UpdateUserPassword();

        public int UpdateUserAndGetUserId() => _userBase.UpdateUserAndRetrieveUserId();
    }
}
