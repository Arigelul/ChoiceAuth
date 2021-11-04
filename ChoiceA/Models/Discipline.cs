using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class Discipline
    {
        public int Id { set; get; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        [Display(Name = "Title")]
        public string Title { set; get; }

        [Display(Name = "Annotation")]
        public string Annotation { set; get; }

        public int TeacherId { set; get; }
        public Teacher Teacher { set; get; }
        public List<StudDisc> StudDiscs { set; get; }
    }
}
