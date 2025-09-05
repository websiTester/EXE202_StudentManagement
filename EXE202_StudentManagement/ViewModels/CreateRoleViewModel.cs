using System.ComponentModel.DataAnnotations;

namespace EXE202_StudentManagement.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public string RoleName { get; set; }
	}
}
