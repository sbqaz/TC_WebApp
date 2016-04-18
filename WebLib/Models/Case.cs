using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLib.Models
{
    public class Case
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long InstallationId { get; set; }
        public string Worker { get; set; }
        public DateTime Time { get; set; }
        public int Observer { get; set; }
        public string ErrorDescription { get; set; }
        public string MadePepair { get; set; }
    }
}