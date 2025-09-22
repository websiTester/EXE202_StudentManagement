using EXE202_StudentManagement.Repositories.Class;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Class
{
    public class GradingService :IGradingService
    {
        private readonly IGradingRepository _repository;
        public GradingService(IGradingRepository repository)
        {
            _repository = repository;
        }

        public GradingViewModel GetGradingDetails(int groupId, int assignmentId)
        {
            return _repository.GetGradingDetails(groupId, assignmentId);
        }

        public bool SaveGrading(GradingViewModel model)
        {
            try
            {
                _repository.SaveGrading(model);
                return true;
            }
            catch
            {
                // Ghi log lỗi tại đây nếu cần thiết
                return false;
            }
        }
    }
}
