using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class HomeSelectViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Discipline> SelDiscs { get; set; }
        public IEnumerable<Discipline> NonSelDiscs { get; set; }
    }
}
