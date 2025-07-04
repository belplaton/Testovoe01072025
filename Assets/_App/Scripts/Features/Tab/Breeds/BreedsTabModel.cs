using System.Collections.Generic;

namespace waterb.Features.Tab.Breeds
{
    public class BreedsTabModel
    {
        public List<DogBreed> Breeds { get; set; } = new List<DogBreed>();
        public DogBreed SelectedBreed { get; set; }
        public bool IsLoading { get; set; }
        public BreedFact SelectedFact { get; set; }
    }

    public class DogBreed
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class BreedFact
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
} 