using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Models
{
    public class VolunteerTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = "Open";

        public string? AdminNotes { get; set; }

        // ✅ Navigation property
        public ICollection<VolunteerAssignment> Assignments { get; set; } = new List<VolunteerAssignment>();
    }
}
