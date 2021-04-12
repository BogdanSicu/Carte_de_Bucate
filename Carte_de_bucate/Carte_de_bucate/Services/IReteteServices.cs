using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Carte_de_bucate.Services
{
    public interface IReteteServices
    {
        IEnumerable Get_all_food();
        IEnumerable Get_all_food_by_ingredient_id(int id);
        IEnumerable Get_all_food_by_time(int timp);
        MancareReadDTO Get_food_by_id(int id);
        int Create_Reteta(MancareAddDTO mancareAddDTO);
        bool SaveChanges();
        int UpdateMancare(int id, MancareUpdateDTO mancareUpdate);
        int Delete_mancare(int id);
        Mancare Patch_get_food_id(int id);
    }
}
