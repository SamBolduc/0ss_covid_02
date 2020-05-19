namespace BillingManagement.Models
{
    public class ContactInfo
    {
        public int ID { get; private set; }
        public string ContactType { get; set; }
        public string Contact { get; set; }

        public string Info => $"{ContactType} : {Contact}";
    }
}