using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Movid.Shared.Model
{
    public class BlogEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [AllowHtml]
        [UIHint("MultilineText")]
        public string Text { get; set; }

        [UIHint("Date")]
        public DateTime Published { get; set; }
        public bool Public { get; set; }

        public AuthorEnum Author { get; set; }
    
        public bool AppLevel { get; set; }
    }

    public enum AuthorEnum
    {
        Mike = 1, John = 2, Ray = 3
    }


}