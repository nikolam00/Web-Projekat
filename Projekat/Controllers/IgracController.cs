using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using Models;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IgracController : ControllerBase
    {
        public Context Context { get; set; }

        public IgracController(Context context)
        {
            Context = context;
        }

        [Route("Unos igraca/{FideId}/{ime}/{prezime}/{datum_rodjenja}/{klub}")]
        [HttpPost]
        public async Task<ActionResult> Dodaj_igraca(int FideId,  string ime, string prezime, DateTime datum_rodjenja, int Rating, Titula title, Klub klub)
        {
            if (ime == "") return BadRequest("Morate uneti ime igraca");
            if (ime.Length > 20) return BadRequest("Pogresna duzina!");

            if (prezime == "") return BadRequest("Morate uneti ime igraca");
            if (prezime.Length > 20) return BadRequest("Pogresna duzina!");

            if(FideId<0||FideId>999999) return BadRequest("Pogresan FideId!");

            if (datum_rodjenja.Year < 1940) return BadRequest("Pogrsan datum rodjenja!");

            if (Rating < 1200 || Rating > 3000) return BadRequest("Pogresna vrednost za rejting!");

            Igrac player = new Igrac();

            player.FideId=FideId;
            player.Ime = ime;
            player.Prezime = prezime;
            player.Datum_rodjenja = datum_rodjenja;
            player.Rejting = Rating;
            player.Titula = title;
            player.Klub = klub;

            try
            {
                Context.Igraci.Add(player);
                await Context.SaveChangesAsync();
                return Ok("Hotel je dodat u bazu");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Obrisi igraca/{Fide Id}")]
        [HttpDelete]
        /*public async Task<ActionResult> Izbrisi_igraca(int FideId)
        {
            if(ime.Length>70) return BadRequest("Ime predugacko da bi hotel postojao u bazi!");

            try
            {
                var hotel= Context.Hoteli.Where(p => p.Naziv==ime).FirstOrDefault();
                if(hotel!=null)
                {
                        Context.Hoteli.Remove(hotel);
                        await Context.SaveChangesAsync();
                        return Ok($"Hotel {ime} je obrisan");
                }
                else
                {
                    return Ok("Takav hotel nije ni postojao u bazi!");
                }

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

    }
}