    using System.ComponentModel.DataAnnotations;

public class OrderBooking
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime BookingDate { get; set; }
    public string Category { get; set; }
    public int? CockCount { get; set; }
    public int? HenCount { get; set; }
    public int? PigeonPairs { get; set; }
    public int? EggCount { get; set; }
    public Guid UserId { get; set; }
    //public User User { get; set; }
}

