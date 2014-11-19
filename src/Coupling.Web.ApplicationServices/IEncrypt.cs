
namespace Coupling.Web.ApplicationServices
{
    public interface IEncrypt
    {
        string Encrypt(string clearText);
        string Encrypt(string clearText, string salt);
    }
}
