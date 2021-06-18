using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using MySqlConnector;

namespace Work_with_database
{
  public static class connectToDB
  {
    //SET SQL_SAFE_UPDATES = 0;
    public static String SQLconnection = "Server=localhost;User ID=root;Password=2000;Database=hospital";
    public static string SafeGetString(this MySqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetValue(colIndex).ToString();
      return string.Empty;
    }
    public static string SafeGetString(this MySqlDataReader reader, string colName)
    {
      int colIndex = reader.GetOrdinal(colName);
      if (!reader.IsDBNull(colIndex) && colIndex >= 0)
        return reader.GetValue(colIndex).ToString();
      return string.Empty;
    }
    public static int? ParseInt(string value)
    {
      int? parseValue;
      try { parseValue = int.Parse(value); }
      catch { parseValue = null; }
      return parseValue;
    }
    public static string ParseIntReturnString(string value)
    {
      try { return int.Parse(value).ToString(); }
      catch { return "Null"; }
    }
  }
}