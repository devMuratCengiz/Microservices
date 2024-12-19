using System.ComponentModel.DataAnnotations;

namespace Microservices.Web.Models.Catalog.Feature
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs Süre")]
        [Required]
        public int Duration { get; set; }
    }
}
