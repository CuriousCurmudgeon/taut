using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut
{
    public class Reaction
    {
        public Reaction()
        {
            Users = new List<string>();
        }

        public string Name { get; set; }

        public int Count { get; set; }

        public IEnumerable<string> Users { get; set; }
    }
}
