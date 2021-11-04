using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class Teacher
    {
        public int Id { set; get; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { set; get; }

        public List<Discipline> Disciplines { set; get; }
    }
}
