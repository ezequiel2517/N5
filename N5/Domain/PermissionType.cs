using System.ComponentModel.DataAnnotations;

namespace N5.Domain
{
    public class PermissionType
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
