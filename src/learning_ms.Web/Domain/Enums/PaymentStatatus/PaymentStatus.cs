using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learning_ms.Web.Domain.Enums.PaymentStatus;

public enum PaymentStatus
{
  Pending,
  Processing,
  Paid,
  Failed,
  Cancelled,
  Refunded,
  PartiallyRefunded,
  Expired,
}
