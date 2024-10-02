using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs
{
    public class PaginationDTO<T>
    {
        public int Size { get; set; }
        public int Index { get; set; }

        public IEnumerable<T> Items { get; set; } = null!;

        public int Recordes { get; set; }

        public int NoOfPages { get; set; }

    }
}
