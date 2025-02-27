﻿using static SGA.ViewModels.StudentViewModel;
using System.ComponentModel.DataAnnotations;

namespace SGA.Models;

public class Student
{
    public int Id { get; set; }
 
    public string Name { get; set; }
    public decimal Attendance { get; set; }
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public Subjects Subject { get; set; }
}