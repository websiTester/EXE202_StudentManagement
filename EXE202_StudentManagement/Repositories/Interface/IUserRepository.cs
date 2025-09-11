using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
	public interface IUserRepository
	{
		public bool UpdateUser(User user);
	}
}