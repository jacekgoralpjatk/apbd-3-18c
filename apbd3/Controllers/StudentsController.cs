using System.Globalization;
using apbd3.Models;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;

namespace apbd3.Controllers;

[ApiController]
[Route("students")]
public class StudentsController : ControllerBase
{
    
    [HttpGet("{idStudent}")]
    public IActionResult GetStudent(string idStudent)
    {
        Student student;
        using (var reader = new StreamReader("Resources/students.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            student = csv.GetRecords<Student>().ToList().Find( x => x.numerIndeksu.Equals(idStudent));
        }
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