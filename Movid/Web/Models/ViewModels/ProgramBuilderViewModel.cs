using Movid.Shared;

namespace Movid.App.Models.ViewModels
{
    public class ProgramBuilderViewModel
    {
        public Clinic Clinic { get; set; }
        public User User { get; set; }
        public ExerciseSet ExerciseSet { get; set; }
        public Account Account { get; set; }


        public string TypeDescription
        {
            get
            {
                if (ExerciseSet == null)
                    return "Program";

                if( ExerciseSet.GetType() == typeof(Program))
                    return "Program";

                return "Protocol";
            }
        }

    }
}