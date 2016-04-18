namespace WebLib.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailNotification { get; set; }
        public bool SMSNotification { get; set; }
    }
}