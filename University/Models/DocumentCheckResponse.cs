using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class DocumentCheckResponse
    {
        public bool checksumExists { get; set; }
        public string idOfDuplicatedDocument { get; set; }
    }
}
