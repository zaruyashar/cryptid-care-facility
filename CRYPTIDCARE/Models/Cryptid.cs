namespace CRYPTIDCARE.Models
{
    public class Cryptid
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string SpeciesType { get; set; }
        public int EnclosureId { get; set; }
        public string ImageUrl { get; set; }
        public string? EnclosureName { get; set; }
    }
}
