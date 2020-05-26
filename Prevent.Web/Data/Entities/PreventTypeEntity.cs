using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prevent.Web.Data.Entities
{
    public class PreventTypeEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<PreventEntity> Prevents { get; set; }
    }
}
