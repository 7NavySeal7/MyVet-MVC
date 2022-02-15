using Infraestructure.Entity.Models.Vet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Models
{
    [Table("User", Schema = "Security")]
    public class UserEntity
    {
        [Key]
        public int IdUser { get; set; }
        
        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [MaxLength(200)]
        public string Password { get; set; }

        public IEnumerable<RolUserEntity> RolUserEntities { get; set; }

        public IEnumerable<UserPetEntity> UserPetEntities { get; set; }

        [NotMapped]
        public string FullName { get { return $"{this.Name} {this.LastName}"; } }
    }
}
