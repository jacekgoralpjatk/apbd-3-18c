using System.Globalization;
using apbd3.Models;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;

namespace apbd3.Controllers;

[ApiController]
[Route("students")]
public class StudentsController : ControllerBase
{
    [HttpGet("names")]
    public string GetNames()
    {
        return "students names";
    }

    [HttpGet("{idStudent}/grades")]
    public string GetStudentGrades(int idStudent)
    {
        return idStudent + " grades: 1,2,3";
    }

    [HttpGet("{idStudent}")]
    public IActionResult GetStudent(int idStudent)
    {
        var student = new Student()
        {
            numerIndeksu = "s1000",
            imie = "Jacke",
            nazwisko = "Gorsky"
        };
        return Ok(student);
    }

    [HttpPost("addStudent")]
    public IActionResult CreateStudent(Student newStudent)
    {
        List<Student> list;
        using (var reader = new StreamReader("Resources/students.csv"))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            list = csvReader.GetRecords<Student>().ToList();
        }

        using (var writer = new StreamWriter("Resources/students.csv"))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            list.Add(newStudent);
            csvWriter.WriteRecords(list);
        }

        return Ok("gituwa");
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        using (var reader = new StreamReader("Resources/students.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return Ok(csv.GetRecords<Student>().ToList());
        }
    }
    
}