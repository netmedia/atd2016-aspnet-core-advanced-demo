using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ATD2016.Conventions.ControllerConvention
{
    public class FeatureConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.Properties.Add("feature", GetFeatureName(controller.ControllerType));
        }
        private string GetFeatureName(TypeInfo controllerType)
        {
            string[] typeNameParts = controllerType.FullName.Split('.');

            if (typeNameParts.Any(typeNamePart => typeNamePart == "Features") == false) return "";

            return typeNameParts
                        .SkipWhile(typeNamePart => typeNamePart.Equals("features", StringComparison.CurrentCultureIgnoreCase) == false)
                        .Skip(1)
                        .Take(1)
                        .FirstOrDefault();
        }
    }
}
