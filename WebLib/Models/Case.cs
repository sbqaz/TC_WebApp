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
			[Display(Name="Oprettet")]
			created = 0,
			[Display(Name = "Påbegyndt")]
			started = 1,
			[Display(Name = "Afsluttet")]
			done = 2,
			[Display(Name = "Afventer")]
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
		[DisplayName("Trafiklysets ID")]
		[Required]
        public long InstallationId { get; set; }
        [Range(0, 3)]
        public int Status { get; set; }
		[DisplayName("Ansvarlig")]
		public string Worker { get; set; }
		[DisplayName("Tidspunkt for oprettelse")]
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
		[DisplayName("Anmelder")]
		public int Observer { get; set; }
		[DisplayName("Fejlbeskrivelse")]
		public string ErrorDescription { get; set; }
		[DisplayName("Repareret af")]
		public string MadeRepair { get; set; }
		[DisplayName("Kommentarer")]
        public string UserComment { get; set; }
    }
}