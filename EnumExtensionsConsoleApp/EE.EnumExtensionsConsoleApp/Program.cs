using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE.EnumExtensionsConsoleApp
{
    //Run the app with CTRL+F5 and it will remin open 
    //When pressing F5 the app will close autometically. We can either do Console.ReadLine(); or Console.ReadKey(); and wait.
    class Program
    {
        static void Main(string[] args)
        {
            var enumString = SampleEnum.SampleOne.ToString();
            $"Using .ToString() To Get enum string: {enumString}".WriteNewLine(WriteLine.ColorCyan);
            "".RevertColor();
            "".EmptyLine();

            "Working with Description attribute".WriteNewLine();

            var enumDescription = SampleEnum.SampleOne.GetDescriptionAttribute();
            $"Getting the string in Description attribute: {enumDescription}".WriteNewLine(WriteLine.ColorYellow);

            var fromDescription = enumDescription.FromDescriptionAttribute<SampleEnum>();
            $"Getting the enum from string in Description attribute: {(int)fromDescription.GetValueOrDefault()} - {fromDescription.GetValueOrDefault()}".WriteNewLine(WriteLine.ColorCyan);

            var isInEnumUsingString = enumString.IsInEnum<SampleEnum>();
            $"Checking '{enumString}' is in enum: {isInEnumUsingString.ToString()}".WriteNewLine(WriteLine.ColorYellow);

            var fromEnumString = enumString.ToEnum<SampleEnum>();
            $"Getting the enum from enum string '{enumString}': {(int)fromEnumString.GetValueOrDefault()} - {fromEnumString.GetValueOrDefault()}".WriteNewLine(WriteLine.ColorCyan);

            var fromEnumInt = ((int)fromEnumString).ToEnum<SampleEnum>();
            $"Getting the enum from enum int value '{(int)fromEnumString}': {(int)fromEnumInt.GetValueOrDefault()} - {fromEnumInt.GetValueOrDefault()}".WriteNewLine(WriteLine.ColorYellow);

            "".RevertColor();
            "".EmptyLine();
            "Working with StringValue attribute".WriteNewLine();

            var enumByStringValue = SampleEnum.SampleTwo.GetStringValueAttibute();
            $"Getting the string in StringValue attribute: {SampleEnum.SampleTwo.ToString()}: {enumByStringValue}".WriteNewLine(WriteLine.ColorCyan);

            var enumFromStringValue = enumByStringValue.FromEnumByStringValueAttribute<SampleEnum>();
            $"Getting the enum from StringValue attribute: {enumByStringValue}: {(int)enumFromStringValue} - {enumFromStringValue.ToString()}".WriteNewLine(WriteLine.ColorYellow);

            "".EmptyLine();
            "".EmptyLine();
            "Press any key to exit >>".WriteSameLine(WriteLine.ColorRed);
            Console.ReadKey();
        }
    }

    public enum SampleEnum
    {
        [Description("ViaDescription_One")]
        [StringValue("ViaStringValue_One")]
        SampleOne = 0,

        [Description("ViaDescription_Two")]
        [StringValue("ViaStringValue_Two")]
        SampleTwo = 1
    }
}
