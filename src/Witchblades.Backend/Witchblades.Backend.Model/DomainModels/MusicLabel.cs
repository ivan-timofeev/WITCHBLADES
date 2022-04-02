namespace Witchblades.Backend.Models
{
    public class MusicLabel
    {
        public Guid           Id         { get; set; }
        public string         LabelName  { get; set; } = default!;
        public List<Artist>?  Artists    { get; set; }
    }
}
