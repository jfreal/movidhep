using Movid.Shared;
using Movid.Shared.Model;
using System;
using System.Collections.Generic;

namespace Movid.App.Models
{
    public class Protocol : ExerciseSet
    {
        public string Name { get; set; }

        private MarketingPlan _marketingPlan;
        public MarketingPlan MarketingPlan
        {
            get { return _marketingPlan ?? ( _marketingPlan = new MarketingPlan()); }
            set { _marketingPlan = value; }
        }
    }

    public class Program : ExerciseSet
    {
        public string Email { get; set; }
    }

    public class ExerciseSet : IOwnable
    {
        public int Id { get; set; }

        public List<Exercise> Exercises { get; set; }

        public string ShortUrl { get; set; }

        public DateTime Created { get; set; }

        public string Greeting { get; set; }

        private List<SendGridEmailEvent> _emailEvents;
        private List<ProgramView> _planViews;

        public List<SendGridEmailEvent> EmailEvents
        {
            get { return _emailEvents ?? ( _emailEvents = new List<SendGridEmailEvent>()); }
            set { _emailEvents = value; }
        }

        public List<ProgramView> PlanViews
        {
            get { return _planViews ?? ( _planViews = new List<ProgramView>()); }
            set { _planViews = value; }
        }

        public int AccountId { get; set; }
        public int UserId { get; set; }
        public int ClinicId { get; set; }
    }
}