using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Peliculas.Models;
using API_Peliculas.Models.Dtos;
using AutoMapper;

namespace API_Peliculas.PeliculasMapper
{
    public class PeliculasMapper : Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<Pelicula, PeliculaCreateDTO>().ReverseMap();
            CreateMap<Pelicula, PeliculaUpdateDTO>().ReverseMap();
        }
    }
}
