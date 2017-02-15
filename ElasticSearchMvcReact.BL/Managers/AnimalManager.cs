using System.Collections.Generic;
using ElasticSearchMvcReact.BL.Attributes;
using ElasticSearchMvcReact.BL.DomainModel;
using ElasticSearchMvcReact.BL.Providers;
using ElasticSearchMvcReact.BL.QueryModels;

namespace ElasticSearchMvcReact.BL.Managers
{
    [TransientLifetime]
    public class AnimalManager : IAnimalManager
    {
        private readonly ISearchProvider<Animal> _searchProvider;

        public AnimalManager(ISearchProvider<Animal> searchProvider)
        {
            _searchProvider = searchProvider;
        }

        public List<string> Suggest(string text)
        {
            return _searchProvider.Suggest(Animal.SearchIndex, text);
        }

        public void CreateIndexWithMapping()
        {
            _searchProvider.CreateIndexWithMapping(Animal.SearchIndex, Animal.SearchType);
        }

        public void CreateAnimal(Animal value)
        {
            _searchProvider.CreateDoc(Animal.SearchIndex, Animal.SearchType, value);
        }

        public QuerySearchResponse<Animal> GetAnimals()
        {
            return _searchProvider.MatchAll(Animal.SearchIndex, Animal.SearchType);
        }

        public IEnumerable<Animal> GetAnimalsByTerms(SearchModel searchModel)
        {
            return _searchProvider.GetAnimalsByTerms(searchModel);
        }

        public Animal GetAnimal(int id)
        {
            return new Animal { AnimalType = "Dog", Id = 1 };
        }

        public void UpdateAnimal(Animal value)
        {
            _searchProvider.UpdateAnimal(value);
        }

        public void DeleteAnimal(int id)
        {
            _searchProvider.DeleteById(id);
        }

       

        public void DeleteIndex(string index)
        {
            _searchProvider.DeleteIndex(index);
        }
    }
}
