using System.ComponentModel.DataAnnotations;

namespace SGA.Models;

public class Student
{
    public int Id { get; set; }
    [Display(Name = "Nome")]
    public string Name { get; set; }
    public int SubjectId { get; set; }
    [Display(Name = "Frequência")]
    public decimal Attendance { get; set; }
    public ICollection<StudentSubject>? StudentSubjects { get; set; }

}