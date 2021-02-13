using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_KASR_MAGZ.Models
{
    public class Players
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public string Club { get; set; }

    }
}
