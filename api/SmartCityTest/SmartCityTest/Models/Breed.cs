using System.ComponentModel.DataAnnotations;
namespace SmartCityTest.Models
{
    public class Breed
    {
        [Required]
        //Primary KEY
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //Foreign KEY
        [Required]
        public int IdSpecies { get; set; }
        public Species Species { get; set; }

    }
}