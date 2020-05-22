using System;
using System.ComponentModel.DataAnnotations;

namespace Prevent.Web.Data.Entities
{
    public class PreventEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "The field {0} invalid character.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Prevent Type")]
        public int PreventTypeId { get; set; }

        public PreventTypeEntity PreventType { get; set; }

        public string File { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }
    }
}
