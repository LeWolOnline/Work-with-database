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
  public partial class Schedule : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        readData();
      }
    }

    private void readData()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection("Server=localhost;User ID=root;Password=2000;Database=hospital")) //LeWol/root
      {
        string query = "SELECT appointments.TreatyID, doctors.LastName, doctors.FirstName, doctors.Patronymic, " +
          "rooms.Room, patients.Fio, patients.Year " +
          "FROM hospital.patients, hospital.appointments, hospital.doctors, hospital.rooms " +
          "WHERE patients.PolicyNumber = appointments.PolicyNumber " +
          "AND doctors.DoctorID = appointments.DoctorID " +
          "AND doctors.DoctorID = rooms.DoctorID; ";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)
              , reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6)));
            i++;
          }
        }
      }
      elementsRepeater.DataSource = values;
      elementsRepeater.DataBind();
    }

    private class ElementData
    {
      public ElementData(int id = 0, string lastName = null, string firstName = null, string patronymic = null
        , int cabinet = 0, string patientFio = null, int PatientYear = 0)
      {
        Id = id;
        DocFio = lastName + " " + firstName + " " + patronymic;
        DocCabinet = cabinet;
        PatientFio = patientFio;
        PatientAge = DateTime.Now.Year - PatientYear;
      }
      public int Id { get; }
      public string DocFio { get; }
      public int DocCabinet { get; }
      public string PatientFio { get; }
      public int PatientAge { get; }
    }
  }
}