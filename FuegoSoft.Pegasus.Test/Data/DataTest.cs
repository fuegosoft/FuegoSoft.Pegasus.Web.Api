using System;
using Xunit;
using FuegoSoft.Pegasus.Lib.Data.Repository;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;

namespace FuegoSoft.Pegasus.Test
{
    public class DataTest
    {
        [Fact]
        public void GetUserIdByUsernameAndPasswordTest()
        {
            using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
            {

                var userId = userUnitOfWork.Users.GetUserIdByUsernameAndPassword("ajqportal", "Alvin123!!!");
            }
        }

        [Fact]
        public void GetUserCredentialByUsernameAndPasswordTest()
        {
            using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
            {
                var userCredential = userUnitOfWork.Users.GetUserCredentialByUsernameAndPassword("ajqportal", "Alvin123!!!");
            }
        }
    }
}
