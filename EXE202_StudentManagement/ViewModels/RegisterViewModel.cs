using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EXE202_StudentManagement.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Remote(action: "IsUsernameInUse", controller: "Account")]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		[Remote(action: "IsEmailInUse", controller: "Account")]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Passwords and Confirm Password do not match")]
		public string ConfirmPassword { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		public string? Role { get; set; }
	}
}
