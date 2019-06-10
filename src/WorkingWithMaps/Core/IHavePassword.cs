using System.Security;

namespace WorkingWithMaps.Example.Core
{
    public interface IHavePassword
    {
        SecureString Password { get; }
        void ClearPassword();
    }
}