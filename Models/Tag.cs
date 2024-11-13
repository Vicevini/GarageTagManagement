// `Models/Tag.cs`**: Modelo de dados para uma tag, com propriedades como `Id`, `IdApartamento`, `TagsAtivas`, `TipoTag`, `ValidadeTag`, `IsActive`.

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
        public string? IdApartamento { get; set; }

        [Required]
        public int? TagsAtivas { get; set; }

        [Required]
        public string? TipoTag { get; set; }

        [Required]
        public DateTime? ValidadeTag { get; set; } 

        [Required]
        public bool IsActive { get; set; }
    }
}