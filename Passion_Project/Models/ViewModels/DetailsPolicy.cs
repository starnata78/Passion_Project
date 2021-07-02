using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passion_Project.Models.ViewModels
{
    public class DetailsPolicy
    {
        public PolicyDto SelectedPolicy { get; set; }

        public IEnumerable<InsurerDto> RelatedInsurers { get; set; }
    }
}