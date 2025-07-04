using System.Collections.Generic;

namespace waterb.Networking
{
    public sealed class BreedsResponse : IResponse
    {
        public List<DogBreedDto> Breeds { get; set; }
    }

    public class DogBreedDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
} 