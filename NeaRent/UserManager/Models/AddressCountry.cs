using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class AddressCountry
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string InitialsShort { get; set; } = null!;

    public string InitialsLong { get; set; } = null!;

    public string PhoneCode { get; set; } = null!;

    public string? CurrencyCode { get; set; }

    public string? CurrencyName { get; set; }

    public string? CurrencySymbol { get; set; }

    public string? PostalCodeRegex { get; set; }

    public bool Active { get; set; }
}
