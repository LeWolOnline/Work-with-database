using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MySqlConnector;
using System.Globalization;
//using Microsoft.Office.Interopt.Word

namespace Work_with_database
{
  public partial class Schedule : Page
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
        string query = "SELECT appointments.TreatyID, doctors.LastName, doctors.FirstName, doctors.Patronymic, " +
          "rooms.Room, patients.Fio, patients.Year, appointments.DateStart, appointments.TimeStart " +
          "FROM hospital.patients, hospital.appointments, hospital.doctors, hospital.rooms " +
          "WHERE patients.PolicyNumber = appointments.PolicyNumber " +
          "AND doctors.DoctorID = appointments.DoctorID " +
          "AND doctors.DoctorID = rooms.DoctorID " +
          "ORDER BY appointments.DateStart;";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)
              , reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetDateTime(7), reader.GetString(8)));
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
        string commandText = "SELECT appointments.DateStart, appointments.TimeStart, patients.Fio, patients.Year, patients.PolicyNumber, patients.Number, patients.Address, districts.District, doctors.LastName, doctors.FirstName, doctors.Patronymic, rooms.Room, patients.Sign, treaties.Cost, appointments.Comment " +
          "FROM hospital.patients, hospital.appointments, hospital.doctors, hospital.rooms, hospital.treaties, hospital.districts, hospital.exempts " +
          "WHERE appointments.PolicyNumber = patients.PolicyNumber AND appointments.DoctorID = doctors.DoctorID AND appointments.TreatyID = treaties.TreatyID AND patients.PolicyNumber = districts.PolicyNumber AND doctors.DoctorID = rooms.DoctorID AND exempts.ExemptID = patients.ExemptID " +
          "AND appointments.TreatyID = '" + id.ToString() + "';";
        using (var command = new MySqlCommand(commandText, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            appDateTime.InnerText = "Время записи: " + reader.GetDateTime(0).ToString("dd'/'MM'/'yyyy", new CultureInfo("ru-RU")) + " " +  reader.GetString(1);
            patFio.InnerText = reader.GetString(2);
            patYear.InnerText = "Год рождения: " + reader.GetInt32(3).ToString();
            patId.InnerText = "Полис: " + reader.GetString(4);
            patCart.InnerText = "Номер карточки: " + reader.GetString(5);
            patAddress.InnerText = "Адрес: " + reader.GetString(6);
            patDistrict.InnerText = reader.GetString(7) + " район";
            docFio.InnerText = reader.GetString(8) + " " + reader.GetString(9) + " " + reader.GetString(10);
            docRoom.InnerText = "Кабинет: " + reader.GetInt32(11).ToString();
            bool isWorker = reader.GetInt32(12) > 0;
            blockIsWorker.Visible = isWorker;
            blockPay.Visible = blockPayBtn.Visible = !isWorker;
            exeSumm.Value = isWorker ? "" : reader.GetDecimal(13).ToString();
            appComment.Value = reader.GetString(14);
          }
        }
      }
    }
    public void saveValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string commandText = "UPDATE hospital.appointments" +
          " SET Comment = '" + appComment.Value +
          "' WHERE appointments.TreatyID = " + int.Parse(hiElementId.Value) + ";";
        Console.WriteLine(commandText);
        using (var command = new MySqlCommand(commandText, connection))
          command.ExecuteReader();
      }
      readData();
    }
    private class ElementData
    {
      public ElementData(int id, string lastName, string firstName, string patronymic
        , int cabinet, string patientFio, int patientYear, DateTime data, string time)
      {
        Id = id;
        DocFio = lastName + " " + firstName + " " + patronymic;
        DocCabinet = cabinet;
        PatientFio = patientFio;
        PatientAge = DateTime.Now.Year - patientYear;
        Data = data.ToString("dd'/'MM'/'yyyy", new CultureInfo("ru-RU"));
        Time = time;
      }
      public int Id { get; }
      public string DocFio { get; }
      public int DocCabinet { get; }
      public string PatientFio { get; }
      public int PatientAge { get; }
      public string Data { get; }
      public string Time { get; }
    }
  }
}