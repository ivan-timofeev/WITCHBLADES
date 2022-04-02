namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record ArtistCard : IViewModel
    {
        public Guid Id { get; set; }
        public string ArtistName { get; set; } = default!;
        public string? ArtistImage { get; set; }
    }
}
