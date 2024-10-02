using AmpliFundCodeSample.Models;
using AmpliFundCodeSample.Repositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmpliFundCodeSample.Controllers
{
    public class PokemonTypeController : ApiController
    {
        private readonly PokemonTypeRepo pokemonTypeRepo = new PokemonTypeRepo();

        [HttpGet]
        public HttpResponseMessage GetTypes(int? id = null, string name = "")
        {
            var types = pokemonTypeRepo.GetTypes(id, name);

            return Request.CreateResponse(HttpStatusCode.OK, types);
        }

        [HttpPost]
        public HttpResponseMessage CreateType(PokemonType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // I would add more validation such as making the execute functions returns 1 (a record is created), duplicate field checks, etc.
                    pokemonTypeRepo.CreateType(type);

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
        public HttpResponseMessage UpdateType(PokemonType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // I would add more validation such as making sure a record can be found, duplicate field checks, etc.
                    // I would also find the record using other fields besides Id.
                    pokemonTypeRepo.UpdateType(type);

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
        public HttpResponseMessage DeleteType(int pokemonTypeId)
        {
            try
            {
                // I would validation that checks that a record can be found.
                pokemonTypeRepo.DeleteType(pokemonTypeId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred. Please contact your admin.");
            }
        }
    }
}