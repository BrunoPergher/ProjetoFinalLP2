using System.ComponentModel.DataAnnotations;

namespace LP2MVCWithAuth.Data.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? HouseNumber { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Postcode { get; set; }

        public string? State { get; set; }

        public string? StreetName { get; set; }

        public bool IsInitical { get; set; }

        public int IdTarefa { get; set; }
    }
}
