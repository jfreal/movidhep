using System;
using System.Collections.Generic;
using Movid.Shared.Model;
using Raven.Client.Indexes;
using System.Linq;

namespace Movid.App.Infrastructure.Raven
{


    public class CategoryPaths : AbstractIndexCreationTask<Exercise, CategoryPaths.CategoryGroup>
    {
        public class CategoryGroup
        {
            public string Category { get; set; }
            public string ClinicId { get; set; }
            public string AccountId { get; set; }
        }

        public CategoryPaths()
        {
            Map = exercises => from exer in exercises
                               from cat in exer.Categories
                               select new
                               {
                                   AccountId = exer.AccountId,
                                   ClinicId = exer.ClinicId,
                                   Category = cat
                               };

            Reduce = categoryGroups => from categoryGroup in categoryGroups
                                       group categoryGroup 
                                       by new {
                                           categoryGroup.Category, 
                                           ClinicId = categoryGroup.ClinicId,
                                           AccountId = categoryGroup.AccountId
                                       }
                                       into g
                                       select new CategoryGroup
                                          {
                                              Category = g.Key.Category,
                                              ClinicId = g.Key.ClinicId,
                                              AccountId = g.Key.AccountId
                                          };
        }
    }
}