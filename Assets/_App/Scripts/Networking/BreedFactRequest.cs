using waterb.Networking;

namespace waterb.Networking
{
    public sealed class BreedFactRequest : IRequest
    {
        public string BreedId { get; set; }
        public BreedFactRequest(string breedId)
        {
            BreedId = breedId;
        }
    }
} 