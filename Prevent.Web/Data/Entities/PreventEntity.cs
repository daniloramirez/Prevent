﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Prevent.Web.Data.Entities
{
    public class PreventEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public TypePreventEntity TypePrevent { get; set; }

        public string File { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }
    }
}