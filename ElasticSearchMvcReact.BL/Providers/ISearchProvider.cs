using System.Collections.Generic;
using ElasticSearchMvcReact.BL.DomainModel;
using ElasticSearchMvcReact.BL.QueryModels;

namespace ElasticSearchMvcReact.BL.Providers
{
    public interface ISearchProvider<T>
    {
        List<string> Suggest(string index,string text);
        void CreateIndexWithMapping(string index, string type);
        void CreateDoc(string index, string type,T docType);
        QuerySearchResponse<T> MatchAll(string index, string type);
        void UpdateAnimal(Animal animal);
        IEnumerable<Animal> GetAnimalsByTerms(SearchModel searchModel);
        void DeleteById(int id);
        void DeleteIndex(string index);
    }
}
