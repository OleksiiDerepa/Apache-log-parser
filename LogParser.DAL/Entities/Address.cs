namespace LogParser.DAL.Entities
{
    public class Address : IEntity
    {
        public long Id { get; set; }
        public string Ip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Country Country { get; set; }
        public long CountryId { get; set; }
    }
}