using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public string Name { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public int Duration { get; set; }

    public int CountryId { get; set; }

    public int CityId { get; set; }

    public decimal Cost { get; set; }

    public int SupplierId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Representative> Representatives { get; set; } = new List<Representative>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Supplier Supplier { get; set; } = null!;
}
