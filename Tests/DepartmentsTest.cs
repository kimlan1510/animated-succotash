using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace UniversityRegistrar
{
  [Collection("UniversityRegistrar")]
  public class DepartmentTest : IDisposable
  {
    public DepartmentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=university_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DepartmentEmptyAtFirst()
    {
      //Arrange, Act
      int result = Department.GetAll().Count;

      //Assertf
    }

    [Fact]
    public void Test_Save_SaveDepartmentToDatabase()
    {
      //Arrange
      Department testDepartment = new Department("computer science");
      testDepartment.Save();

      //Act
      List<Department> result = Department.GetAll();
      List<Department> testList = new List<Department>{testDepartment};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsDepartmentInDatabase()
    {
      //Arrange
      Department testDepartment = new Department("Beat Poetry");
      testDepartment.Save();
      //Act
      Department foundDepartment = Department.Find(testDepartment.GetId());
      //Assert
      Assert.Equal(testDepartment, foundDepartment);
    }

    [Fact]
    public void Test_Update_UpdatesDepartmentInDatabase()
    {
      //Arrange
      Department testDepartment = new Department("Sleepology");
      testDepartment.Save();
      string newName= "Beat Poetry";
      //Act
      testDepartment.Update("Beat Poetry");
      string result =testDepartment.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_AddCourse_AddsCourseToDepartment()
    {
      //Arrange
      Department testDepartment = new Department("science");
      testDepartment.Save();

      Course testCourse = new Course("Sleepology", "SL101", "No", "F");
      testCourse.Save();

      Course testCourse2 = new Course("Underwater Basketweaving", "UB107", "No", "N/A");
      testCourse2.Save();

      //Act
      testDepartment.AddCourse(testCourse);
      testDepartment.AddCourse(testCourse2);

      List<Course> result = testDepartment.GetCourses();
      List<Course> testList = new List<Course>{testCourse, testCourse2};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetCourses_ReturnsAllDepartmentCourses_CourseList()
    {
      //Arrange
      Department testDepartment = new Department("Expandrew");
      testDepartment.Save();

      Course testCourses1 = new Course("Underwater Basketweaving", "UB107", "No", "N/A");
      testCourses1.Save();

      Course testCourses2 = new Course("Sleepology", "SL101", "No", "F");
      testCourses2.Save();

      //Act
      testDepartment.AddCourse(testCourses1);
      List<Course> result = testDepartment.GetCourses();
      List<Course> testList = new List<Course> {testCourses1};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_AddStudent_AddsStudentToDepartment()
    {
      //Arrange
      Department testDepartment = new Department("Sociology");
      testDepartment.Save();

      Student testStudent = new Student("Expandrew", new DateTime(2016, 10, 20), "Game Art & Design");
      testStudent.Save();

      Student testStudent2 = new Student("Kimlan", new DateTime(2017, 02, 28), "Software Engineering");
      testStudent2.Save();

      //Act
      testDepartment.AddStudent(testStudent);
      testDepartment.AddStudent(testStudent2);

      List<Student> result = testDepartment.GetStudent();
      List<Student> testList = new List<Student>{testStudent, testStudent2};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetStudent_ReturnsAllDepartmentStudent_StudentList()
    {
     //Arrange
     Department testDepartment = new Department("Medical Science");
     testDepartment.Save();

     Student testStudent1 = new Student("MaryAnne", new DateTime(2015, 05, 14), "Marine Biology");
     testStudent1.Save();

     Student testStudent2 = new Student("Garth", new DateTime(2014, 09, 05), "Literature");
     testStudent2.Save();

     //Act
     testDepartment.AddStudent(testStudent1);
     List<Student> savedStudent = testDepartment.GetStudent();
     List<Student> testList = new List<Student> {testStudent1};

     //Assert
     Assert.Equal(testList, savedStudent);
    }

    [Fact]
    public void Delete_DeletesDepartmentAssociationsFromDatabase_StudentsList()
    {
      //Arrange
      Department testDepartment = new Department("Homeland Security");
      testDepartment.Save();

      Course testCourse = new Course("Sleepology", "SL101", "No", "F");
      testCourse.Save();

      Student testStudent = new Student("Steven", new DateTime(1984, 12, 25), "Gun Economics");
      testStudent.Save();

      //Act
      testDepartment.AddCourse(testCourse);
      testDepartment.AddStudent(testStudent);
      testDepartment.Delete();

      List<Course> resultDepartmentCourses = testDepartment.GetCourses();
      List<Course> testDepartmentCourses = new List<Course> {};

      List<Student> resultDepartmentStudents = testDepartment.GetStudent();
      List<Student> testDepartmentStudents = new List<Student> {};

      //Assert
      Assert.Equal(testDepartmentStudents, resultDepartmentStudents);
      Assert.Equal(testDepartmentCourses, resultDepartmentCourses);
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
      Department.DeleteAll();
    }
  }
}
