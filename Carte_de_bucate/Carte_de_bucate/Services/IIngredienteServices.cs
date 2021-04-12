using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.Services
{
    public interface IIngredienteServices
    {
        int Create_Ingredient(IngredientAddDTO ingredient);
        IEnumerable Get_all_ingrediente(int id);
        bool SaveChanges();
    }
}
