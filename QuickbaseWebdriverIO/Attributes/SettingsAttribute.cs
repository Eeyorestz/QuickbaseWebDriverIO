using System;

namespace QuickbaseWebdriverIO.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SettingsAttribute : Attribute
    {
        public SettingsAttribute(string name) => SettingName = name;

        public string SettingName { get; set; }
    }
}
