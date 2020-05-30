using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayersManagementSystemClass
{
    public class Team
    {
        public Team()
        {
        }

        public string TeamName { get; set; }
        public string Ground { get; set; }
        public string Coach { get; set; }
        public int FoundYear { get; set; }
        public string Region { get; set; }
    }
}
