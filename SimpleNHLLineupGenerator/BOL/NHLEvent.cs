using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNHLLineupGenerator.BOL
{
    public class NHLEvent
    {
        public string EntryId { get; set; }
        public string ContestId { get; set; }
        public string ContestName { get; set; }
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string W1{ get; set; }
        public string W2 { get; set; }
        public string D1 { get; set; }
        public string D2 { get; set; }
        public string UTIL1 { get; set; }
        public string UTIL2 { get; set; }
        public string G { get; set; }
    }
}
