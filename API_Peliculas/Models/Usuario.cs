using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
