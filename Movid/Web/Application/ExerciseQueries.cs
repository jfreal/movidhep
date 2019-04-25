using System.Collections.Generic;
using Movid.App.Infrastructure.Raven;
using Movid.App.Models;
using Movid.Shared.Model;
using Raven.Client;
using Raven.Client.Linq;

namespace Movid.App.Application
{
    public class ExerciseQueries
    {
        public static IRavenQueryable<Exercise> SearchExercisesByName(Account account, Clinic clinic, IDocumentSession session, string term, bool admin)
        {
            var searchString = term.Replace(" ", "?");
            IRavenQueryable<Exercise> exercises;

            if (admin)
            {
                exercises = session.Query<Exercise>(typeof (ByOwnableAndName).Name)
                                   .Where(x => x.Name.StartsWith(term) && x.Master)
                                   .OrderBy(x => x.Name);
            }
            else
            {
                exercises = session.Query<Exercise>(typeof(ByOwnableAndName).Name)
                                         .Where(x => x.Name.StartsWith(term) && (x.ClinicId == clinic.Id || x.AccountId == account.Id))
                                     .OrderBy(x => x.Name);   
            }
          
                                        
            return exercises;
        }

        public static IRavenQueryable<Exercise> SearchExercisesByCategory(Account account, Clinic clinic, IDocumentSession session, string term, bool admin)
        {
            IRavenQueryable<Exercise> exercises;

            if (admin)
            {
                exercises = session.Query<Exercise>(typeof (ByCategoriesAndNameSortByName).Name)
                       .Where(x => x.Categories.Contains(term) && (x.Master))
                       .OrderBy(x => x.Name);
            }
            else
            {
                exercises = session.Query<Exercise>(typeof(ByCategoriesAndNameSortByName).Name)
                                        .Where(x => x.Categories.Contains(term) && (x.ClinicId == clinic.Id || x.AccountId == account.Id))
                                        .OrderBy(x => x.Name);
            }

                                        
            return exercises;
        }
    }
}