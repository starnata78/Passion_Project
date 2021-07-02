using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passion_Project.Models.ViewModels
{
    public class DetailsInsurer
    {
        public InsurerDto SelectedInsurer { get; set; }
        public IEnumerable<PolicyDto> RelatedPolicies { get; set; }
    }
}