using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Common.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayString(this Enum value, DisplayAttributeProperties prop)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                switch (prop)
                {
                    case DisplayAttributeProperties.Description: return attributes[0].Description;
                    case DisplayAttributeProperties.GroupName: return attributes[0].GroupName;
                    case DisplayAttributeProperties.Name: return attributes[0].Name;
                    case DisplayAttributeProperties.Order: return attributes[0].Order.ToString();
                    case DisplayAttributeProperties.Prompt: return attributes[0].Prompt;
                    case DisplayAttributeProperties.ShortName: return attributes[0].ShortName;
                    default:
                        return "";
                }
            else
                return "";
        }

        public static string GetDescriptionString(this Enum value)
        {
            if (value != null)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            return string.Empty;
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name) // I prefer to get attributes this way
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }

    public enum DisplayAttributeProperties
    {
        Description, GroupName, Name, Order, Prompt, ShortName
    }
}
