using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.Net_MvcWeb_Pj3.Aptech.Models
{
   public class Employee
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public double Salary { get; set; }

    public bool Status { get; set; }

    public DateTime JoiningDate { get; set; }

    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual  Department? Department { get; set; }

    public string StatusText => Status ? "Active" : "Deactive";

    public string JoiningDateSQL => JoiningDate.ToString("yyyy-MM-dd");

    public string JoiningDateVi => JoiningDate.ToString("dd/MM/yyyy");

    public bool Invalid()
    {
        bool invalid = false;

        if (Name.Length < 3)
        {
            invalid = true;
        }

        if (Name.Length > 33)
        {
            invalid = true;
        }

        if (Salary < 10000)
        {
            invalid = true;
        }

        return invalid;
    }
}

}
