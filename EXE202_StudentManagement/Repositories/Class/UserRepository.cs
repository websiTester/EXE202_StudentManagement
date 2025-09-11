using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;

namespace EXE202_StudentManagement.Repositories.Class
{
	public class UserRepository : IUserRepository
	{
		private readonly Exe202Context _context;
		public UserRepository(Exe202Context context)
		{
			_context = context;
		}
		public bool UpdateUser(User user)
		{
			_context.Users.Update(user);
			var result = _context.SaveChanges();
			return result > 0;

		}
	}
}