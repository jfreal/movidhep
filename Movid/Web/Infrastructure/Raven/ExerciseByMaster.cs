using Movid.Shared.Model;
using Raven.Client.Indexes;
using System.Linq;

namespace Movid.App.Infrastructure.Raven
{
    public class ExerciseByMaster : AbstractIndexCreationTask<Exercise>
    {
        public ExerciseByMaster()
        {
            Map = exercises => from exer in exercises
                               select new
                               {
                                   Name = exer.Name,
                                   exer.Master
                               };
        }
    }
}