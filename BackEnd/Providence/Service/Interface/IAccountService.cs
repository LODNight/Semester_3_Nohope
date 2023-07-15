using Providence.Models;

namespace Providence.Service.Interface;

public interface IAccountService
{
    public dynamic ShowAccountActive();

    public bool ChangePass(ChangePass changePass);
    public bool CheckMail(string email);
    public dynamic VerifyCode(string account);
    public bool Active(Verify verify);
}
