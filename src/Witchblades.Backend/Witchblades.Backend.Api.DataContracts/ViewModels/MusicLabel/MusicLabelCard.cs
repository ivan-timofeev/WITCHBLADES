namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record MusicLabelCard
    {
        public Guid Id { get; set; }
        public string LabelName { get; set; } = default!;
    }
}
