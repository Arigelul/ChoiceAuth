using ChoiceA.AnotherClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class Student : IValidatableObject
    {
        public int Id { set; get; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Forbidden("xxx", "yyy")]
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required]
        [StringLength(15, MinimumLength = 2)]
        [Display(Name = "Group")]
        public string Group { set; get; }

        public List<StudDisc> StudDiscs { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool IsForbidden(string prop)
            {
                string[] forbiddens = { "aaa", "bbb", "ccc" };
                return forbiddens.Any(f => prop == f);
            }

            if (IsForbidden(Name))
                yield return new ValidationResult("Name is a forbidden word.", new string[] { "Name" });
            if (IsForbidden(Group))
                yield return new ValidationResult("Group is a forbidden word.", new string[] { "Group" });
        }
    }
}
