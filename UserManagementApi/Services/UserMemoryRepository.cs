using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public class UserMemoryRepository : IUserRepository
    {
        private static readonly List<UserModel> _users = new List<UserModel>();
        private static int _nextId = 1;

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _users;
        }

        public UserModel AddUser(UserModel user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public UserModel GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public bool DeleteUser(int id)
        {
            var userToRemove = _users.FirstOrDefault(u => u.Id == id);
            if (userToRemove == null)
            {
                return false;
            }
            _users.Remove(userToRemove);
            return true;
        }
    }
}
