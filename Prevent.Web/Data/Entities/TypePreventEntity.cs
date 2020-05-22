using System.ComponentModel.DataAnnotations;

namespace Prevent.Web.Data.Entities
{
    public class TypePreventEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }
    }
}
