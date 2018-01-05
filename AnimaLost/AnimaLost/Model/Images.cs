namespace AnimaLost.Model
{
    public class Images
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdAnimal { get; set; }

        public Animal IdAnimalNavigation { get; set; }
    }
}