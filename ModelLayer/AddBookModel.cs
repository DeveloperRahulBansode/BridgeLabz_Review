using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class AddBookModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
    }
}
