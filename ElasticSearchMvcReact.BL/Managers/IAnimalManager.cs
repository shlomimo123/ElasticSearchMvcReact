using System.Collections.Generic;
using ElasticSearchMvcReact.BL.DomainModel;
using ElasticSearchMvcReact.BL.QueryModels;

namespace ElasticSearchMvcReact.BL.Managers
{
    public interface IAnimalManager
    {
        void CreateIndexWithMapping();
        List<string> Suggest(string text);

        QuerySearchResponse<Animal> GetAnimals();

        IEnumerable<Animal> GetAnimalsByTerms(SearchModel searchModel);

        Animal GetAnimal(int id);
        void UpdateAnimal(Animal value);
        void DeleteAnimal(int id);
        void CreateAnimal(Animal value);
        void DeleteIndex(string index);
    }
}
