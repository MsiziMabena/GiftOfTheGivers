using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftOfTheGivers.Models
{
    public class VolunteerAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public VolunteerTask VolunteerTask { get; set; }

        [Required]
        public string VolunteerUserId { get; set; }

        [ForeignKey("VolunteerUserId")]
        public ApplicationUser VolunteerUser { get; set; }

        public DateTime AssignmentDate { get; set; } = DateTime.Now;
    }
}
