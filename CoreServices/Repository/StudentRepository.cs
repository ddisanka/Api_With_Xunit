using CoreServices.Model;
using CoreServices.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
    public class StudentRepository:IStudentRepository
    {
        StudentDBContext db;
        public StudentRepository(StudentDBContext _db)
        {
            db = _db;
        }

        public async Task<List<StudentViewModel>> GetStudents()
        {
            if (db != null)
            {
                return await (from p in db.tblStudents
                              select new StudentViewModel
                              {
                                  ID = p.ID,
                                  Fname = p.Fname,
                                  Lname = p.Lname,
                                  email = p.email,
                                  gender = p.gender,
                                  phone = p.phone,
                                  DOB = p.DOB
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<StudentViewModel> GetStudent(int? stdId)
        {
            if (db != null)
            {
                return await (from p in db.tblStudents
                              where p.ID == stdId
                              select new StudentViewModel
                              {
                                  ID = p.ID,
                                  Fname = p.Fname,
                                  Lname = p.Lname,
                                  email = p.email,
                                  gender = p.gender,
                                  phone = p.phone,
                                  DOB = p.DOB
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddStudent(Student student)
        {
            if (db != null)
            {
                await db.tblStudents.AddAsync(student);
                await db.SaveChangesAsync();

                return student.ID;
            }

            return 0;
        }

        public async Task<int> DeleteStudent(int? stdId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var student = await db.tblStudents.FirstOrDefaultAsync(x => x.ID == stdId);

                if (student != null)
                {
                    //Delete that post
                    db.tblStudents.Remove(student);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateStudent(Student student)
        {
            if (db != null)
            {
                //Delete that post
                db.tblStudents.Update(student);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

    }
}
