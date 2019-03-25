using System;
using FuegoSoft.Pegasus.Lib.Business.Planner;
using FuegoSoft.Pegasus.Lib.Business.Strategy;
using Xunit;
namespace FuegoSoft.Pegasus.Test
{

    public class BusinessTest
    {
        UserPlanner userLoginPlanner;
        public BusinessTest()
        {
            userLoginPlanner = new UserPlanner();
        }

        [Fact]
        public void GetUserCredentialByLoginTest()
        {
            userLoginPlanner.SetUserStrategy(new UserStrategy("ajqportal", "Alvin123!!!"));
            var userCredential = userLoginPlanner.GetUserCredential();
        }

        [Fact]
        public void GetUserIdByLoginTest()
        {
            userLoginPlanner.SetUserStrategy(new UserStrategy("ajqportal", "Alvin123!!!"));
            var userId = userLoginPlanner.GetUserId();
        }

        [Fact]
        public void GetUserKeyTest()
        {
            userLoginPlanner.SetUserStrategy(new UserStrategy("ajqportal", "Alvin123!!!"));
            var userKey = userLoginPlanner.GetUserKey();
        }
    }
}
