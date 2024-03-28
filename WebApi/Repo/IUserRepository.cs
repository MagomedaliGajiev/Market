using WebApi.Db;

namespace WebApi.Repo
{
    public interface IUserRepository
    {
        void UserAdd(string name, string password, RoleId roleId);
        RoleId UserCheck(string name, string password);
    }
}
