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
        public string Msg { get; set; }

        public string BuildNewCaseString(string instaName, string instaAddr)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Der er oprettet en ny sag på lyskrydet: \n");
            stringBuilder.Append(instaName + "\n");
            stringBuilder.Append(instaAddr);
            return stringBuilder.ToString();
        }

        public string BuildStatusChangedCase(string instaName, string instaAddr, int oldStatus, int newStatus)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Status er ændret på sag for lyskrudet: \n");
            stringBuilder.Append(instaName + "\n");
            stringBuilder.Append(instaAddr + "\n");
            stringBuilder.Append("Ny status er: " + IntToCaseStatus(newStatus) + "\n");
            stringBuilder.Append("Tidligere status er: " + IntToCaseStatus(oldStatus) + "\n");
            return stringBuilder.ToString();
        }

        private string IntToCaseStatus(int s)
        {
            string returnString = "";
            switch (s)
            {
                case  (int)Case.CaseStatus.created:
                    returnString = "Oprettet";
                    break;
                case (int)Case.CaseStatus.started:
                    returnString =  "I gang";
                    break;
                case (int)Case.CaseStatus.done:
                    returnString =  "Færdig";
                    break;
                case (int)Case.CaseStatus.pending:
                    returnString =  "Afventer";
                    break;
            }
            return returnString;
        }
    }
    
}