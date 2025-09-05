
using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IGroupService
    {
       Group GetGroupByMemberId(string memberId);
    }
}
