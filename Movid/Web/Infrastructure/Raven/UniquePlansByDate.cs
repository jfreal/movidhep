//NO LONGER USED
//using System;
//using System.Linq;
//using Movid.App.Controllers;
//using Movid.App.Models;
//using Raven.Client.Indexes;

//namespace Movid.App.Infrastructure.Raven
//{
//    public class UniqueProgramByDate
//    {
//        public DateTime Date { get; set; }
//        public string Group { get; set; }
//        public int Count { get; set; }

//        public string CreatedBy { get; set; }
//    }

//    public class UniquePlansByDate : AbstractIndexCreationTask<Program, UniqueProgramByDate>
//    {
//        public UniquePlansByDate()
//        {
//            Map = plans => from s in plans
//                           select new
//                                      {
//                                          s.Created.Date,
//                                          s.Group,
//                                          Count = 1,
//                                          CreatedBy = s.CreatedById
//                                      };

//            Reduce = results => from result in results
//                                group result by new { result.Date, result.Group, result.CreatedBy }
//                                into g
//                                select new UniqueProgramByDate
//                                           {
//                                               Date = g.Key.Date,
//                                               Group = g.Key.Group,
//                                               Count = g.Sum(x=>x.Count),
//                                               CreatedBy = g.Key.CreatedBy

//                                           };
//        }
//    }
//}