using System.Collections.Generic;

namespace Converter.Models
{
    public class UnitCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<UnitItem> Units { get; set; }
    }
}