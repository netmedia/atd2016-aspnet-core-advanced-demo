using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ATD2016.ApplicationParts.Models
{
    public class PublicModelTypes
    {
        public static IReadOnlyList<TypeInfo> Types => new List<TypeInfo>()
        {
            typeof(Customer).GetTypeInfo(),
            typeof(Order).GetTypeInfo(),
        };
    }
}
