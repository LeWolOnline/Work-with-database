using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MySqlConnector;
using System.Globalization;
using System.IO;
using System.Drawing.Printing;

namespace Work_with_database
{
  public partial class Documents : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      readData();
      ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:initElements(); ", true);
    }

    private void readData()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        string query = "SELECT Record, Flat, DateDoc, FioHost FROM documents;";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(
              connectToDB.SafeGetString(reader, 0)
              , connectToDB.SafeGetString(reader, 1)
              , reader.GetDateTime(2)
              , connectToDB.SafeGetString(reader, 3)));
            i++;
          }
        }
      }
      elementsRepeater.DataSource = values;
      elementsRepeater.DataBind();
    }

    public void getElementInfo(Object sender, EventArgs e)
    {
      string id = hiElementId.Value;

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "SELECT Record, Flat, DateDoc, Document, FioHost, Passport, Born, Part FROM documents " +
          "WHERE Record = '" + id.ToString() + "';";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            Record.InnerText = "Номер записи о праве собственности: " + connectToDB.SafeGetString(reader, 0);
            Flat.InnerText = "Номер квартиры: " + connectToDB.SafeGetString(reader, 1);
            DateDoc.InnerText = "Дата документа о собственности: " + reader.GetDateTime(2).ToString("dd'/'MM'/'yyyy", new CultureInfo("ru-RU"));
            Document.InnerText = "Документ на право собственности: " + connectToDB.SafeGetString(reader, 3);
            FioHost.InnerText = "ФИО собственника: " + connectToDB.SafeGetString(reader, 4);
            Passport.InnerText = "Номер паспорта: " + connectToDB.SafeGetString(reader, 5);
            Born.InnerText = "Год рождения собственника: " + connectToDB.SafeGetString(reader, 6);
            Part.InnerText = "Принадлежащая ему доля, %: " + connectToDB.SafeGetString(reader, 7);
          }
        }
      }
    }
    
    private class ElementData
    {
      public ElementData(string record, string flat, DateTime data, string fioHost)
      {
        Record = record;
        Flat = flat;
        FioHost = fioHost;
        Data = data.ToString("dd'/'MM'/'yyyy", new CultureInfo("ru-RU"));
      }
      public string Record { get; }
      public string Flat { get; }
      public string FioHost { get; }
      public string Data { get; }
    }
  }
}