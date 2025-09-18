using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class GroupTaskRepository : IGroupTaskRepository
    {
        private readonly Exe202Context _context;
        public GroupTaskRepository(Exe202Context context)
        {
            _context = context;
        }

        public void AddGroupTask(GroupTask groupTask)
        {
             _context.GroupTasks.Add(groupTask);
            _context.SaveChanges();
        }

     

        public bool DeleteTask(int taskId)
        {
            var taskToDelete = _context.GroupTasks.Find(taskId);
            if (taskToDelete == null)
            {
                return false;
            }

            _context.GroupTasks.Remove(taskToDelete);
            _context.SaveChanges();
            return true;
        }

        public GroupTask GetGroupTaskById(int taskId)
        {
            return _context.GroupTasks.Find(taskId);
        }

        public void UpdateGroupTask(GroupTask groupTask)
        {
            _context.GroupTasks.Update(groupTask);
            _context.SaveChanges();
        }
    }
}
