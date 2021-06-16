using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passion_Project.Models
{
    public class Contract
    {
        [Key]
        public int ID { get; set; }
        //Owner can have many policies
        [ForeignKey("Owner")]
        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        //Policy can have only one owner
        [ForeignKey("Policy")]
        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        //The same policy type can be offered by many insurers
        [ForeignKey("Insurer")]
        public int InsurerID { get; set; }
        public virtual Insurer Insurer { get; set; }

        public bool Active { get; set; }

        public DateTime PurchaseDate { get; set; }


    }
    public class ContractDto
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Policy { get; set; }
        public string Insurer { get; set; }
        public bool Active { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

}