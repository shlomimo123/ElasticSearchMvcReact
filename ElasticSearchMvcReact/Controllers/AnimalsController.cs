using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using ElasticSearchMvcReact.BL.DomainModel;
using ElasticSearchMvcReact.BL.Managers;
using ElasticSearchMvcReact.BL.Providers;
using WebAPIRestWithNest.Filters;
using ElasticSearchMvcReact.BL.QueryModels;

namespace WebAPIRestWithNest.Controllers
{
    [RoutePrefix("api/animals")]
    [AnimalExceptionFilter]
    public class AnimalsController : ApiController
    {
        private readonly IAnimalManager _animalManager;

        public AnimalsController(IAnimalManager animalManager)
        {
            _animalManager = animalManager;
        }

        // GET api/animals
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_animalManager.GetAnimals());
        }

        [HttpGet]
        [Route("{id}")]
        public Animal Get(int id)
        {
            return _animalManager.GetAnimal(id);
           
        }

        [HttpPost]
        [Route("Suggest")]
        public IHttpActionResult Suggest(SearchModel searchModel)
        {
            return Ok(_animalManager.Suggest(searchModel.Value));
        }

        // POST api/animals
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Animal value)
        {
            _animalManager.CreateAnimal(value);
            // could set the Id here in the retrun content
            return Created<Animal>(Request.RequestUri, value );
        }

        // PUT api/animals/5
        [HttpPut]
        [HttpPatch]
        [Route("")]
        public void Put([FromBody]Animal value)
        {
            _animalManager.UpdateAnimal(value);
        }

        [HttpGet]
        [HttpPatch]
        [Route("CreateIndexWithMapping")]
        public void CreateIndexWithMapping()
        {
            _animalManager.CreateIndexWithMapping();
        }


        // DELETE api/animals/5
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            _animalManager.DeleteAnimal(id);
        }

        // DELETE api/animals/5
        [HttpDelete]
        [Route("deleteIndex/{index}")]
        public void DeleteIndex(string index)
        {
            //_animalManager.DeleteIndex("outofprocessslab-2014.04.11");

            _animalManager.DeleteIndex(index);
        }
    }
}
