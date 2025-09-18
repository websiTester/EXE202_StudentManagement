using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using System.Threading.Tasks;

namespace EXE202_StudentManagement.Services.Class
{
    public class GroupTaskService : IGroupTaskService
    {
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskService(IGroupTaskRepository groupTaskRepository)
        {
            _groupTaskRepository = groupTaskRepository;
        }

        public void AddGroupTask(GroupTask groupTask)
        {
_groupTaskRepository.AddGroupTask(groupTask);
        }

        public bool DeleteGroupTask(int taskId)
        {
            return _groupTaskRepository.DeleteTask(taskId);
        }

        public GroupTask GetGroupTaskById(int id)
        {
return _groupTaskRepository.GetGroupTaskById(id);
        }

        public void UpdateGroupTask(int taskId, string newStatus)
        {
            var existingTask = _groupTaskRepository.GetGroupTaskById(taskId);

            if (existingTask != null && existingTask.Status != newStatus)
            {
                existingTask.Status = newStatus;
                _groupTaskRepository.UpdateGroupTask(existingTask);
            }

        }
    }
}
