﻿using System;
using System.Collections.Generic;

namespace VroomVroom.Server.Models;

public partial class DriverLicense
{
    public int UserId { get; set; }

    public int LicenseTypeId { get; set; }

    public string HolderName { get; set; } = null!;

    public DateOnly DateOfIssue { get; set; }

    public virtual DriverLicenseType LicenseType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
