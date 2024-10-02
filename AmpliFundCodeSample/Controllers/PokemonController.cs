using AmpliFundCodeSample.Models;
using AmpliFundCodeSample.Repositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmpliFundCodeSample.Controllers
{
    public class PokemonController : ApiController
    {
        private readonly PokemonRepo pokemonRepo = new PokemonRepo();

        [HttpGet]
        public HttpResponseMessage GetPokemon(int? pokeDexNum = null, string name = null)
        {
            var pokemon = pokemonRepo.GetPokemon(pokeDexNum, name);

            return Request.CreateResponse(HttpStatusCode.OK, pokemon);
        }

        [HttpPost]
        public HttpResponseMessage CreatePokemon(Pokemon pokemon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // I would add more validation such as making the execute functions returns 1 (a record is created), duplicate field checks, etc.
                    pokemonRepo.CreatePokemon(pokemon);

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }

            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred. Please contact your admin.");
            }
        }

        [HttpPatch]
        public HttpResponseMessage UpdatePokemon(Pokemon pokemon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // I would add more validation such as making sure a record can be found, duplicate field checks, etc.
                    // I would also find the record using other fields besides Id.
                    pokemonRepo.UpdatePokemon(pokemon);

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }

            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred. Please contact your admin.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeletePokemon(int pokemonId)
        {
            try
            {
                // I would validation that checks that a record can be found.
                pokemonRepo.DeletePokemon(pokemonId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred. Please contact your admin.");
            }
        }
    }
}
