using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreTest3._1.Models
{
    public class DbConnectionOptions
    {
        public string WriteConnection { get; set; }

        public List<string> ReadConnectionList { get; set; }
    }
}
