namespace Converter.Models
{
    public class UnitItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // multiplier to base unit
        public double ToBase { get; set; }
    }
}