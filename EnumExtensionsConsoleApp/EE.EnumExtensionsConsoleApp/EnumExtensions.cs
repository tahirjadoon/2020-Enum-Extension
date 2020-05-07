using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EE.EnumExtensionsConsoleApp
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Using StringValue Attribute: Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value">A enum property</param>
        /// <returns>Returns a text format of a enum property</returns>
        /// <example>
        /// string actionTypeValue = ActionType.StatusChanged.GetStringValue();
        /// </example>
        public static string GetStringValueAttibute(this Enum value)
        {
            return GetEnumStringValue(value);
        }

        /// <summary>
        /// Using StringValue Attribute: Will get the enum type value from a string
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Enum property in string format</param>
        /// <returns>Enum type of instance</returns>
        /// <example>
        /// <![CDATA[
        ///     ActionType actionType = Enum.GetEnumValue<ActionType>("StatusChanged");
        /// ]]>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException()">
        /// Thrown when parameter value does not match the enum string value attribute.
        /// </exception>
        public static T FromEnumByStringValueAttribute<T>(this string value) where T : struct
        {
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                if (string.Compare(value, GetEnumStringValue(enumValue), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return enumValue;
                }
            }
            throw new ArgumentOutOfRangeException(string.Format("{0}.{1}", typeof(T), value));
        }

        /// <summary>
        /// Using StringValue Attribute: Will get the enum type value from a string. A specific fault enum value will be return when any fault.
        /// </summary>
        /// <typeparam name="T">Type of enum object</typeparam>
        /// <param name="value">Enum string value</param>
        /// <param name="faultEnumValue">An enum value represents a fault status.</param>
        /// <returns>Return a converted enum value or a fault enum value.</returns>
        public static T FromEnumByStringValueAttribute<T>(this string value, T faultEnumValue) where T : struct
        {
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                if (string.Compare(value, GetEnumStringValue(enumValue), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return enumValue;
                }
            }
            return faultEnumValue;
        }

        private static string GetEnumStringValue(object value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs != null && attribs.Length > 0 ? attribs[0].StringValue : null;
        }
        
        /// <summary>
        /// Getting the Description attribute for the enum
        /// </summary>
        /// <param name="value">The enum</param>
        /// <returns>Returns enum</returns>
        public static string GetDescriptionAttribute(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // return description
            //return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : "Description Not Found";
            return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : "";
        }

        /// <summary>
        /// Gets the enum using the description attribute
        /// </summary>
        /// <typeparam name="T">The enum</typeparam>
        /// <param name="description">The description attribute value</param>
        /// <param name="isNullable">true means if not found then return null rather than throwing an error</param>
        /// <returns></returns>
        public static T? FromDescriptionAttribute<T>(this string description, bool isNullable = false) where T : struct
        {
            Type t = typeof(T);
            
            foreach (FieldInfo fi in t.GetFields())
            {
                var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes.Cast<DescriptionAttribute>().Any(attr => attr.Description.Equals(description)))
                {
                    return (T)fi.GetValue(null);
                }
            }
            if (isNullable)
                return null;
            throw new InvalidOperationException($"{description} is not an underlying value of the enum.");
        }

        /// <summary>
        /// Get the enum by its name string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The enum name</param>
        /// <param name="noMatchAsNull">true means if not found then return null rather than throwing an error</param>
        /// <returns>return enum</returns>
        public static T? GetEnumNullableValueByName<T>(this string value, bool noMatchAsNull = true) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                if (string.Compare(value, enumValue.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return enumValue;
                }
            }
            if (noMatchAsNull)
            {
                return null;
            }
            throw new ArgumentOutOfRangeException(string.Format("{0}.{1}", typeof(T), value));
        }

        public static bool IsInEnum<T>(this string enumString)
        {
            var isOk = Enum.IsDefined(typeof(T), enumString);
            return isOk;
        }

        public static bool IsInEnum<T>(this int enumValue)
        {
            var isOk = Enum.IsDefined(typeof(T), enumValue);
            return isOk;
        }

        public static T? ToEnum<T>(this string enumString, bool isNullable = false) where T : struct
        {
            if (string.IsNullOrWhiteSpace(enumString))
            {
                if (!isNullable)
                    throw new InvalidOperationException("Empty or null string being used for the enum");
                else
                    return null;
            }

            if (!enumString.IsInEnum<T>())
            {
                if (!isNullable)
                    throw new InvalidOperationException($"{enumString} is not an underlying value of the enum.");
                else
                    return null;
            }
            return (T)Enum.Parse(typeof(T), enumString, true);
        }

        public static T? ToEnum<T>(this int enumValue, bool isNullable = false) where T : struct
        {
            if (!enumValue.IsInEnum<T>())
            {
                if (!isNullable)
                    throw new InvalidOperationException($"{enumValue} is not an underlying value of the  enum.");
                else
                    return null;
            }
            return (T)(object)enumValue;
        }
    }

    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
    }
}
