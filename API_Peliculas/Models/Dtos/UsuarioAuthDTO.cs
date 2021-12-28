using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Models.Dtos
{
    public class UsuarioAuthDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El password es obligatorio.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El password debe tener entre 4 y 10 caracteres.")]
        public string Password { get; set; }
    }
}
