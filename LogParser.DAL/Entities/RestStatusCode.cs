namespace LogParser.DAL.Entities
{
    public class RestStatusCode : IEntity
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
    }
}