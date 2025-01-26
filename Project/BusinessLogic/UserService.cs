using System.Linq;
using System.Threading.Tasks;
using Program.Models;
using Program.DataAccess;

namespace Program.BusinessLogic
{
    public class UserService
    {
        private readonly Database _db; // Database instance

        public UserService(Database db)
        {
            _db = db;
        }

        // Authenticates a user by email and password
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await Task.Run(() =>
            {
                return _db.GetUsers().FirstOrDefault(u => u.Email == email && u.Password == password);
            });
        }

        // Adds a new user to the database
        public async Task AddUserAsync(User user)
        {
            await Task.Run(() => _db.AddUser(user));
        }

        // Edits an existing user
        public async Task EditUserAsync(int id, string name, string email, string password, UserRole role)
        {
            await Task.Run(() =>
            {
                var user = new User { Id = id, Name = name, Email = email, Password = password, Role = role };
                _db.UpdateUser(user);
            });
        }

        // Deletes a user by ID
        public async Task DeleteUserAsync(int id)
        {
            await Task.Run(() => _db.DeleteUser(id));
        }

        // Returns a list of all users
        public async Task<List<User>> ListUsersAsync()
        {
            return await Task.Run(() => _db.GetUsers().OrderBy(u => u.Id).ToList());
        }
    }
}