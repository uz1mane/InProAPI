using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class InvestmentContainer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Skin>? Skins { get; set; }

        public User Owner { get; set; }

        public InvestmentContainer()
        {
            Skins = new List<Skin>();
        }
    }
}
