using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IGroupTaskRepository
    {
        void AddGroupTask(GroupTask groupTask);
        GroupTask GetGroupTaskById(int taskId);
        void UpdateGroupTask(GroupTask groupTask);

        bool DeleteTask(int taskId);


    }
}
