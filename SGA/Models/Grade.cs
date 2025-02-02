using static SGA.ViewModels.StudentViewModel;

namespace SGA.Models
{

  public class Grade
  {
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int Value { get; set; }
    public int SubjectId { get; set; }
    public Subjects subject { get; set; }
  }

}
