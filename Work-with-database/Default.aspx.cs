using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MySqlConnector;

namespace Work_with_database
{
  public partial class _Default : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void saveValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string[] flatNumbers = new string[] { };
        string query = "SELECT Flat FROM flat;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            flatNumbers = flatNumbers.Append(connectToDB.SafeGetString(reader, 0)).ToArray();
        if (flatNumbers.Contains(inputFlatNumber.Value))
        {
          validPolicyNumber.Visible = false;
        }
        else
        {
          validPolicyNumber.Visible = true;
          return;
        }

        int index = 0;
        query = "SELECT Record FROM documents;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            index = reader.GetInt32(0) > index ? reader.GetInt32(0) : index;

        query = "INSERT INTO documents (Flat, Record, Document, DateDoc, FioHost, Passport, Born, Part) " +
          "VALUES (" + inputFlatNumber.Value + ", '" + (index + 1).ToString() + "', '" + inputDocType.Value + "', '" + inputDate.Value + "', '" +
          inputFio.Value + "', '" + inputPassport.Value + "', '" + inputYear.Value + "', '" + inputPart.Value + "');";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
      }
    }
  }
}