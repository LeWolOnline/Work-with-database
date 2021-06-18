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
      readDoctors();
      ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:initElements(); ", true);
    }
    private void readDoctors()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        string query = "SELECT doctors.DoctorID, doctors.LastName, doctors.FirstName, doctors.Patronymic FROM hospital.doctors;";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(connectToDB.SafeGetString(reader, 0)
              , connectToDB.SafeGetString(reader, 1)
              , connectToDB.SafeGetString(reader, 2)
              , connectToDB.SafeGetString(reader, 3)));
            i++;
          }
        }
      }
      typesRepeater.DataSource = values;
      typesRepeater.DataBind();
    }

    public void saveValue(Object sender, EventArgs e)
    {

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string[] policyNumbers = new string[] { };
        string query = "SELECT PolicyNumber FROM hospital.patients;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            policyNumbers = policyNumbers.Append(reader.GetString(0)).ToArray();
        if (policyNumbers.Contains(inputPolicyNumber.Value))
        {
          validPolicyNumber.Visible = false;
        }
        else
        {
          validPolicyNumber.Visible = true;
          return;
        }

        int index = 0;
        query = "SELECT TreatyID FROM hospital.appointments;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            index = reader.GetInt32(0) > index ? reader.GetInt32(0) : index;

        query = "INSERT INTO hospital.appointments (TreatyID, DateStart, TimeStart, DoctorID, PolicyNumber, Comment) " +
          "VALUES (" + (index + 1).ToString() + ", '" + inputDate.Value + "', '" + inputTime.Value + "', '" + hiSelect.Value + "', '" + inputPolicyNumber.Value + "', ' ');";
        query += "\nINSERT INTO hospital.treaties (TreatyID, Cost, Summa) VALUES (" + (index + 1).ToString() + ", 0, 0);";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        inputDate.Value = "";
        inputTime.SelectedIndex = 0;
        inputPolicyNumber.Value = "";
      }
    }

    private class ElementData
    {
      public ElementData(string id = null, string lastName = null, string firstName = null, string patronymic = null)
      {
        int? parseId;
        try { parseId = int.Parse(id); }
        catch { parseId = null; }
        Fio = lastName + " " + firstName + " " + patronymic;
        Id = parseId;
      }

      public string Fio { get; }
      public int? Id { get; }
    }
  }
}