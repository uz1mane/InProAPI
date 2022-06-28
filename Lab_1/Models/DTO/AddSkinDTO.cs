using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models.DTO
{
    public class AddSkinDTO
    {        
        [Required]
        public string Name { get; set; }
        [Required]
        public double BoudghtFor { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int PercentGoal { get; set; }

        [Required]
        public int containerId { get; set; }
    }
}
