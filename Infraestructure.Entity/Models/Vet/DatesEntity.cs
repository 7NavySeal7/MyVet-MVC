using Infraestructure.Entity.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Models.Vet
{
    [Table("Dates", Schema="Vet")]
    public class DatesEntity
    {
        [Key]
        public int IdDate { get; set; }
        public DateTime Date { get; set; }
        
        [MaxLength(100)]
        public string Contact  { get; set; }

        [ForeignKey("ServicesEntity")]
        public int IdServices { get; set; }
        public ServicesEntity ServicesEntity { get; set; }

        [ForeignKey("PetEntity")]
        public int IdPet { get; set; }
        public PetEntity PetEntity { get; set; }
        public DateTime? ClosingDate { get; set; }

        [ForeignKey("StateEntity")]
        public int IdState { get; set; }
        public StateEntity StateEntity { get; set; }
        
        [MaxLength(300)]
        public string Description { get; set; }

        [MaxLength(300)]
        public string Observation { get; set; }

        [ForeignKey("UserVetEntity")]
        public int? idUserVet { get; set; }
        public UserEntity UserVetEntity { get; set; }

    }
}
