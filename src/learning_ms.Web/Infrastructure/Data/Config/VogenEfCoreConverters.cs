using learning_ms.Web.Domain.CartAggregate;
using learning_ms.Web.Domain.GuestUserAggregate;
using learning_ms.Web.Domain.OrderAggregate;
using learning_ms.Web.Domain.ProductAggregate;
using Vogen;

namespace learning_ms.Web.Infrastructure.Data.Config;

[EfCoreConverter<ProductId>]
[EfCoreConverter<CartId>]
[EfCoreConverter<CartItemId>]
[EfCoreConverter<GuestUserId>]
[EfCoreConverter<OrderId>]
[EfCoreConverter<OrderItemId>]
[EfCoreConverter<Quantity>]
[EfCoreConverter<Price>]
internal partial class VogenEfCoreConverters;
