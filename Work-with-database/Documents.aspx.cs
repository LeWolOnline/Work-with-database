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
using TemplateEngine.Docx;

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
      getDocument();
    }
    public void getDocument()
    {
      string sFileName = HttpContext.Current.Server.MapPath(@"~/Files/ШаблонДоговора.docx");
      string sCopyFileName = HttpContext.Current.Server.MapPath(@"~/Files/Договор.docx");
      File.Copy(sFileName, sCopyFileName, true);

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "SELECT Record, Flat, DateDoc, Document, FioHost, Passport, Born, Part FROM documents " +
          "WHERE Record = '" + hiElementId.Value + "';";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var valuesToFill = new TemplateEngine.Docx.Content(
              new FieldContent("NumberOfDocument", connectToDB.SafeGetString(reader, 0))
              , new FieldContent("Flat", connectToDB.SafeGetString(reader, 1))
              , new FieldContent("Date", reader.GetDateTime(2).ToString("dd'/'MM'/'yyyy", new CultureInfo("ru-RU")))
              , new FieldContent("Document", connectToDB.SafeGetString(reader, 3))
              , new FieldContent("Fio", connectToDB.SafeGetString(reader, 4))
              , new FieldContent("Passport", connectToDB.SafeGetString(reader, 5))
              , new FieldContent("Year", connectToDB.SafeGetString(reader, 6))
              , new FieldContent("Part", connectToDB.SafeGetString(reader, 7))
            );
            try
            {
              using (var outputDocument = new TemplateProcessor(sCopyFileName)
              .SetRemoveContentControls(true))
              {
                outputDocument.FillContent(valuesToFill);
                outputDocument.SaveChanges();
              }
            }
            catch { Console.WriteLine("Возникла непредвиденная ошибка"); }
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