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
            values.Add(new ElementData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
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

        string commandText = "INSERT INTO hospital.appointments (TreatyID, DateStart, TimeStart, DoctorID, PolicyNumber, Comment) " +
          "VALUES (" + (index + 1).ToString() + ", '" + inputDate.Value + "', '" + inputTime.Value + "', '" + hiSelect.Value + "', '" + inputPolicyNumber.Value + "', ' ');";
        
        commandText += "\nINSERT INTO hospital.treaties (TreatyID, Cost, Summa) " +
          "VALUES (" + (index + 1).ToString() + ", 0, 0);";
        using (var command = new MySqlCommand(commandText, connection))
          command.ExecuteReader();
        inputDate.Value = "";
        inputTime.SelectedIndex = 0;
        inputPolicyNumber.Value = "";
      }
    }

    private class ElementData
    {
      public ElementData(int id = 0, string lastName = null, string firstName = null, string patronymic = null)
      {
        Fio = lastName + " " + firstName + " " + patronymic;
        Id = id;
      }

      public string Fio { get; }
      public int Id { get; }
      public int Cabinet { get; }
      public string DocType { get; }
    }
  }
}