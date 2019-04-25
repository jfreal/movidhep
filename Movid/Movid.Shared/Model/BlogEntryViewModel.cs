using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Movid.Shared.Model
{
    public class BlogEntryViewModel
    {
        
        public string Id { get; set; }
        public string Title { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        [UIHint("Date")]
        public DateTime Published { get; set; }
        public bool Public { get; set; }
    }
}