using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Seyid.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public decimal Grade { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public override string ToString()
        {
            return $"Id: {Id},Name: {Name},Surname: {Surname}, Age: {Age}, Grade: {Grade}, GroupName: {Group.Name}";
        }
    }
}
