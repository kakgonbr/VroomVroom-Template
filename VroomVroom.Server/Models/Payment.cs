using System;
using System.Collections.Generic;

namespace VroomVroom.Server.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int UserId { get; set; }

    public int BookingId { get; set; }

    public long AmountPaid { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
