using System;
using System.ComponentModel;
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
            police = 0,
            user = 1,
            thirdPart = 2,
            own = 3,
            alarm = 4,
            lightError = 5,
            toneSignal = 6,
            pedestrianPush = 7,
            shutdownError = 8,
            trafficInjury = 9,
            detectorError = 10
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long InstallationId { get; set; }
        [Range(0, 3)]
        public int Status { get; set; }
        public string Worker { get; set; }
        public DateTime Time { get; set; }

        /// <summary>
        /// Observer that reportet the case.
        /// police = 0
        /// user = 1
        /// thirdPart = 2
        /// own = 3
        /// alarm = 4
        /// lightError = 5
        /// toneSignal = 6
        /// pedestrianPush = 7
        /// shutdownError = 8
        /// trafficInjury = 9
        /// detectorError = 10
        /// </summary>
        [Range(0, 10)]
        public int Observer { get; set; }
        public string ErrorDescription { get; set; }
		[DisplayName("")]
		public string MadeRepair { get; set; }
		[DisplayName("Kommentarer")]
        public string UserComment { get; set; }
    }
}