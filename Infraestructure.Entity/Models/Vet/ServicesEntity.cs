using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Models.Vet
{
    [Table("Services", Schema = "Vet")]
    public class ServicesEntity
    {
        [Key]
        public int IdService { get; set; }
        public string Service { get; set; }
        public string Description  { get; set; }
    }
}
