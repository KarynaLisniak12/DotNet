using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilderApp
{
    public class Record
    {
        public int Id { set; get; }
        public string Text { set; get; }
        public string Author { set; get; }
        public DateTime RecordDate { set; get; }
    }
}
