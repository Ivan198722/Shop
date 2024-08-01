using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Shop.Models
{
    public class PropertyInfo
    {
        public string PropertyName { get; set; }

        public List<PropertyParameter> PropertyParameters { get; set; }

        
        
    }


    public class PropertyParameter
    {
        public int NumberOfParameter { get; set; }

        public string Parameter { get; set; }

        public bool IsMatchingProperty { get; set; }
    }
}
