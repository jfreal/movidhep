using Movid.Shared.Model;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace Movid.App.Infrastructure.Raven
{
    public class ByCategoriesAndNameSortByName : AbstractIndexCreationTask<Exercise>
    {
        public ByCategoriesAndNameSortByName()
        {
            Index("Name", FieldIndexing.Default);
            Index("Categories", FieldIndexing.Default);

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