namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IClassRepository
    {
        Models.Class GetClassByCode(string classCode);

        IEnumerable<Models.Class> GetClassesByTeacherId(string teacherId);
        void AddClass(Models.Class newClass);
        bool IsClassCodeExist(string classCode);
    }
}
