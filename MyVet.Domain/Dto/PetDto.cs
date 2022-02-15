using Infraestructure.Entity.Models.Vet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyVet.Domain.Dto
{
    public class PetDto
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime DateBorns { get; set; }

        [Required(ErrorMessage = "El sexo es requeridos")]
        public int IdSex { get; set; }

        [Required(ErrorMessage = "El tipo de mascota es requerido")]
        public int IdTypePet { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }



        //Estas propiedades son utilizadas para caputurar los datos en tipo string del sexo y el tipo de mascota.
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string TypePet { get; set; }
        public int IdUser { get; set; }
    }
}
