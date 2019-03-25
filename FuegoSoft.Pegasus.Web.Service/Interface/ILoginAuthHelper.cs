using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Web.Service.Interface
{
    public interface ILoginAuthHelper
    {
        string GetUserToken(UserCredential stringToken);
    }
}
