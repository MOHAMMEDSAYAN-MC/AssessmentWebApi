using System;
using System.Collections.Generic;

namespace AssessmentApi.Model;

public partial class Contact
{
    public string ContactId { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Pincode { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Insured> Insureds { get; set; } = new List<Insured>();
}
