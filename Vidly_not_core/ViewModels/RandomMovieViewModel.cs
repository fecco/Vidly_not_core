using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly_not_core.Models;

namespace Vidly_not_core.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}