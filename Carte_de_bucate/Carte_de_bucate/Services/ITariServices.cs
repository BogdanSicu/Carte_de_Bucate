using Carte_de_bucate.DTOs;
using Carte_de_bucate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;

namespace Carte_de_bucate.Services
{
    public interface ITariServices
    {
        IEnumerable Get_all_tari();
        ActionResult<TaraDTO> Get_tara_by_id(int id);
        int Put_Tara_id(int id, TaraPutPostDTO taraPutDTO);
        int Create_Tara(TaraPutPostDTO tara);
        int Delete_tara(int id);
        bool SaveChanges();
    }
}
