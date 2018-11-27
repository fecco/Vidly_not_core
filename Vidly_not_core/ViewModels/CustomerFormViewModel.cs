using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly_not_core.Models;

namespace Vidly_not_core.ViewModels
{
    public class CustomerFomrViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}