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
			[Display(Name = "Politi")]
            police = 1,
			[Display(Name = "Randers Kommune")]
			user = 2,
			[Display(Name = "Tredjemand")]
			thirdPart = 3,
			[Display(Name = "Egen observation")]
			own = 4
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
		
		//Foreign key
		[DisplayName("Trafiklysets ID")]
        [Required]
        public Installation InstallationId { get; set; }

		[DisplayName("Ansvarlig")]
		public string Worker { get; set; }

		[DisplayName("Tidspunkt for oprettelse")]
		public DateTime Time { get; set; }

		[DisplayName("Aktuel status")]
        public CaseStatus Status { get; set; }

		[DisplayName("Anmelder")]
		public ObserverSelection Observer { get; set; }

		[Required]
		[DisplayName("Fejlbeskrivelse")]
		public string ErrorDescription { get; set; }

		[Required]
		[DisplayName("Repareret af")]
		public string MadeRepair { get; set; }

		[Required]
		[DisplayName("Kommentarer")]
        public string UserComment { get; set; }
    }
}

