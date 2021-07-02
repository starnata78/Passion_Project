using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passion_Project.Models.ViewModels
{
    public class DetailsOwner
    {
        public Owner SelectedOwner { get; set; }
        public IEnumerable<ContractDto> RelatedContracts { get; set; }
    }
}