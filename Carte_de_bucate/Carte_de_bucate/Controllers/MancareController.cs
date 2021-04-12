using AutoMapper;
using Carte_de_bucate.Services;
using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using System.IO;

namespace Carte_de_bucate.Controllers
{
    [Route("api/controller/mancare")]
    [ApiController]
    public class MancareController : Controller
    {
        private readonly IReteteServices _reteteServices;
        private readonly IMapper _mapper;

        public MancareController(IReteteServices reteteServices,
            IMapper mapper)
        {
            _reteteServices = reteteServices;
            _mapper = mapper;
        }


        [HttpGet] 
        public IActionResult Get_All_Food()
        {
            var finalList = _reteteServices.Get_all_food();
            if (finalList == null)
            {
                return NotFound();
            }

                return Ok(finalList);
        }

        [HttpGet("ingredient/{id}")]
        public IActionResult Get_all_food_by_ingredient_id(int id)
        {
            var finalList = _reteteServices.Get_all_food_by_ingredient_id(id);
            if (finalList == null)
            {
                return NotFound();
            }

            return Ok(finalList);
        }

        [HttpGet("timp/{timp}")]
        public IActionResult Get_all_food_by_time (int timp)
        {
            var finalList = _reteteServices.Get_all_food_by_time(timp);
            if (finalList == null)
            {
                return NotFound();
            }

                return Ok(finalList);
        }

        [HttpGet("{id}")]
        public IActionResult Get_food_by_id(int id)
        {
            var finalList = _reteteServices.Get_food_by_id(id);
            if (finalList == null)
            {
                return NotFound();
            }

            return Ok(finalList);

        }

        //returneaza Mancare_Read_DTO pentru ca
        //ii arata clientului o reprezentare a ceea ce a trimis 
        //catre server si ce a salvat in baza de date
        [HttpPost] 
        public IActionResult Create_Reteta(MancareAddDTO mancareAddDTO)
        {
            var response = _reteteServices.Create_Reteta(mancareAddDTO);
            if (response == 1)
            {
                return BadRequest();
            }
            if(response==2)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}")] 
        public IActionResult Put_Mancare(int id, MancareUpdateDTO mancareUpdate)
        {

            var response = _reteteServices.UpdateMancare(id, mancareUpdate);
            if (response == 1)
            {
                return BadRequest();
            }
            if (response == 2)
            {
                return NotFound();
            }
            else return Ok();
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete_Mancare(int id)
        {
            var response = _reteteServices.Delete_mancare(id);
            if (response == 1)
            {
                return NotFound();
            }
            return Ok();
        }

        //[HttpPatch("{id}")]
        //public IActionResult Patch_mancare_mod_preparare(int id, MancarePatchModPreparare modPreparare)
        //{
        //    if (id < 1)
        //    {
        //        return BadRequest("The ID can't be less than 1");
        //    }

        //    var mancare = _repositoryMancare.Get_food_by_id(id);
        //    if (mancare == null)
        //    {
        //        return NotFound();
        //    }

        //    mancare.ModPreparare = modPreparare.ModPreparare;

        //    _repositoryMancare.SaveChanges();

        //    return Ok();
        //}


        //multa forta, multa valoare
        [HttpPatch("{id}")]
        public IActionResult Patch_mancare_mod_preparare(int id, [FromBody] JsonPatchDocument<MancarePatchModPreparare> modPreparare)
        {

            if (id < 1)
            {
                return BadRequest("The ID can't be less than 1");
            }

            if (modPreparare == null)
            {
                return BadRequest("Empty parametres");
            }

            if(modPreparare.Operations.FirstOrDefault(x=> x.path == "/ModPreparare") == null)
            {
                return BadRequest("The path is not good");
            }


            var mancare = _reteteServices.Patch_get_food_id(id);
            if (mancare == null)
            {
                return NotFound();
            }

            //mancare.ModPreparare = modPreparare.ModPreparare;

            var patch = _mapper.Map<MancarePatchModPreparare>(mancare);

            modPreparare.ApplyTo(patch, ModelState);

            _mapper.Map(patch, mancare);

            var nush =_reteteServices.SaveChanges();

            return Ok();
        }

    }
}
