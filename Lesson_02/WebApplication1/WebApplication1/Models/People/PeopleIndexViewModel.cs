using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.People
{
    public class PeopleIndexViewModel
    {
        public List<PersonViewModel> People { get; set; }
        public PersonViewModel MaxPerson { get; set; }
        public PersonViewModel MinPerson { get; set; }
    }
}
