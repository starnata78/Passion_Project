using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passion_Project.Models.ViewModels
{
    public class UpdateContract
    {
        //serves just as a view model to store information we need to present to /Contract/Update/{}:
        //the existing information 
        //also like to include all owners, policies and insurers to choose from when updating this contract
        public ContractDto SelectedContract { get; set; }
        
        //all ownerID to choose form when updating Contract info

        public IEnumerable<Owner> OwnersOptions { get; set; }

        public IEnumerable<Policy> PoliciesOptions { get; set; }
        public IEnumerable<Insurer>InsurersOptions{ get; set; }

    }
}