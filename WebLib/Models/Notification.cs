using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel;
using System.Text;

namespace WebLib.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Msg { get; set; }

        public string BuildNewCaseString(string instaName, string instaAddr)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Der er oprettet en ny sag på lyskrydet: \n");
            stringBuilder.Append(instaName + "\n");
            stringBuilder.Append(instaAddr);
            return stringBuilder.ToString();
        }

        public string BuildStatusChangedCase(string instaName, string instaAddr, Case.CaseStatus oldStatus, Case.CaseStatus newStatus)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Status er ændret på sag for lyskrudet: \n");
            stringBuilder.Append(instaName + "\n");
            stringBuilder.Append(instaAddr + "\n");
            stringBuilder.Append("Ny status er: " + newStatus + "\n");
            stringBuilder.Append("Tidligere status er: " + oldStatus + "\n");
            return stringBuilder.ToString();
        }
    }
    
}