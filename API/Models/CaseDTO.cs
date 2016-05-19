using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebLib.Models;

namespace API.Models
{
    public class CaseDTO
    {
        public long Id { get; set; }
        [Required]
        public long InstallationId { get; set; }
        public string Worker { get; set; }
        public DateTime Time { get; set; }
        public Case.CaseStatus Status { get; set; }
        public Case.ObserverSelection Observer { get; set; }
        public string ErrorDescription { get; set; }
        public string MadeRepair { get; set; }
        public string UserComment { get; set; }
    }
}