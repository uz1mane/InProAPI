using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class Skin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double BoughtFor { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int PercentGoal { get; set; }

        [Required]
        public InvestmentContainer container { get; set; } 


    }
}
