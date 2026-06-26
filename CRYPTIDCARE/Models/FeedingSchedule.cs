namespace CRYPTIDCARE.Models
{
    public class FeedingSchedule
    {
        public int Id { get; set; }
        public int CryptidId { get; set; }
        public int KeeperId { get; set; }
        public string DietaryItems { get; set; }
    }
}
