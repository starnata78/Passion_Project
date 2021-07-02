using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Passion_Project.Models
{
    public class Insurer
    {
        [Key]
        public int InsurerID { get; set; }
        public string Name { get; set; }

        //insurer can offer many different policies
        public ICollection<PolicyDto> Policies { get; set; }

    }

    public class InsurerDto
    {
        public int InsurerID { get; set; }
        public string Name { get; set; }
    }
}