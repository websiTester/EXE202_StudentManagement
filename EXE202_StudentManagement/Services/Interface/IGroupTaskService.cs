using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IGroupTaskService
    {
        void AddGroupTask(GroupTask groupTask);

        void UpdateGroupTask(int taskId,string newStatus);
        GroupTask GetGroupTaskById(int id);
        bool DeleteGroupTask(int taskId);

    }
}
