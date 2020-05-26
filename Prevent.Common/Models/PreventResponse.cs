using System;

namespace Prevent.Common.Models
{
    public class PreventResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string File { get; set; }

        public DateTime Date { get; set; }

        public UserResponse User { get; set; }

    }
}
