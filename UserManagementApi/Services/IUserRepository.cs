using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel AddUser(UserModel user);
        UserModel GetUserById(int id);
        bool DeleteUser(int id);
    }
}
