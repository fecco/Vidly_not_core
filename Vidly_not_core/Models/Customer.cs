using System;
using System.Collections.Generic;
//az alábbi sort kell hozzáadni az override miatt:
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_not_core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        //az alábbi sor az override a normál konvenciónak:
        [Display(Name = "Date of Birth: ")]
        public DateTime? CustomersBirthDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedtoNewsLetter { get; set; }
        //navigation property mivel ez köti össze a customer-t a membership típussal

        public MembershipType MembershipType { get; set; }
        //az alábbi sort automatikusan külső kulcsként fogja felismerni:

        [Display(Name = "Membership Type: ")]
        public byte MembershipTypeId { get; set; }
    }
}