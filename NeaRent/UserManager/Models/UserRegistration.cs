using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class UserRegistration
{
    public Guid Id { get; set; }

    public Guid AzureObjectId { get; set; }

    public int UserType { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int PhoneCountryId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public bool PhoneVerified { get; set; }

    public string Address { get; set; } = null!;

    public decimal AddressLat { get; set; }

    public decimal AddressLng { get; set; }

    public int CountryId { get; set; }

    public string? District { get; set; }

    public string Street { get; set; } = null!;

    public string? Flat { get; set; }

    public string PostCode { get; set; } = null!;

    public string Area { get; set; } = null!;

    public string Province { get; set; } = null!;

    public int Distance { get; set; }

    public bool ShowExactLocation { get; set; }

    public int IdentityType { get; set; }

    public string IdentityImage { get; set; } = null!;

    public string SelfieImage { get; set; } = null!;

    public int Status { get; set; }

    public bool VettingBusy { get; set; }

    public DateTime VettingStart { get; set; }

    public DateTime? VettingEnd { get; set; }

    public int LastStep { get; set; }

    public bool Active { get; set; }
}
