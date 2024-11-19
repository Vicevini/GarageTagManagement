

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageTagManagement.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public List<int>? ActiveTags { get; set; }
    }
}