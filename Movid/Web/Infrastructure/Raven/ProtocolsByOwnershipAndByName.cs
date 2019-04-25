using System.Linq;
using Movid.App.Models;
using Movid.Shared;
using Raven.Client.Indexes;

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