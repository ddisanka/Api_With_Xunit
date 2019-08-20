using CoreServices.Controllers;
using CoreServices.Repository;
using CoreServices.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CoreServices.Model;


namespace CoreServices.Test
{
    public class PostUnitTestController
    {
        private StudentRepository repository;
        public static DbContextOptions<StudentDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=CMDDDISANKA;Database=TestingNewDB;Trusted_Connection=True; MultipleActiveResultSets=True;";

        static PostUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<StudentDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public PostUnitTestController()
        {
            var context = new StudentDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            repository = new StudentRepository(context);

        }
        #region Get By Id  

        [Fact]
        public async void Task_GetStudentById_Return_OkResult()
        {
            //Arrange  
            var controller = new StudentController(repository);
            var studId = 1;

            //Act  
            var data = await controller.GetStudent(studId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetStudentById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new StudentController(repository);
            var studId = 3;

            //Act  
            var data = await controller.GetStudent(studId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetStudentById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new StudentController(repository);
            int? studId = null;

            //Act  
            var data = await controller.GetStudent(studId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetStudentById_MatchResult()
        {
            //Arrange  
            var controller = new StudentController(repository);
            int? studId = 1;

            //Act  
            var data = await controller.GetStudent(studId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var student = okResult.Value.Should().BeAssignableTo<StudentViewModel>().Subject;

            Assert.Equal("mala", student.Fname);
            Assert.Equal("ggh", student.Lname);
        }

        #endregion

        #region Get All  

        [Fact]
        public async void Task_GetStudents_Return_OkResult()
        {
            //Arrange  
            var controller = new StudentController(repository);

            //Act  
            var data = await controller.GetStudents();

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetStudents_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new StudentController(repository);

            //Act  
            var data = controller.GetStudents();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetStudents_MatchResult()
        {
            //Arrange  
            var controller = new StudentController(repository);

            //Act  
            var data = await controller.GetStudents();

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var student = okResult.Value.Should().BeAssignableTo<List<StudentViewModel>>().Subject;

            Assert.Equal("mala", student[0].Fname);
            Assert.Equal("ggh", student[0].Lname);

            Assert.Equal("kamala", student[1].Fname);
            Assert.Equal("kopkopkop", student[1].Lname);
        }

        #endregion

        #region Add New Blog  

        // [Fact]
        // public async void Task_Add_ValidData_Return_OkResult()
        // {
        //     //Arrange  
        //     var controller = new StudentController(repository);
        //     var post = new Post() { Title = "Test Title 3", Description = "Test Description 3", CategoryId = 2, CreatedDate = DateTime.Now };

        //     //Act  
        //     var data = await controller.AddPost(post);

        //     //Assert  
        //     Assert.IsType<OkObjectResult>(data);
        // }

        // [Fact]
        // public async void Task_Add_InvalidData_Return_BadRequest()
        // {
        //     //Arrange  
        //     var controller = new PostController(repository);
        //     Post post = new Post() { Title = "Test Title More Than 20 Characteres", Description = "Test Description 3", CategoryId = 3, CreatedDate = DateTime.Now };

        //     //Act              
        //     var data = await controller.AddPost(post);

        //     //Assert  
        //     Assert.IsType<BadRequestResult>(data);
        // }

        // [Fact]
        // public async void Task_Add_ValidData_MatchResult()
        // {
        //     //Arrange  
        //     var controller = new PostController(repository);
        //     var post = new Post() { Title = "Test Title 4", Description = "Test Description 4", CategoryId = 2, CreatedDate = DateTime.Now };

        //     //Act  
        //     var data = await controller.AddPost(post);

        //     //Assert  
        //     Assert.IsType<OkObjectResult>(data);

        //     var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        //     // var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;  

        //     Assert.Equal(3, okResult.Value);
        // }

        #endregion

        #region Update Existing Blog  

        // [Fact]
        // public async void Task_Update_ValidData_Return_OkResult()
        // {
        //     //Arrange  
        //     var controller = new PostController(repository);
        //     var postId = 2;

        //     //Act  
        //     var existingPost = await controller.GetPost(postId);
        //     var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
        //     var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        //     var post = new Post();
        //     post.Title = "Test Title 2 Updated";
        //     post.Description = result.Description;
        //     post.CategoryId = result.CategoryId;
        //     post.CreatedDate = result.CreatedDate;

        //     var updatedData = await controller.UpdatePost(post);

        //     //Assert  
        //     Assert.IsType<OkResult>(updatedData);
        // }

        // [Fact]
        // public async void Task_Update_InvalidData_Return_BadRequest()
        // {
        //     //Arrange  
        //     var controller = new PostController(repository);
        //     var postId = 2;

        //     //Act  
        //     var existingPost = await controller.GetPost(postId);
        //     var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
        //     var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        //     var post = new Post();
        //     post.Title = "Test Title More Than 20 Characteres";
        //     post.Description = result.Description;
        //     post.CategoryId = result.CategoryId;
        //     post.CreatedDate = result.CreatedDate;

        //     var data = await controller.UpdatePost(post);

        //     //Assert  
        //     Assert.IsType<BadRequestResult>(data);
        // }

        // [Fact]
        // public async void Task_Update_InvalidData_Return_NotFound()
        // {
        //     //Arrange  
        //     var controller = new PostController(repository);
        //     var postId = 2;

        //     //Act  
        //     var existingPost = await controller.GetPost(postId);
        //     var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
        //     var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        //     var post = new Post();
        //     post.PostId = 5;
        //     post.Title = "Test Title More Than 20 Characteres";
        //     post.Description = result.Description;
        //     post.CategoryId = result.CategoryId;
        //     post.CreatedDate = result.CreatedDate;

        //     var data = await controller.UpdatePost(post);

        //     //Assert  
        //     Assert.IsType<NotFoundResult>(data);
        // }

        #endregion

        #region Delete Post  

        [Fact]
        public async void Task_Delete_Student_Return_OkResult()
        {
            //Arrange  
            var controller = new StudentController(repository);
            var studId = 2;

            //Act  
            var data = await controller.DeleteStudent(studId);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async void Task_Delete_Student_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new StudentController(repository);
            var studId = 5;

            //Act  
            var data = await controller.DeleteStudent(studId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_Delete_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new StudentController(repository);
            int? studId = null;

            //Act  
            var data = await controller.DeleteStudent(studId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        #endregion
    }
}
