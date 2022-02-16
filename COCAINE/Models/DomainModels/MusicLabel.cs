namespace COCAINE.Models.DomainModels
{
    public class MusicLabel
    {
        public int           Id { get; set; }
        public string        LabelName { get; set; }
        public List<Artist>  Artists { get; set; }
    }
}
