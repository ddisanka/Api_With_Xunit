using CoreServices.Model;
using System;

namespace CoreServices.Test
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(StudentDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.tblStudents.AddRange(
                 //new Student() { Fname = "chamidu", Lname = "wakka", email = "adds@gmail.com", gender = "1", phone = 454545545, DOB = DateTime.Now  },
                 new Student() { Fname = "mala", Lname = "ggh", email = "adds@gmail.com", gender = "1", phone = 454545545, DOB = DateTime.Now  },
                 new Student() { Fname = "kamala", Lname = "kopkopkop", email = "adds@gmail.com", gender = "1", phone = 454545545, DOB = DateTime.Now  }
            );
            context.SaveChanges();
        }
    }
}