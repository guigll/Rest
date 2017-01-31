using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rest.Models
{
    [Table("Pessoas")]
    public class Pessoa
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}