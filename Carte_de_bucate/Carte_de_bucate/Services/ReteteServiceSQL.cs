using AutoMapper;
using Carte_de_bucate.Data;
using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace Carte_de_bucate.Services
{
    public class ReteteServiceSQL : IReteteServices
    {
        private readonly ReteteContext _context;
        private readonly ITariServices _tariRepository;
        private readonly IMapper _mapper;
        public ReteteServiceSQL(ReteteContext context, ITariServices repositoryTari, IMapper mapper)
        {
            _context = context;
            _tariRepository = repositoryTari;
            _mapper = mapper;
        }

        public int Create_Reteta(MancareAddDTO mancareAddDTO)
        {
            if (mancareAddDTO == null) {
                return 1;
            }

            var tara = _tariRepository.Get_tara_by_id(mancareAddDTO.TaraID);
            if (tara == null)
            {
                return 2;
            }

            var mancareNew = new Mancare {
                NameFood = mancareAddDTO.NameFood,
                TaraID = mancareAddDTO.TaraID,
                TimpPreparare = mancareAddDTO.TimpPreparare,
                ModPreparare = mancareAddDTO.ModPreparare
            };
            
            _context.Mancare.Add(mancareNew);
            _context.SaveChanges();

            return 3;
        }

        public IEnumerable Get_all_food()
        {
            var firstMancare= _context.Mancare.Include(x=>x.Tara)
                .Include(t=>t.ListaIngrediente).ThenInclude(x=>x.Ingredient).ToList();

            if (firstMancare == null)
                return null;

            var groupingList = firstMancare.GroupBy(x => x.Tara.Id).ToList();

            var finalList = new List<List<MancareReadDTO>>();
            foreach (var groupingElement in groupingList)
            {

                var mancareList = new List<MancareReadDTO>();

                foreach (var element in groupingElement)
                {
                    var mancareResult = new MancareReadDTO
                    {
                        Id = element.Id,
                        ModPreparare = element.ModPreparare,
                        NameFood = element.NameFood,
                        TimpPreparare = element.TimpPreparare,
                        Tara = new TaraMancareDTO()
                    };
                    mancareResult.Tara.Id = element.Tara.Id;
                    mancareResult.Tara.Denumire_tara = element.Tara.DenumireTara;
                    mancareResult.ListaIngrediente = new List<IngredienteMancareDTO>();

                    foreach (var theElement in element.ListaIngrediente)
                    {
                        var x = new IngredienteMancareDTO
                        {
                            Denumire = theElement.Ingredient.Denumire,
                            Id = theElement.Ingredient.Id
                        };
                        mancareResult.ListaIngrediente.Add(x);
                    }
                    mancareList.Add(mancareResult);
                }
                finalList.Add(mancareList);
            }
            //var resultOK = result.GroupBy(x => x.Tara.Id).Select(x=>new { 
            //Id = x.ToList().FirstOrDefault().Id
            //}).ToList();

           return finalList;
        }

        public MancareReadDTO Get_food_by_id(int id)
        {
            if (id < 1)
            {
                return null;
            }

            var mancare = _context.Mancare.Include(x => x.Tara).Include(x => x.ListaIngrediente).
                ThenInclude(x => x.Ingredient).FirstOrDefault(idul => idul.Id == id);

            if (mancare != null)
            {
                var mancareRead = new MancareReadDTO
                {
                    Id = mancare.Id,
                    ModPreparare = mancare.ModPreparare,
                    NameFood = mancare.NameFood,
                    TimpPreparare = mancare.TimpPreparare,
                    Tara = new TaraMancareDTO()
                };
                mancareRead.Tara.Id = mancare.Tara.Id;
                mancareRead.Tara.Denumire_tara = mancare.Tara.DenumireTara;
                mancareRead.ListaIngrediente = new List<IngredienteMancareDTO>();
                foreach (var ingredient in mancare.ListaIngrediente)
                {
                    var x = new IngredienteMancareDTO
                    {
                        Denumire = ingredient.Ingredient.Denumire,
                        Id = ingredient.Ingredient.Id
                    };
                    mancareRead.ListaIngrediente.Add(x);
                }

                return mancareRead;
            }
            else return null;
        }

        public int Delete_mancare(int id)
        {

            if (id < 1)
            {
                return 1;
            }

            var mancare = _context.Mancare.FirstOrDefault(idul => idul.Id == id);

            if(mancare==null)
            {
                return 1;
            }    
            _context.Remove(mancare);
            _context.SaveChanges();
            return 2;
        }

        public bool SaveChanges()
        {
            //daca se salveaza schimbarile,
            //o sa fie mai mare decat 0, deci return TRUE
            return (_context.SaveChanges() >= 0);
        }

        public int UpdateMancare(int id, MancareUpdateDTO mancareUpdate)
        {
            if (id < 1)
            {
                return 1;
            }

            var mancareNew = _context.Mancare.Include(x => x.Tara).Include(x => x.ListaIngrediente).
                ThenInclude(x => x.Ingredient).FirstOrDefault(idul => idul.Id == id);

            if (mancareNew == null)
            {
                return 2;
            }

            var tara = _tariRepository.Get_tara_by_id(mancareUpdate.TaraID);
            if (tara == null)
            {
                return 2;
            }

            mancareNew.ModPreparare = mancareUpdate.ModPreparare;
            mancareNew.NameFood = mancareUpdate.NameFood;
            mancareNew.TaraID = mancareUpdate.TaraID;
            mancareNew.TimpPreparare = mancareUpdate.TimpPreparare;

            _context.SaveChanges();

            return 3;
        }

        public IEnumerable Get_all_food_by_ingredient_id(int id)
        {
            if (id < 1)
            {
                return null;
            }

            var firstMancare = _context.Mancare.Include(x => x.Tara)
                .Include(t => t.ListaIngrediente).ThenInclude(x => x.Ingredient).ToList();

            var selectedElements = firstMancare.Where(mancare => mancare.ListaIngrediente.Any() &&
            mancare.ListaIngrediente.Where(ingredient => ingredient.IngrediendID == id).Count() > 0 &&
            mancare.ListaIngrediente.FirstOrDefault(ingredient => ingredient.IngrediendID == id).IngrediendID > 0).ToList();

            //(from element in firstMancare
            // where element.ListaIngrediente.Count() > 0 && element.ListaIngrediente.FirstOrDefault(x => x.IngrediendID == id).IngrediendID > 0
            // select element).ToList();

            var finalList = new List<MancareReadDTO>();

            foreach (var element in selectedElements)
            {
                var mancareRead = new MancareReadDTO
                {
                    Id = element.Id,
                    ModPreparare = element.ModPreparare,
                    NameFood = element.NameFood,
                    TimpPreparare = element.TimpPreparare,
                    Tara = new TaraMancareDTO()
                };
                mancareRead.Tara.Id = element.Tara.Id;
                mancareRead.Tara.Denumire_tara = element.Tara.DenumireTara;
                mancareRead.ListaIngrediente = new List<IngredienteMancareDTO>();

                foreach (var ingredient in element.ListaIngrediente)
                {
                    var x = new IngredienteMancareDTO
                    {
                        Denumire = ingredient.Ingredient.Denumire,
                        Id = ingredient.Ingredient.Id
                    };
                    mancareRead.ListaIngrediente.Add(x);
                }
                finalList.Add(mancareRead);
            }

            return finalList;
        }

        public IEnumerable Get_all_food_by_time(int timp)
        {
            if (timp > 5)
            {
                var firstMancare = _context.Mancare.Include(x => x.Tara)
                                 .Include(t => t.ListaIngrediente).ThenInclude(x => x.Ingredient).ToList();

                var selectedElements = firstMancare.AsQueryable().Where(mancare => mancare.TimpPreparare < timp).ToList();

                //(from element in firstMancare
                //           where element.TimpPreparare < timp
                //           select element).ToList();

                var finalList = new List<MancareReadDTO>();

                foreach (var element in selectedElements)
                {
                    var mancareRead = new MancareReadDTO
                    {
                        Id = element.Id,
                        ModPreparare = element.ModPreparare,
                        NameFood = element.NameFood,
                        TimpPreparare = element.TimpPreparare,
                        Tara = new TaraMancareDTO()
                    };

                    mancareRead.Tara.Id = element.Tara.Id;
                    mancareRead.Tara.Denumire_tara = element.Tara.DenumireTara;
                    mancareRead.ListaIngrediente = new List<IngredienteMancareDTO>();

                    foreach (var ingredient in element.ListaIngrediente)
                    {
                        var x = new IngredienteMancareDTO
                        {
                            Denumire = ingredient.Ingredient.Denumire,
                            Id = ingredient.Ingredient.Id
                        };
                        mancareRead.ListaIngrediente.Add(x);
                    }
                    finalList.Add(mancareRead);
                }

                return finalList;
            }
            else return null;
        }

        public Mancare Patch_get_food_id(int id)
        {
            return _context.Mancare.Include(x => x.Tara).Include(x => x.ListaIngrediente).
                ThenInclude(x => x.Ingredient).FirstOrDefault(idul => idul.Id == id);
        }
    }
}
