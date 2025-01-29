using System.ComponentModel.DataAnnotations;

namespace SGA.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        //public int SubjectId { get; set; }
        //public Subject? Subject { get; set; }

        [Display(Name = "Nota")]
        public int Grade { get; set; }

        [Display(Name = "Frequência")]
        public decimal Attendance { get; set; }
        public SubjectEnum Subject { get; set; }
    }
}
