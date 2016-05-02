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
		[DisplayName("Trafiklysets ID")]
		[Required]
        public long InstallationId { get; set; }
        [Range(0, 3)]

		[DisplayName("Ansvarlig")]
		public string Worker { get; set; }
		[DisplayName("Tidspunkt for oprettelse")]
		public DateTime Time { get; set; }

        public CaseStatus Status { get; set; }
        public string Worker { get; set; }
        public DateTime Time { get; set; }

        [Range(0, 10)]
		[DisplayName("Anmelder")]
		public ObserverSelection Observer { get; set; }
		[DisplayName("Fejlbeskrivelse")]
		public string ErrorDescription { get; set; }
		[DisplayName("Repareret af")]
		public string MadeRepair { get; set; }
		[DisplayName("Kommentarer")]

        public string UserComment { get; set; }
    }
}

