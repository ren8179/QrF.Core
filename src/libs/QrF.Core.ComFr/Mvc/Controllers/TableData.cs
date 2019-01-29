using System.Collections;

namespace QrF.Core.ComFr.Mvc.Controllers
{
    public class TableData
    {
        public TableData(IEnumerable data, int recordsTotal, int draw)
        {
            Draw = draw;
            RecordsTotal = recordsTotal;
            RecordsFiltered = recordsTotal;
            Data = data;
        }
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable Data { get; set; }
    }
}
