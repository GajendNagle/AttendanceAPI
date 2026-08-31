using PMPoshanWithAngular.Server.Data.CustomeModels.Reponse;
using System.Reflection;

namespace PMPoshanWithAngular.Server.Data.Constants
{
    /// <summary>
    /// Extension methods for <see cref="ServiceResponseClassifier"/>.
    /// </summary>
    public static class ServiceResponseClassifierExtensions
    {
        /// <summary>
        /// Returns the string value of the matching static field in
        /// <see cref="ServiceResponseConstants"/> whose name equals the enum member name.
        /// Falls back to the enum name itself if no matching property is found.
        /// </summary>
        public static string ToResponseString(this ServiceResponseClassifier classifier)
        {
            string propertyName = classifier.ToString();

            FieldInfo? field = typeof(ServiceResponseConstants)
                .GetField(propertyName, BindingFlags.Public | BindingFlags.Static);

            if (field != null)
                return field.GetValue(null) as string ?? propertyName;

            PropertyInfo? property = typeof(ServiceResponseConstants)
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);

            if (property != null)
                return property.GetValue(null) as string ?? propertyName;

            return propertyName; // fallback: return enum name as string
        }
    }
}