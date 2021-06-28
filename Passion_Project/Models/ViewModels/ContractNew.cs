using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passion_Project.Models.ViewModels
{
    public class ContractNew
    {

        //This viewmodel is a "vessel" model which stores information we need to present to /Contract/New;
        public IEnumerable<Owner> AvailableOwners { get; set; }
        public IEnumerable<Policy> AvailablePolicies { get; set; }
        public IEnumerable<Insurer> AvailableInsurers { get; set; }
    }
}