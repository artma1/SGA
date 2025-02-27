﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGA.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGA.ViewModels
{
    public class StudentViewModel : Student
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Frequência")]
        public decimal Attendance { get; set; }
        

        public List<DisciplineGrade> Grades { get; set; } = new List<DisciplineGrade>();

        public class DisciplineGrade
        {
            public int SubjectId { get; set; }
            public string SubjectName { get; set; }
            public double Grade { get; set; }

            public Subjects Subject { get; set; }
        }

        public enum Subjects
        {
            [Description("Cálculo")]
            Calculo = 1,

            [Description("Economia")]
            Economia = 2,

            [Description("Geometria")]
            Geometria = 3,

            [Description("Filosofia")]
            Filosofia = 4,

            [Description("Projetos")]
            Projetos = 5
        }

    }
}
