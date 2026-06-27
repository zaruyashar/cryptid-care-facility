namespace CRYPTIDCARE.Models
{
    public class GlobalSearchResult
    {
        public string SearchQuery { get; set; }
        public IEnumerable<Cryptid> Cryptids { get; set; } = new List<Cryptid>();
        public IEnumerable<Enclosure> Enclosures { get; set; } = new List<Enclosure>();
        public IEnumerable<Keeper> Keepers { get; set; } = new List<Keeper>();
        public IEnumerable<FeedingSchedule> FeedingSchedules { get; set; } = new List<FeedingSchedule>();
    }
}