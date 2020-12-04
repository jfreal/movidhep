using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movid.Shared.Model
{

    public class MasterExercise : Exercise
    {

        public DateTime LastPushedOut { get; set; }

        //when existing exercises are copied into an account we set the originalexerciseid 
        //from the copied exercise, since we are splitting master exercises into their own collection
        //and can't manually set an id, we need to store the pre-master-collection id in this property to reference later
        public int MasterExerciseId { get; set; }
    }

    public class Exercise : IOwnable
    {

        public DateTime CreatedOn { get; set; }
        private List<string> _categories;

        public class Details
        {
            private string _value;

            public Details()
            {

            }

            public Details(string label, string shorthand, string[] options, string value)
            {
                Label = label;
                Shorthand = shorthand;
                Options = options;
                Value = value;

                if (string.IsNullOrWhiteSpace(SelectedOption))
                {
                    SelectedOption = Options.ToList().FirstOrDefault();
                }

            }

            public Details(string labelAndShorthand, string[] options, string value) : this(labelAndShorthand, labelAndShorthand, options, value) { }

            public string Label { get; set; }
            public string Shorthand { get; set; }
            public string[] Options { get; set; }
            public string Value
            {
                get { return _value ?? (_value = ""); }
                set { _value = value; }
            }

            public string SelectedOption { get; set; }
        }

        public Exercise()
        {
            ExerciseDetails = new List<Details>
                              {
                                  new Details("Repetitions", "Reps", new string[0], ""),
                                  new Details("Sets", new string[0], ""),
                                  new Details("Hold", new[] {"Seconds", "Minutes"}, ""),
                                  new Details("Frequency", "Freq", new[] {"Daily", "Twice Daily"}, ""),
                                  new Details("Rest", new[] {"Seconds", "Minutes"}, "")
                              };
        }



        public int Id { get; set; }
        public string Name { get; set; }


        [UIHint("Categories")]
        public List<string> Categories
        {
            get { return _categories ?? (_categories = new List<string>()); }
            set { _categories = value; }
        }

        public string VideoId { get; set; }

        [UIHint("MultilineText")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public int OriginalExercise { get; set; }

        public Exercise Copy()
        {
            return (Exercise)this.MemberwiseClone();
        }

        [UIHint("ExerciseDetails")]
        public List<Details> ExerciseDetails { get; set; }

        [ScaffoldColumn(false)]
        public int ClinicId { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [ScaffoldColumn(false)]
        public int AccountId { get; set; }

        [ScaffoldColumn(false)]
        public bool Master { get; set; }
    }
}
