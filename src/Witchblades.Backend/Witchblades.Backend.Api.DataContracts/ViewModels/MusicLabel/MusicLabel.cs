namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record MusicLabel : IViewModel
    {
        public Guid Id { get; set; }
        public string LabelName { get; set; } = default!;
        public List<Artist>? Artists { get; set; }
    }
}
