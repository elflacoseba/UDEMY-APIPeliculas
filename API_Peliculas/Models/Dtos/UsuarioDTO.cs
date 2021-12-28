using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Models.Dtos
{
    public class UsuarioDTO
    {
        public string NombreUsuario { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
