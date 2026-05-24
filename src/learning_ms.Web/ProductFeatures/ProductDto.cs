using learning_ms.Web.Domain.ProductAggregate;

namespace learning_ms.Web.ProductFeatures;
public record ProductDto(ProductId Id, string Name, decimal UnitPrice);
