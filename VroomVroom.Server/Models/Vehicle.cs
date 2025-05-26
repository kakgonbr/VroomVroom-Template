﻿using System;
using System.Collections.Generic;

namespace VroomVroom.Server.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int ModelId { get; set; }

    public string? Condition { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual VehicleModel Model { get; set; } = null!;
}
