using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learning_ms.Web.Domain.Enums.PaymentProvider;

public enum PaymentProvider
{
  None,
  Stripe,
  PayPal,
  Flutterwave,
  Paystack,
  MTNMobileMoney,
  OrangeMoney,
}
