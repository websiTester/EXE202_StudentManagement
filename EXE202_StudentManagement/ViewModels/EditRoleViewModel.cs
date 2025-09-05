using EXE202_StudentManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace EXE202_StudentManagement.ViewModels
{
	public class EditRoleViewModel
	{
		public EditRoleViewModel() { UserList = new List<User>(); }
		public string Id { get; set; }

		[Required(ErrorMessage = "Role Name is required")]
		public string RoleName { get; set; }

		public List<User> UserList { get; set; }
	}
}
