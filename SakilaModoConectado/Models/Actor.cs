using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakilaModoConectado.Models
{
    public class Actor
    {
        public int actor_id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public DateTime last_update { get; set; }
    }
}
