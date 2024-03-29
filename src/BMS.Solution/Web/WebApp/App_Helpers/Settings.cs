﻿#region Using

using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;

#endregion
#nullable disable
namespace WebApp
{
  /// <summary>
  ///     Provides access to the current application's configuration file.
  /// </summary>
  public static class Settings
  {
    /// <summary>
    ///     Specifies the default entry prefix value ("config").
    /// </summary>
    private const string Prefix = "config";


   

    public static readonly string App = GetValue<string>("App");
    public static readonly string AppName = GetValue<string>("AppName");
    public static readonly string AppFlavor = GetValue<string>("AppFlavor");
    public static readonly string AppFlavorSubscript = GetValue<string>("AppFlavorSubscript");
    public static readonly string Version = GetValue<string>("Version");
    public static readonly string ThemeVersion = GetValue<string>("ThemeVersion");
    public static readonly string IconPrefix = GetValue<string>("IconPrefix");
    public static readonly string Theme = GetValue<string>("Theme");

    public static readonly string Company = GetValue<string>("Company");
    public static readonly string ICP = GetValue<string>("ICP");
    /// <summary>
    ///     Gets the entry for the given key and prefix and retrieves its value as the specified type.
    ///     <para>If no prefix is specified the default prefix value ("config") will be used.</para>
    ///     <para>
    ///         <example>e.g. GetValue&lt;string&gt;("config", "SettingName")</example>
    ///     </para>
    ///     Would result in checking the configuration file for a key named: "config:SettingName"
    /// </summary>
    /// <typeparam name="T">The type of which the value will be converted into and returned.</typeparam>
    /// <param name="prefix">The prefix of the entry to locate.</param>
    /// <param name="key">The key of the entry to locate.</param>
    /// <returns>The value of the entry, or the default value, as the specified type.</returns>
    public static T GetValue<T>(string key, string prefix = Prefix) where T:notnull
    {
      var entry = string.Format("{0}:{1}", prefix, key);

      // Make sure the key represents a possible valid entry
      if (entry.IsNullOrWhiteSpace())
      {
        return default;
      }

      var value = ConfigurationManager.AppSettings[entry];

      // If the key is available but does not contain any value, return the default value of the specfied type
      if (value.IsNullOrWhiteSpace())
      {
        return default;
      }

      // In case the specified type is an enum, try to parse the entry as an enum value
      if (typeof(T).IsEnum)
      {
        return (T)Enum.Parse(typeof(T), value, true);
      }

      // In case the specified type is a bool and the entry value represents an integer
      if (typeof(T) == typeof(bool) && value.Is<int>())
      {
        // We convert to value to an integer first before changing the entry value to the specified type
        return (T)Convert.ChangeType(value.As<int>(), typeof(T));
      }

      // Change the entry value to the specified type
      return (T)Convert.ChangeType(value, typeof(T));
    }

    /// <summary>
    ///     To dynamical replace the appSettings "config:CurrentTheme"
    ///     Calling function such as Settings.SetValue<string>("config:CurrentTheme", "smart-style-0");
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">the key of the theme === config:CurrentTheme</param>
    /// <param name="value">the value of the theme === smart-style-{0}</param>
    /// <returns></returns>
    public static T SetValue<T>(string key, string value) where T : notnull
    {
      var config = WebConfigurationManager.OpenWebConfiguration("~");

      config.AppSettings.Settings[key].Value = value;
      config.Save(ConfigurationSaveMode.Modified);
      ConfigurationManager.RefreshSection("appSettings");

      return default;
    }

  }
}