namespace WebApi.AuthorizationModel
{
    public class AuthenticationMock : IUserAuthenticationService
    {
        public UserModel Authenticate(LoginModel model)
        {
            if (model.UserName == "admin" && model.Password == "password")
            {
                return new UserModel { Password = model.Password, UserName = model.UserName, Role = UserRole.Administrator };
            }
            if (model.UserName == "user" && model.Password == "super")
            {
                return new UserModel { Password = model.Password, UserName = model.UserName, Role = UserRole.User };
            }
            return null;
        }
    }
}
