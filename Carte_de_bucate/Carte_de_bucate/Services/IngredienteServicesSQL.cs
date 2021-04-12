using Carte_de_bucate.Data;
using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.Services
{
    public class IngredienteServicesSQL : IIngredienteServices
    {

        private readonly ReteteContext _context;
        public IngredienteServicesSQL(ReteteContext context)
        {
            _context = context;
        }

        public int Create_Ingredient(IngredientAddDTO ingredient)
        {

            if (ingredient == null)
                return 1;

            var ingredientNew = new Ingrediente
            {
                Denumire = ingredient.Denumire
            };
          
            _context.Ingrediente.Add(ingredientNew);
            _context.SaveChanges();

            return 2;
        }

        public IEnumerable Get_all_ingrediente(int id)
        {
            if (id < 1)
            {
                return null;
            }

            var listaIngrediente =  _context.Ingrediente.Include(x => x.ListaRetete).ThenInclude(x => x.Mancare).ToList();


            var verificareMancare = _context.Ingrediente_In_Retete.ToList();

            var verificare2 = verificareMancare.AsQueryable().Where(element => element.MancareID == id).ToList();

            //(from element in verificareMancare
            //             where element.MancareID == id
            //               select element).ToList();

            if (verificare2.Count() < 1)
            {
                return null;
            }


            var finalList = new List<IngredientReadDTO>();

            foreach (var ingredient in listaIngrediente)
            {
                int ok = 0;

                var ingredient_dto = new IngredientReadDTO
                {
                    Denumire = ingredient.Denumire,
                    Id = ingredient.Id,
                    ListaMancaruri = new List<MancareAddDTO>()
                };

                foreach (var mancare in ingredient.ListaRetete)
                {
                    if (mancare.MancareID == id)
                        ok = 1;

                    var mancareAddDTO = new MancareAddDTO
                    {
                        NameFood = mancare.Mancare.NameFood,
                        ModPreparare = mancare.Mancare.ModPreparare,
                        TaraID = mancare.Mancare.TaraID,
                        TimpPreparare = mancare.Mancare.TimpPreparare
                    };

                    ingredient_dto.ListaMancaruri.Add(mancareAddDTO);
                }

                if (ok == 1)
                {
                    finalList.Add(ingredient_dto);
                }

            }

            //var finalList = listaIngrediente.AsQueryable().Where(ingredient => ingredient.ListaRetete.Count() > 0 && 
            //ingredient.ListaRetete.FirstOrDefault(mancare => mancare.MancareID == id).MancareID > 0).Select(ingredientDTO => new IngredientReadDTO
            //{
            //    Denumire = ingredientDTO.Denumire,
            //    Id = ingredientDTO.Id,
            //    ListaMancaruri = ingredientDTO.ListaRetete.Where(s => s.IngrediendID == s.IngrediendID).Select(p => new MancareAddDTO
            //    {
            //        NameFood = p.Mancare.NameFood,
            //        ModPreparare = p.Mancare.ModPreparare,
            //        TaraID = p.Mancare.TaraID,
            //        TimpPreparare = p.Mancare.TimpPreparare
            //    }).ToList()
            //}).ToList();

            return finalList;
        }

        public bool SaveChanges()
        {
            //daca se salveaza schimbarile,
            //o sa fie mai mare decat 0, deci return TRUE
            return (_context.SaveChanges() >= 0);
        }
    }
}
