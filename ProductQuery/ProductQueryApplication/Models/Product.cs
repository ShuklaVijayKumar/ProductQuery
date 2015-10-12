using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductQuery.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}