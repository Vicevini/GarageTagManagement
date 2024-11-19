
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageTagManagement.Models
{
    public class Tag
    {
     
        [Key]
        public int Id { get; set; }

        [Required]
        public int? IdApartamento { get; set; }

        //todo - inserir created_at

        [Required]
        public string? TipoTag { get; set; }

        [Required]
        public DateTime? ValidadeTag { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
    }   