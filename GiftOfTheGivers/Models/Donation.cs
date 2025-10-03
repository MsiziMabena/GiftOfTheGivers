using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        public string ResourceType { get; set; } = string.Empty;

        [Required]
        public string QuantityDescription { get; set; } = string.Empty;

        [Required]
        public string DonorName { get; set; } = string.Empty;

        [Required]
        public string ContactInfo { get; set; } = string.Empty;

        [Required]
        public string PickupLocation { get; set; } = string.Empty;

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;
    }
}
