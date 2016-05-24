using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		[DisplayName("Trafiklysets ID")]
        public long Id { get; set; }
		[DisplayName("Trafiklysets navn")]
		public string Name { get; set; }
        public InstalStatus Status { get; set; }
        public Position Position { get; set; }
    }
}