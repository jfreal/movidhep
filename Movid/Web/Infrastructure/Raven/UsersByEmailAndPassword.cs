using System.Linq;
using Movid.Shared;
using Raven.Client.Indexes;

namespace Movid.App.Infrastructure.Raven
{
    public class UsersByEmailAndPassword : AbstractIndexCreationTask<User>
    {
        public UsersByEmailAndPassword()
        {
            Map = users => from doc in users
                           select new {Password = doc.Password, Email = doc.Email};
        }
    }
}