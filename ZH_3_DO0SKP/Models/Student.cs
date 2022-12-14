using System;
using System.Collections.Generic;

namespace ZH_3_DO0SKP.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public string? Neptun { get; set; }

    public virtual ICollection<Order> Order { get; } = new List<Order>();
}
