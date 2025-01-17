using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Comment
{
    public class CommentCreateUpdateDto
    {
        public required string ContentId { get; set; } = "";
        [MaxLength(1000)]
        public required string Body { get; set; }
    }
}
