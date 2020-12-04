using Movid.Shared.Model;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace Movid.App.Infrastructure.Raven
{



    public class ByOwnableAndName : AbstractIndexCreationTask<Exercise>
    {
        

        public ByOwnableAndName()
        {
            //Index("Name", FieldIndexing.Analyzed);
            //Sort(x => x.Name, SortOptions.String);

            Map = exercises => from exr in exercises
                               orderby exr.Name
                               select new
                                          {
                                              ClinicId = exr.ClinicId, 
                                              AccountId = exr.AccountId, 
                                              UserId = exr.UserId, 
                                              Name = exr.Name,
                                              Master = exr.Master
                                          };
        }
    }
}