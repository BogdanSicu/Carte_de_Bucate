using Carte_de_bucate.Data;
using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Carte_de_bucate.Services
{
    public class TariSerivecSQL : ITariServices
    {
        private readonly ReteteContext _context;
        public TariSerivecSQL(ReteteContext context)
        {
            _context = context;
        }

        public int Create_Tara(TaraPutPostDTO taraPostDTO)
        {
            if (taraPostDTO == null)
            {
                return 1;
            }

            //pana nu apelam SaveChanges(), nu se va salva in baza de date

            var taraNew = new Tari
            {
                DenumireTara = taraPostDTO.Denumire_tara
            };

            _context.Tari.Add(taraNew);
            _context.SaveChanges();

           return 2;
        }

        public int Delete_tara(int id)
        {
            if (id < 1)
            {
                return 1;
            }

            var verificareTara = _context.Tari.Include(x => x.ListaMancaruri).FirstOrDefault(x => x.Id == id);

            if (verificareTara == null)
            {
                return 2;
            }

            _context.Remove(verificareTara);
            _context.SaveChanges();

            return 3;
        }

        public bool SaveChanges()
        {
            //daca se salveaza schimbarile,
            //o sa fie mai mare decat 0, deci return TRUE
            return (_context.SaveChanges() >= 0);
        }
        public IEnumerable Get_all_tari()
        {
            var listTari = _context.Tari.Include(x => x.ListaMancaruri).ToList();
            
            if (listTari != null)
            {
                var finalList = listTari.Where(y => y.Id == y.Id).Select(x => new TaraDTO
                {
                    DenumireTara = x.DenumireTara,
                    Id = x.Id,
                    Mancaruri = x.ListaMancaruri.Where(s => s.NameFood == s.NameFood).Select(p => new MancareTaraDTO
                    {
                        NameFood = p.NameFood
                    }).ToList()
                }).ToList();

                return finalList;
            }
            else return null;
        }

        public ActionResult<TaraDTO> Get_tara_by_id(int id)
        {
            
            if (id < 1)
            {
                return null;
            }

            var tara = _context.Tari.Include(x => x.ListaMancaruri).FirstOrDefault(x => x.Id == id);
            if (tara != null)
            {
                var taraDTO = new TaraDTO
                {
                    Id = tara.Id,
                    DenumireTara = tara.DenumireTara,
                    Mancaruri = new List<MancareTaraDTO>()
                };

                foreach (var denumireMancare in tara.ListaMancaruri)
                {
                    var numeMancare = new MancareTaraDTO
                    {
                        NameFood = denumireMancare.NameFood
                    };
                    taraDTO.Mancaruri.Add(numeMancare);
                }

               return taraDTO;
            }
            else return null;
        }

        public int Put_Tara_id(int id, TaraPutPostDTO taraPutDTO)
        {

            if (id < 1)
            {
                return 1;
            }

            var tara = _context.Tari.Include(x => x.ListaMancaruri).FirstOrDefault(x => x.Id == id);
            if (tara == null)
            {
                return 2;
            }

            tara.DenumireTara = taraPutDTO.Denumire_tara;

            _context.SaveChanges();

            return 3;
        }
    }
}
