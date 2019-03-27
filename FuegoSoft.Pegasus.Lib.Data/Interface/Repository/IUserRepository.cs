using System;
using System.Collections.Generic;
using FuegoSoft.Pegasus.Lib.Core.Utilities.Interface;
using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Lib.Data.Interface.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        int GetUserIdByUsernameAndPassword(string username, string password);
        UserCredential GetUserCredentialByUsernameAndPassword(string username, string password);
        Guid GetUserKeyByUsernameAndPassword(string username, string password);
        bool IsUserBanned(Guid userKey);
        bool IsUserDeleted(Guid userKey);
        UserCredential GetUserCredentialByUserKey(Guid userKey);
        bool IsUsernameIsAlreadyTaken(string username, string emailAddress, string contactNumber);
    }
}
