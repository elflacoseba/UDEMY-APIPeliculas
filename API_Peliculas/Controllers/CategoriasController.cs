using API_Peliculas.Models;
using API_Peliculas.Models.Dtos;
using API_Peliculas.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();

            var listaCategoriasDTO = new List<CategoriaDTO>();

            foreach (var cat in listaCategorias)
            {
                listaCategoriasDTO.Add(_mapper.Map<CategoriaDTO>(cat));
            }

            return Ok(listaCategoriasDTO);
        }
        
        [HttpGet("{Id:int}", Name = "GetGategoria")]
        public IActionResult GetGategoria(int Id)
        {
           Categoria cat = _ctRepo.GetCategoria(Id);

            if (cat != null)
            {
                CategoriaDTO catDTO =_mapper.Map<CategoriaDTO>(cat);  

                return Ok(catDTO);  
            }
            else
            {
                return NotFound();
            }
        }
    }
}
