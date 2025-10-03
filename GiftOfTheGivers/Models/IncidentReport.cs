using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Models
{
    public class IncidentReport
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public string IncidentType { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string UrgencyLevel { get; set; } = string.Empty;

        public DateTime ReportDate { get; set; } = DateTime.UtcNow;

        public string? ReportedByUserId { get; set; }
        public ApplicationUser? ReportedByUser { get; set; }
    }
}
