using System.Linq;
using Movid.Shared.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Movid.App.Infrastructure.Raven
{
    public class ByCategoriesAndNameSortByName : AbstractIndexCreationTask<Exercise>
    {
        public ByCategoriesAndNameSortByName()
        {
            Index("Name", FieldIndexing.Analyzed);
            Index("Categories", FieldIndexing.Analyzed);

            Map = exercises => from exercise in exercises
                               orderby exercise.Name
                               select new {
                                   Categories = exercise.Categories, 
                                   ClinicId = exercise.ClinicId, 
                                   AccountId = exercise.AccountId,
                                   Name = exercise.Name,
                                   Master = exercise.Master
                               };
        }
    }
}