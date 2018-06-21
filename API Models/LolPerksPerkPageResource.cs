using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_API.API_Models
{
    public class LolPerksPerkPageResource
    {
        public int[] autoModifiedSelections { get; set; }
        public bool current { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isDeletable { get; set; }
        public bool isEditable { get; set; }
        public bool isValid { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public int primaryStyleId { get; set; }
        public int[] selectedPerkIds { get; set; }
        public int subStyleId { get; set; }
    }
}
