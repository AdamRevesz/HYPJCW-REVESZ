using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Comment
{
    public class CommentCreateUpdateDto
    {
        [MaxLength(1000)]
        public required string Body { get; set; }
    }
}
