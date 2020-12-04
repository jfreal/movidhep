using Movid.App.Models;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace Movid.App.Infrastructure.Raven
{
    public class ProgramByOwnershipAndDate : AbstractIndexCreationTask<Program>
    {
        public ProgramByOwnershipAndDate()
        {
            Map = plans => from plan in plans
                           orderby plan.Created
                           select new
                                      {
                                          plan.UserId,
                                          plan.AccountId,
                                          plan.ClinicId,
                                          plan.Created
                                      };
        }
    }
}