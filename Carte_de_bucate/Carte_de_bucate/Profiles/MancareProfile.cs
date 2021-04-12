using AutoMapper;
using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;

namespace Carte_de_bucate.Profiles
{
    //asta este pentru auto-mapper
    public class MancareProfile : Profile
    {
        public MancareProfile()
        {
            //mapeaza din Mancare in Mancare_Read_DTO
            CreateMap<Mancare, MancareReadDTO>();

            //mapeaza din mancare_add_dto in mancare
            CreateMap<MancareAddDTO, Mancare>();

            CreateMap<MancareUpdateDTO, Mancare>();

            CreateMap<TaraPutPostDTO, Tari>();
            CreateMap<Tari, TaraPutPostDTO>();

            CreateMap<IngredientAddDTO, Ingrediente>();

            //asta este pentru PATCH
            CreateMap<Mancare, MancarePatchModPreparare>();

            CreateMap<MancarePatchModPreparare, Mancare>();

        }
    }
}
