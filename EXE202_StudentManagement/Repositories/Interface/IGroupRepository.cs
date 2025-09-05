
using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IGroupRepository
    {

         Group GetGroupByMemberId(string memberId);

    }
}
