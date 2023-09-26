using System;
using System.Collections.Generic;

namespace AssessmentApi.Model;

public partial class Insured
{
    public string InsuredId { get; set; } = null!;

    public int? UserTypeId { get; set; }

    public string? ContactId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? AadharNumber { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Pannumber { get; set; }

    public string? AccountNumber { get; set; }

    public string? Ifsccode { get; set; }

    public string? BankName { get; set; }

    public string? BankAddress { get; set; }

    public virtual Contact? Contact { get; set; }
}
