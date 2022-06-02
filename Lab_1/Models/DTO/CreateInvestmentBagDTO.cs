using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models.DTO
{
    public class CreateInvestmentBagDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
