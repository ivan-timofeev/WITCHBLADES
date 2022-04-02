namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record AlbumUpdate : IViewModel
    {
        public string? AlbumName { get; set; }
        public Guid? Artist { get; set; }
        public IEnumerable<Guid>? Tracks { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? AlbumImage { get; set; }
    }
}
