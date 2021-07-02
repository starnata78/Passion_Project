using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Passion_Project.Models
{
    public class Policy
    {
        [Key]
        public int PolicyID { get; set; }
        public string Name { get; set; }

        //The same policy type can be offered by many insurers
        public ICollection<Insurer> Insurers { get; set; }
    }

   
}

