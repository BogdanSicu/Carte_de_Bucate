using AutoMapper;
using Carte_de_bucate.Services;
using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Http.Description;

namespace Carte_de_bucate.Controllers
{
    [Route("api/controller/tara")]
    [ApiController]
    public class TaraController : Controller
    {

        private readonly ITariServices _taraServices;

        public TaraController(ITariServices taraServices)
        {
            _taraServices = taraServices;
        }

        [HttpGet]
        public IActionResult Get_tari()
        {
            var listTari = _taraServices.Get_all_tari();
            if (listTari == null)
                return BadRequest();
            else return Ok(listTari);
        }


        [HttpGet("{id}")]
        public IActionResult Get_tara_id(int id)
        {
            var tara = _taraServices.Get_tara_by_id(id);
            if (tara == null)
            {
                return BadRequest();
            }
            else return Ok(tara);
        }

        [HttpPut("{id}")]
        public IActionResult Put_Tara(int id, TaraPutPostDTO taraPutDTO)
        {
            var response = _taraServices.Put_Tara_id(id, taraPutDTO);
            if (response == 1)
            {
                return BadRequest("Invalid id");
            }
            if (response == 2)
            {
                return NotFound();
            }
            else return Ok();
        }

        [HttpPost]
        public IActionResult Post_Tara(TaraPutPostDTO taraPostDTO)
        {
            var response = _taraServices.Create_Tara(taraPostDTO);
            if (response == 1)
            {
                return BadRequest();
            }
            
            else return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete_tara(int id)
        {
            var response = _taraServices.Delete_tara(id);
            if (response == 1)
            {
                return BadRequest("Invalid id");
            }
            if (response == 2)
            {
                return NotFound();
            }
            else return Ok();
        }

    }
}
