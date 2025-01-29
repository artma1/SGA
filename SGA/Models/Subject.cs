namespace SGA.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SubjectDescription { get; set; }
    public ICollection<StudentSubject>? StudentSubjects { get; set; }

}
