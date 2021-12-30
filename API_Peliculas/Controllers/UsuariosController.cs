using API_Peliculas.Models;
using API_Peliculas.Models.Dtos;
using API_Peliculas.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace API_Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _uRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsuariosController(IUsuarioRepository plRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment, IConfiguration config)
        {
            _uRepo = plRepo;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _config = config;
        }

        [HttpGet]
        public IActionResult Getusuarios()
        {
            var listaUsuarios = _uRepo.GetUsuarios();

            var listaUsuariosDTO = new List<UsuarioDTO>();

            foreach (var user in listaUsuarios)
            {
                listaUsuariosDTO.Add(_mapper.Map<UsuarioDTO>(user));
            }

            return Ok(listaUsuariosDTO);
        }

        [HttpGet("{Id:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int Id)
        {
            Usuario user = _uRepo.GetUsuario(Id);

            if (user != null)
            {
                UsuarioDTO userDTO = _mapper.Map<UsuarioDTO>(user);

                return Ok(userDTO);
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpPost("Registro")]
        public IActionResult Registro(UsuarioAuthDTO userAuthDTO)
        {            
            if (_uRepo.ExistsUsuario(userAuthDTO.Usuario))
            {
                return BadRequest("El usuario ya existe.");
            }
            else
            {
                var usuarioCrear = new Usuario();

                usuarioCrear.NombreUsuario = userAuthDTO.Usuario;

                var usuarioCreado = _uRepo.Registro(usuarioCrear, userAuthDTO.Password);

                return Ok(usuarioCreado);
            }
        }
       

        [HttpPost("Login")]
        public IActionResult Login(UsuarioAuthLoginDTO usuarioAuthLoginDTO)
        {
            var usuarioDesdeRepo = _uRepo.Login(usuarioAuthLoginDTO.Usuario, usuarioAuthLoginDTO.Password);

            if (usuarioDesdeRepo == null)
            {
                return Unauthorized();
            }
            else
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuarioDesdeRepo.NombreUsuario)
                };

                //Generación de Token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings.Token").Value));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credenciales,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });
            }

        }
    }
}
