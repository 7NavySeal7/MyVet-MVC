using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Models.Vet
{
    [Table("Sex", Schema="Vet")]
    public class SexEntity
    {
        [Key]
        public int IdSex { get; set; }
        
        [MaxLength(100)]
        public string Sex { get; set; }
    }
}
