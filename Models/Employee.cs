using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travel_agency_Lyapynova.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int PositionId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<ServiceAgreement> ServiceAgreements { get; set; } = new List<ServiceAgreement>();

    public virtual User User { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Surname) && Surname.Length < 3 && Surname.Length > 50)
            errors.Add(new ValidationResult("Фамилия должно быть от 3 до 50 символов."));

        if (string.IsNullOrEmpty(Name) && Name.Length < 3 && Name.Length > 50)
            errors.Add(new ValidationResult("Имя должно быть от 3 до 50 символов."));

        if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber.Length != 11)
            errors.Add(new ValidationResult("Телефон должен состоять из 11 символов"));

        return errors;
    }
}
