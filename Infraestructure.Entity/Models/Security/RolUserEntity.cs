using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Models
{
    [Table("RolUser", Schema = "Security")]
    public class RolUserEntity
    {
        [Key]
        public int IdRolUser { get; set; }
        
        [ForeignKey("RolEntity")]
        public int IdRol { get; set; }
        public RolEntity RolEntity { get; set; }

        [ForeignKey("UserEntity")]
        public int idUser { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}
