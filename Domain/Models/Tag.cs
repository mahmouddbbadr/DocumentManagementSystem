


namespace Domain.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<Document> Documents { get; set; }


    }
}
