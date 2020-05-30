using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayersManagementSystemClass
{
    public class Player
    {
        public string PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string BirthPlace { get; set; }
        public string TeamName { get; set; }
    }
}
