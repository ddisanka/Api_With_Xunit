using CoreServices.Model;
using CoreServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
    public interface IStudentRepository
    {
        Task<List<StudentViewModel>> GetStudents();
        Task<StudentViewModel> GetStudent(int? stdId);

        Task<int> AddStudent(Student student);

        Task<int> DeleteStudent(int? stdId);

        Task UpdateStudent(Student student);
    }
}
