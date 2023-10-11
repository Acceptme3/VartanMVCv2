using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace VartanMVCv2.Services
{
    public class AdminAreaAuthorization : IControllerModelConvention
    {
        private readonly string policy;
        private readonly string area;

        public AdminAreaAuthorization(string area, string policy)
        {
            this.policy = policy;
            this.area = area;
        }
        public void Apply(ControllerModel controller)
        {
            if (controller.Attributes.Any(a =>
            a is AreaAttribute && (a as AreaAttribute)!.RouteValue.Equals(area, StringComparison.OrdinalIgnoreCase))
                || controller.RouteValues.Any(r =>
                r.Key.Equals(area, StringComparison.OrdinalIgnoreCase) && r.Value!.Equals(area, StringComparison.OrdinalIgnoreCase)))
            {
                controller.Filters.Add(new AuthorizeFilter(policy));
            }
        }
    }
}
