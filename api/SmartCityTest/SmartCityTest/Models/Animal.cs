using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartCityTest.Models
{
    public class Animal
    {
        [Required]
        //Primary key
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //Foreign KEY
        public string NameColor { get; set; }
        public Color Color { get; set; }
        public int IdBreed { get; set; }
        public Breed Breed { get; set; }
    }
}