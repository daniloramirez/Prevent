using System.Collections.Generic;

namespace Prevent.Common.Models
{
    public class PreventTypeResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PreventResponse> Prevents { get; set; }
    }
}
