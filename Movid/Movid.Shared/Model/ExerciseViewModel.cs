using System.ComponentModel.DataAnnotations;

namespace Movid.Shared.Model
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [UIHint("MultilineText")]
        public string Categories { get; set; }
       

        public string VideoId { get; set; }

        [UIHint("MultilineText")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public string Group { get; set; }

        [ScaffoldColumn(false)]
        public int OriginalExercise { get; set; }
      
    }
}