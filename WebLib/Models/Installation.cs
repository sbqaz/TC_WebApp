using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebLib.Models
{
    public class Installation
    {
        public enum InstalStatus
        {
            Green = 0,
            Yellow = 1,
            Red = 2
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public InstalStatus Status { get; set; }
        public Position Position { get; set; }
    }
}