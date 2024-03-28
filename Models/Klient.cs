using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Klient
{
    public int KlientId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string PassportSerias { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<ServiceAgreement> ServiceAgreements { get; set; } = new List<ServiceAgreement>();

    public virtual User User { get; set; } = null!;
}
