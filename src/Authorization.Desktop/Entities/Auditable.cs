using System;

namespace Authorization.Desktop.Entities;

public class Auditable:BaseEntity
{
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }
}
