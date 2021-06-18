using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MySqlConnector;
using System.Globalization;
using TemplateEngine.Docx;
using System.IO;
using System.Drawing.Printing;

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
            values.Add(new ElementData(
              connectToDB.SafeGetString(reader, 0)
              , connectToDB.SafeGetString(reader, 1)
              , connectToDB.SafeGetString(reader, 2)
              , connectToDB.SafeGetString(reader, 3)
              , connectToDB.SafeGetString(reader, 4)
              , connectToDB.SafeGetString(reader, 5)
              , connectToDB.SafeGetString(reader, 6)
              , reader.GetDateTime(7)
              , connectToDB.SafeGetString(reader, 8)));
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
        string query = "SELECT appointments.DateStart, appointments.TimeStart, patients.Fio, patients.Year, patients.PolicyNumber, patients.Number, patients.Address, districts.District, doctors.LastName, doctors.FirstName, doctors.Patronymic, rooms.Room, patients.Sign, treaties.Cost, appointments.Comment " +
          "FROM hospital.patients, hospital.appointments, hospital.doctors, hospital.rooms, hospital.treaties, hospital.districts, hospital.exempts " +
          "WHERE appointments.PolicyNumber = patients.PolicyNumber AND appointments.DoctorID = doctors.DoctorID AND appointments.TreatyID = treaties.TreatyID AND patients.PolicyNumber = districts.PolicyNumber AND doctors.DoctorID = rooms.DoctorID AND exempts.ExemptID = patients.ExemptID " +
          "AND appointments.TreatyID = '" + id.ToString() + "';";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            hiPatientId.Value = connectToDB.SafeGetString(reader, 4);
            appDateTime.InnerText = "Время записи: " + reader.GetDateTime(0).ToString("dd'/'MM'/'yyyy", new CultureInfo("ru-RU")) + " " + connectToDB.SafeGetString(reader, 1);
            patFio.InnerText = connectToDB.SafeGetString(reader, 2);
            patYear.InnerText = "Год рождения: " + connectToDB.SafeGetString(reader, 3);
            patId.InnerText = "Полис: " + connectToDB.SafeGetString(reader, 4);
            patCart.InnerText = "Номер карточки: " + connectToDB.SafeGetString(reader, 5);
            patAddress.InnerText = "Адрес: " + connectToDB.SafeGetString(reader, 6);
            patDistrict.InnerText = connectToDB.SafeGetString(reader, 7) + " район";
            docFio.InnerText = connectToDB.SafeGetString(reader, 8) + " " + connectToDB.SafeGetString(reader, 9) + " " + connectToDB.SafeGetString(reader, 10);
            docRoom.InnerText = "Кабинет: " + connectToDB.SafeGetString(reader, 11);
            bool isWorker = connectToDB.ParseInt(connectToDB.SafeGetString(reader, 12)) > 0;
            blockIsWorker.Visible = isWorker;
            blockPay.Visible = blockPayBtn.Visible = !isWorker;
            exeSumm.Value = isWorker ? "" : connectToDB.SafeGetString(reader, 13);
            appComment.Value = connectToDB.SafeGetString(reader, 14);
          }
        }
      }
    }
    public void saveValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "UPDATE hospital.appointments" +
          " SET Comment = '" + appComment.Value +
          "' WHERE appointments.TreatyID = " + connectToDB.ParseIntReturnString(hiElementId.Value) + ";";
        Console.WriteLine(query);
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
      }
      readData();
    }
    public void printPatientFile(Object sender, EventArgs e)
    {
      string sFileName = HttpContext.Current.Server.MapPath(@"~/Files/МедицинскаяКартаПациента.docx");
      string sCopyFileName = HttpContext.Current.Server.MapPath(@"~/Files/МедицинскаяКартаПациентаКопия.docx");
      File.Copy(sFileName, sCopyFileName, true);

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "SELECT patients.Fio, patients.Year, patients.PolicyNumber, patients.Number, districts.District, patients.Address, patients.Sign, patients.Department, exempts.ExemptType " +
          "FROM hospital.patients, hospital.districts, exempts " +
          "WHERE patients.PolicyNumber = districts.PolicyNumber AND patients.ExemptID = exempts.ExemptID " +
          "AND patients.PolicyNumber = '" + hiPatientId.Value + "';";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var valuesToFill = new TemplateEngine.Docx.Content(
              new FieldContent("NumberOfPatientFile", connectToDB.SafeGetString(reader, 3))
              , new FieldContent("Fio", connectToDB.SafeGetString(reader, 0))
              , new FieldContent("Year", connectToDB.SafeGetString(reader, 1))
              , new FieldContent("PolicyNumber", connectToDB.SafeGetString(reader, 2))
              , new FieldContent("District", connectToDB.SafeGetString(reader, 4))
              , new FieldContent("Address", connectToDB.SafeGetString(reader, 5))
              , new FieldContent("IsWorker", (connectToDB.ParseInt(connectToDB.SafeGetString(reader, 6)) > 0) ? "Да" : "Нет")
              , new FieldContent("Department", (connectToDB.ParseInt(connectToDB.SafeGetString(reader, 6)) > 0) ? connectToDB.SafeGetString(reader, 7) : "Нет")
              , new FieldContent("Exempt", connectToDB.SafeGetString(reader, 8))
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
        hiPrintPatientFile.Value = sCopyFileName;

        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "javascript:printDoc(); ", true);
      }
    }
    private class ElementData
    {
      public ElementData(string id, string lastName, string firstName, string patronymic
        , string cabinet, string patientFio, string patientYear, DateTime data, string time)
      {
        Id = id;
        DocFio = lastName + " " + firstName + " " + patronymic;
        DocCabinet = cabinet;
        PatientFio = patientFio;
        PatientAge = (DateTime.Now.Year - connectToDB.ParseInt(patientYear)).ToString();
        Data = data.ToString("dd'/'MM'/'yyyy", new CultureInfo("ru-RU"));
        Time = time;
      }
      public string Id { get; }
      public string DocFio { get; }
      public string DocCabinet { get; }
      public string PatientFio { get; }
      public string PatientAge { get; }
      public string Data { get; }
      public string Time { get; }
    }
  }
}