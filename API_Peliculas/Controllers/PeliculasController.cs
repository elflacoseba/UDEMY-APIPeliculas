using API_Peliculas.Models;
using API_Peliculas.Models.Dtos;
using API_Peliculas.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace API_Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepository _plRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public PeliculasController(IPeliculaRepository plRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _plRepo = plRepo;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _plRepo.GetPeliculas();

            var listaPeliculasDTO = new List<PeliculaDTO>();

            foreach (var pelis in listaPeliculas)
            {
                listaPeliculasDTO.Add(_mapper.Map<PeliculaDTO>(pelis));
            }

            return Ok(listaPeliculasDTO);
        }

        [HttpGet("{Id:int}", Name = "GetPelicula")]
        public IActionResult GetPelicula(int Id)
        {
            Pelicula peli = _plRepo.GetPelicula(Id);

            if (peli != null)
            {
                PeliculaDTO peliDTO = _mapper.Map<PeliculaDTO>(peli);

                return Ok(peliDTO);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{categoriaId:int}", Name = "GetPeliculasEnCategoria")]
        public IActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            IEnumerable<Pelicula> pelis = _plRepo.GetPeliculasPorCategoria(categoriaId);

            if (pelis != null)
            {
                List<PeliculaDTO> pelisDTO = new List<PeliculaDTO>();

                foreach (var item in pelis)
                {
                    pelisDTO.Add(_mapper.Map<PeliculaDTO>(item));
                }

                return Ok(pelisDTO);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {

            try
            {
                var pelis = _plRepo.BuscarPelicula(nombre);

                if (pelis.Any())
                {
                    List<PeliculaDTO> pelisDTO = new List<PeliculaDTO>();

                    foreach (var item in pelis)
                    {
                        pelisDTO.Add(_mapper.Map<PeliculaDTO>(item));    
                    }

                    return Ok(pelisDTO);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando la información de la aplicación.");
            }
        }

        [HttpPost]
        public IActionResult CrearPelicula([FromForm] PeliculaCreateDTO peliculaCreateDTO)
        {
            if (peliculaCreateDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_plRepo.ExistsPelicula(peliculaCreateDTO.Nombre))
            {
                ModelState.AddModelError("", "La película ya existe.");
                return StatusCode(404,ModelState);
            }

            /*Subida de archivo*/
            var archivo = peliculaCreateDTO.Foto;
            var rutaPrincipal = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if (archivo.Length > 0)
            {
                //nueva imagen
                var nombreFoto = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"fotos");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(subidas,nombreFoto + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStream);
                }

                peliculaCreateDTO.RutaImagen = @"\fotos\" + nombreFoto + extension;
            }

            var peli = _mapper.Map<Pelicula>(peliculaCreateDTO);

           if (_plRepo.CreatePelicula(peli))
            {
                return CreatedAtRoute("GetPelicula", new { Id = peli.Id }, peli);
            }
            else
            {
                ModelState.AddModelError("", $"La película {peli.Nombre} no se pudo crear.");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// Actualiza la información de una Película.
        /// </summary>
        /// <param name="Id">ID de la Película</param>
        /// <param name="PeliculaDTO"></param>
        /// <returns></returns>
        [HttpPatch("{Id:int}", Name = "ActualizarPelicula")]        
        public IActionResult ActualizarPelicula(int Id, [FromBody] PeliculaDTO PeliculaDTO)
        {
            if (PeliculaDTO == null || Id != PeliculaDTO.Id)
            {
                return BadRequest(ModelState);
            }

            Pelicula peli = _mapper.Map<Pelicula>(PeliculaDTO);

           if (!_plRepo.UpdatePelicula(peli))
            {
                ModelState.AddModelError("", "No se pudo actualizar la información de la película.");
                return StatusCode(500, ModelState);
            }
            else
            {
                return NoContent();
            }

        }

        /// <summary>
        /// Elimina una Película.
        /// </summary>
        /// <param name="Id">ID de la Película</param>
        /// <param name="PeliculaDTO"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}", Name = "EliminarPelicula")]
        public IActionResult EliminarPelicula(int Id)
        {
            if (!_plRepo.ExistsPelicula(Id))
            {
                return NotFound();
            }
            
            if (!_plRepo.DeletePelicula(Id))
            {
                ModelState.AddModelError("", "No se pudo eliminar la película.");
                return StatusCode(500, ModelState);
            }
            else
            {
                return NoContent();
            }

        }
    }
}
