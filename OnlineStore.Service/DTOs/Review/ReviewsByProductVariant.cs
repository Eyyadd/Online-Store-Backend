using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Review
{
    public class ReviewsByProductVariant
    {
        public int ProductId { get; set; }
        public List<CommentRateDTO> Comment { get; set; }=new List<CommentRateDTO>();
    }
}
