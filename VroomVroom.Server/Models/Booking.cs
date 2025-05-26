﻿using System;
using System.Collections.Generic;

namespace VroomVroom.Server.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int VehicleId { get; set; }

    public int UserId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int PreRentDuration { get; set; }

    public int PostRentDuration { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
