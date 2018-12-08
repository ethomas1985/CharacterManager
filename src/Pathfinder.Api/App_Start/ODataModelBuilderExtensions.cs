using System;
using Microsoft.AspNet.OData.Builder;

namespace Pathfinder.Api {
    public static class ODataModelBuilderExtensions {
        public static ODataModelBuilder AddEnumTypeAndValues<T>(this ODataModelBuilder builder)
        {
            var enumTypeConfig = builder.EnumType<T>();
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                enumTypeConfig.Member(value);
            }
            return builder;
        }
    }
}
