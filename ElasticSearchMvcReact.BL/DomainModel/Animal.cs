using ElasticSearchMvcReact.BL.ElasticSearchAttributes;
using System;

namespace ElasticSearchMvcReact.BL.DomainModel
{
    public class Animal
    {
        public const string SearchIndex = "animals";

        public const string SearchType = "animal";

        [FieldType(fieldType = FieldTypes._long)]
        public int Id { get; set; }

        [FieldType(fieldType = FieldTypes._string)]
        [NotAnalyzed]
        public string AnimalType { get; set; }

        [FieldType(fieldType =FieldTypes._string)]
        [Analyzer(analyzerType =AnalyzerTypes.Standard)]
        [Suggest]
        public string Description { get; set; }

        [FieldType(fieldType = FieldTypes._string)]
        [Analyzer(analyzerType = AnalyzerTypes.Standard)]
        [Suggest]
        public string Gender { get; set; }

        [FieldType(fieldType = FieldTypes._string)]
        public string LastLocation { get; set; }

        [FieldType(fieldType = FieldTypes._date)]
        public DateTime DateOfBirth { get; set; }

        [FieldType(fieldType = FieldTypes._date)]
        public DateTime CreatedTimestamp { get; set; }

        [FieldType(fieldType = FieldTypes._date)]
        public DateTime UpdatedTimestamp { get; set; }
    }
}