using Microsoft.AspNetCore.Mvc;
namespace learning_ms.Web.Infrastructure.Persistence.Conventions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
public class RoutePrefixConvention : IApplicationModelConvention
{
  private readonly AttributeRouteModel _routePrefix;

  public RoutePrefixConvention(string prefix)
  {
    _routePrefix = new AttributeRouteModel(new RouteAttribute(prefix));
  }

  public void Apply(ApplicationModel application)
  {
    foreach (var controller in application.Controllers)
    {
      foreach (var selector in controller.Selectors)
      {
        selector.AttributeRouteModel = selector.AttributeRouteModel != null
          ? AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel)
          : _routePrefix;
      }
    }
  }
}
