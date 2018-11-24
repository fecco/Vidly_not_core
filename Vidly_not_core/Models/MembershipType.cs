using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly_not_core.Models
{
    public class MembershipType
    {
        public byte MembershipTypeId { get; set; }
        public string MembershipTypeName { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
    }
}