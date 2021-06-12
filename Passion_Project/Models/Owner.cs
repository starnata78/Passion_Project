using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Passion_Project.Models
{
    public class Owner
    {
        [Key]
        public int OwnerID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }

        /* 
        TODO: Address, contact number
         */
    }
}