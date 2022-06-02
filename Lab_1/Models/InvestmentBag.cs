using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class InvestmentBag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Skin>? Skins { get; set; }

        public InvestmentBag()
        {
            Skins = new List<Skin>();
        }
    }
}
