using System.Linq;
using Movid.Shared.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Movid.App.Infrastructure.Raven
{
    //shared for public library view
    public class MasterExerciseSearch : AbstractIndexCreationTask<MasterExercise, MasterExerciseSearch.ReduceResult>
    {
        public class ReduceResult
        {
            public string Query { get; set; }
            public int AccountId { get; set; }
            public int ClinicId { get; set; }
            public int UserId { get; set; }
            public bool Master { get; set; }
        }

        public MasterExerciseSearch()
        {
            Index("Query", FieldIndexing.Analyzed);

            Map = exercises => from exer in exercises
                               from cat in exer.Categories
                               select new
                               {
                                   Query = new object[]
                                                          {
                                                              cat,
                                                              exer.Name
                                    
                                                          },
                                   AccountId = exer.AccountId,
                                   ClinicId = exer.ClinicId,
                                   UserId = exer.UserId,
                                   Master = exer.Master
                               };
        }
    }


    //shared for public library view
    public class ExerciseSearch : AbstractIndexCreationTask<Exercise, ExerciseSearch.ReduceResult>
    {
        public class ReduceResult
        {
            public string Query { get; set; }
            public int AccountId { get; set; }
            public int ClinicId { get; set; }
            public int UserId { get; set; }
            public bool Master { get; set; }
        }

        public ExerciseSearch()
        {
            Index("Query", FieldIndexing.Analyzed);

            Map = exercises => from exer in exercises  
                               from cat in exer.Categories
                               select new
                                          {
                                              Query = new object[]
                                                          {
                                                              cat,
                                                              exer.Name
                                    
                                                          },
                                              AccountId = exer.AccountId,
                                              ClinicId = exer.ClinicId,
                                              UserId = exer.UserId,
                                              Master = exer.Master
                                          };
        }
    }
}