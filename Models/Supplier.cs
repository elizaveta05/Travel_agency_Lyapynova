using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<CooperationAgreement> CooperationAgreements { get; set; } = new List<CooperationAgreement>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public virtual User? User { get; set; }
}
