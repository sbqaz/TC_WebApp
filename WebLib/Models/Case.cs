using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLib.Models
{
    public class Case
    {
        public enum CaseStatus
        {
            created = 0,
            started = 1,
            done = 2,
            pending = 3
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long InstallationId { get; set; }
        [Range(0, 3)]
        public int Status { get; set; }
        public string Worker { get; set; }
        public DateTime Time { get; set; }
        public int Observer { get; set; }
        public string ErrorDescription { get; set; }
        public string MadePepair { get; set; }


    }
}