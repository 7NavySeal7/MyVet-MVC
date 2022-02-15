using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVet.Domain.Dto
{
    public class DatesDto
    {
        public int IdDate { get; set; }
        public DateTime Date { get; set; }

        [MaxLength(100)]
        public string Contact { get; set; }

        public int IdServices { get; set; }

        public int IdPet { get; set; }

        public DateTime? ClosingDate { get; set; }

        public int IdState { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [MaxLength(300)]
        public string Observation { get; set; }

        public int? idUserVet { get; set; }


        public string NamePet { get; set; }
        public string NameState { get; set; }
        public string NameService { get; set; }
        public string StrClosingDate { get; set; }
        public string StrDate { get; set; }

    }
}
