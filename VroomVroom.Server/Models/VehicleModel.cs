﻿using System;
using System.Collections.Generic;

namespace VroomVroom.Server.Models;

public partial class VehicleModel
{
    public int ModelId { get; set; }

    public int VehicleTypeId { get; set; }

    public string ModelName { get; set; } = null!;

    public long RatePerDay { get; set; }

    public int ManufacturerId { get; set; }

    public string? ImageFile { get; set; }

    public string? Description { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual VehicleType VehicleType { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
