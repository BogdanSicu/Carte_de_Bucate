using AutoMapper;
using Carte_de_bucate.Services;
using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Carte_de_bucate.Controllers
{
    [Route("api/controller/ingredient")]
    [ApiController]
    public class IngredientController:Controller
    {
        private readonly IIngredienteServices _ingredienteServices;

        public IngredientController(IIngredienteServices ingredienteServices)
        {
            _ingredienteServices = ingredienteServices;
        }

        [HttpGet("reteta/{id}")]
        public IActionResult Get_all_ingrediente(int id)
        {
            var finalList = _ingredienteServices.Get_all_ingrediente(id);

            if (finalList == null)
            {
                return NotFound();
            }

            return Ok(finalList);
        }

        [HttpPost]
        public IActionResult Post_Ingredient(IngredientAddDTO ingredient)
        {
            var response = _ingredienteServices.Create_Ingredient(ingredient);

            if (response == 1)
            {
                return BadRequest();
            }
            else return Ok();

          
        }
    }
}
