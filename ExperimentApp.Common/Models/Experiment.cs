using System.ComponentModel.DataAnnotations;

namespace ExperimentApp.Common.Models
{
    public class Experiment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DeviceToken { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
