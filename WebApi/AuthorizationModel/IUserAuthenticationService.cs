namespace WebApi.AuthorizationModel
{
    public interface IUserAuthenticationService
    {
        UserModel Authenticate(LoginModel model);
    }
}
