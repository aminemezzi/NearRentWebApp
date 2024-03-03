using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class User
{
    public Guid AzureObjectId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public DateTime Joined { get; set; }

    public int CurrentStatus { get; set; }

    public bool EmailValidated { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserCategoryPreference> UserCategoryPreferences { get; set; } = new List<UserCategoryPreference>();

    public virtual ICollection<UserEmail> UserEmails { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserPhone> UserPhones { get; set; } = new List<UserPhone>();

    public virtual ICollection<UserStatusHistory> UserStatusHistories { get; set; } = new List<UserStatusHistory>();
}
