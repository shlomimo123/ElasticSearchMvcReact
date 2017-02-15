using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ElasticSearchMvcReact.BL.Attributes;
using ElasticSearchMvcReact.BL.DomainModel;
using ElasticSearchMvcReact.BL.Providers;
using Newtonsoft.Json;
using Elasticsearch.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using ElasticSearchMvcReact.BL.ElasticSearchAttributes;
using ElasticSearchMvcReact.BL;
using ElasticSearchMvcReact.BL.QueryModels;

namespace ElasticSearchMvcReact.BL.Providers
{
    [TransientLifetime]
    public class SearchProvider<T> : ISearchProvider<T>
    {
        private readonly ElasticLowLevelClient _elasticsearchClient;

        public SearchProvider()
        {
            var node = new Uri("http://localhost:9200");
            var connectionPool = new SniffingConnectionPool(new[] { node });
            var config = new ConnectionConfiguration(connectionPool);
            _elasticsearchClient = new ElasticLowLevelClient(config);
        }
        public List<string> Suggest(string index, string text)
        {
            List<string> suggestions = new List<string>();
            List<string> fieldsToWorkOn = new List<string>();
            List<string> suggestionNames = new List<string>();

            Type t = typeof(T);

            foreach (var m in t.GetProperties())
            {
                if (m.GetCustomAttributes(typeof(Suggest), true).Length > 0)
                {
                    fieldsToWorkOn.Add(m.Name);
                }
            }

            JObject query = new JObject();
            JProperty propTextToSuggest = new JProperty("text", text);
            query.Add(propTextToSuggest);

            foreach (string fieldName in fieldsToWorkOn)
            {
                JObject termObject = new JObject();

                JProperty field = new JProperty("field", fieldName);
                termObject.Add(field);

                JObject suggest = new JObject();
                suggest.Add("term", termObject);

                string suggestName = $"my-suggestion-{Guid.NewGuid().ToString()}";
                suggestionNames.Add(suggestName);
                query.Add(suggestName, suggest);
            }

            string json = JsonConvert.SerializeObject(query);
            PostData<object> jsonPostData = new PostData<object>(json);
            ElasticsearchResponse<object> results = _elasticsearchClient.Suggest<object>(index, jsonPostData);
            JObject result = JObject.Parse(results.Body.ToString());

            foreach (string suggestionName in suggestionNames)
            {
                MySuggestion[] suggestion = JsonConvert.DeserializeObject<MySuggestion[]>(result[suggestionName].ToString());
                List<KeyValuePair<string, List<string>>> options = suggestion.Select(x => new KeyValuePair<string, List<string>>(x.text, x.options.Select(y => y.text).ToList())).ToList();

                if (options.Sum(x => x.Value.Count()) > 0) // there is options to suggest
                {
                    suggestions.AddRange(GetCombos(options));
                }
            }

            return suggestions.Distinct().ToList();
        }
        
        public void CreateIndexWithMapping(string indexName, string typeName)
        {
            Type t = typeof(T);

            JObject jsonProperties = new JObject();
            foreach (var m in t.GetProperties())
            {
                JObject prop = new JObject();

                JProperty propFieldType;
                if (m.GetCustomAttributes(typeof(FieldType), true).Length > 0)
                {
                    propFieldType = new JProperty("type", ((FieldType)m.GetCustomAttributes(typeof(FieldType), true)[0]).ConvertFieldTypeToElasticFieldType());
                }
                else
                {
                    propFieldType = new JProperty("type", "string");
                }
                prop.Add(propFieldType);

                JProperty propNotAnalyzed;
                if (m.GetCustomAttributes(typeof(NotAnalyzed), true).Length > 0)
                {
                    propNotAnalyzed = new JProperty("index", "not_analyzed");
                    prop.Add(propNotAnalyzed);
                }

                JProperty propAnalyzer;
                if (m.GetCustomAttributes(typeof(Analyzer), true).Length > 0)
                {
                    propAnalyzer = new JProperty("analyzer", ((Analyzer)m.GetCustomAttributes(typeof(Analyzer), true)[0]).ConvertAnalyzerTypeToElasticAnalyzer());
                    prop.Add(propAnalyzer);
                }

                jsonProperties.Add(m.Name, prop);
            }

            JObject jsonType = new JObject();
            jsonType.Add("properties", jsonProperties);

            JObject jsonMappings = new JObject();
            jsonMappings.Add(typeName, jsonType);

            JObject jsonQuery = new JObject();
            jsonQuery.Add("mappings", jsonMappings);

            string json = JsonConvert.SerializeObject(jsonQuery);
            PostData<object> jsonPostData = new PostData<object>(json);
            var results = _elasticsearchClient.IndicesCreate<object>(indexName, jsonPostData);
        }

        public void CreateDoc(string index, string type, T docType)
        {
            string json = JsonConvert.SerializeObject(docType);
            PostData<object> jsonPostData = new PostData<object>(json);
            var results = _elasticsearchClient.Index<object>(index, type, jsonPostData);
        }

        public QuerySearchResponse<T> MatchAll(string index, string type)
        {
            string json = "{\"query\": { \"match_all\": {} }}";

            PostData<object> jsonPostData = new PostData<object>(json);
            ElasticsearchResponse<QuerySearchResponse<T>> result = _elasticsearchClient.Search<QuerySearchResponse<T>>(index, type, jsonPostData);

            return result.Body;
        }

        private void ValidateIfIdIsAlreadyUsedForIndex(string id)
        {
            //var idsList = new List<string> { id };
            //var result = _elasticsearchClient.Search<Animal>(s => s
            //    .Index("animals")
            //    .AllTypes()
            //    .Query(q => q.Term(p => p.Id, id)));
            //if (result.Documents.Any()) throw new ArgumentException("Id already exists in store");
        }

        public void UpdateAnimal(Animal animal)
        {

            //var index = _elasticsearchClient.Index(animal, i => i
            //    .Index("Animal.SearchIndex")
            //    .Type("animal")
            //    .Id(animal.Id)
            //    .Ttl("1m")
            //    );

        }

        public IEnumerable<Animal> GetAnimalsByTerms(SearchModel searchModel)
        {
            return null;

            //var result = _elasticsearchClient.Search<Animal>(s => s.Index("animals").AllTypes()
            //.Query(q => q.QueryString(f => f.Query(searchModel.Value + "*"))));
            //return result.Documents.ToList();
        }

        public void DeleteById(int id)
        {
            //_elasticsearchClient.Delete<Animal>(id);
        }

        public void DeleteIndex(string index)
        {
            //_elasticsearchClient.DeleteIndex(index);
        }

        public Animal GetAnimal(int id)
        {
            return null;

            //var idsList = new List<string> { id.ToString(CultureInfo.InvariantCulture) };
            //var result = _elasticsearchClient.Search<Animal>(s => s
            //    .Index("animals")
            //    .AllTypes()
            //    .Query(q => q.Term(p => p.Id, id)));

            //return result.Documents.First();
        }


        #region Helpers
        List<string> GetCombos(IEnumerable<KeyValuePair<string, List<string>>> remainingTags)
        {
            if (remainingTags.Count() == 1)
            {
                if (remainingTags.First().Value.Count() > 0)
                    return remainingTags.First().Value;
                else
                {
                    List<string> x = new List<string>();
                    x.Add(remainingTags.First().Key);
                    return x;
                }
            }
            else
            {
                var current = remainingTags.First();
                List<string> outputs = new List<string>();
                List<string> combos = GetCombos(remainingTags.Where(tag => tag.Key != current.Key));

                if(current.Value.Count==0)
                {
                    foreach (var combo in combos)
                    {
                        outputs.Add($"{current.Key} {combo}");
                    }
                }

                foreach (var tagPart in current.Value)
                {
                    foreach (var combo in combos)
                    {
                        outputs.Add($"{tagPart} {combo}");
                    }
                }
                return outputs;
            }
        } 
        #endregion

    }
}
