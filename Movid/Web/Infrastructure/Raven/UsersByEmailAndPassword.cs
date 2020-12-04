using Movid.Shared;
using Raven.Client.Documents.Indexes;
using System.Linq;

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