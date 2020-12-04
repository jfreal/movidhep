using Movid.App.Models;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace Movid.App.Infrastructure.Raven
{
    public class ProtocolsByOwnershipAndByName : AbstractIndexCreationTask<Protocol>
    {
        public ProtocolsByOwnershipAndByName()
        {
            Map = plans => from plan in plans
                            orderby plan.Created
                            select new {
                                plan.UserId,
                                plan.AccountId,
                                plan.ClinicId,
                                plan.Name,
                                plan.Created                                
                            };
        }
    }
}