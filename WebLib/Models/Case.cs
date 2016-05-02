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

        public enum ObserverSelection
        {
            police = 1,
            user = 2,
            thirdPart = 3,
            own = 4,
            alarm = 5,
            lightError = 6,
            toneSignal = 7,
            pedestrianPush = 8,
            shutdownError = 9,
            trafficInjury = 10,
            detectorError = 11
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long InstallationId { get; set; }
        [Range(0, 3)]
        public CaseStatus Status { get; set; }
        public string Worker { get; set; }
        public DateTime Time { get; set; }

        /// <summary>
        /// test hest
        /// </summary>
        /// <param name="Observer"> test test </param>
        [Range(0, 11)]
        public ObserverSelection Observer { get; set; }
        public string ErrorDescription { get; set; }
        public string MadeRepair { get; set; }
        public string UserComment { get; set; }
    }
}

