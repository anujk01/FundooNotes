using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Users
{
    public class NoteUpdateModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Color { get; set; }
        public bool IsPin { get; set; }
        public bool IsArchive { get; set; }
        public bool IsRemainder { get; set; }
        public bool IsTrash { get; set; }
    }
}
