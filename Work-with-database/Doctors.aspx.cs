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
  public partial class Doctors : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      readTypes();
      readData();
      ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:initElements(); ", true);
    }

    private void readData(string select = "")
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(_Default.SQLconnection)) 
      {
        string quType = select != "" ? (" AND types.Type = '" + select + "'") : ("");
        string query = "SELECT doctors.LastName, doctors.FirstName, doctors.Patronymic, rooms.Room, types.Type, doctors.DoctorID " +
          "FROM hospital.doctors, hospital.rooms, hospital.types " +
          "WHERE doctors.DoctorID = rooms.DoctorID AND doctors.DoctorID = types.DoctorID" + quType + ";";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5)));
            i++;
          }
        }
      }
      elementsRepeater.DataSource = values;
      elementsRepeater.DataBind();
    }
    private void readTypes()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(_Default.SQLconnection))
      {
        string query = "SELECT DISTINCT types.Type FROM hospital.types;";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(reader.GetString(0)));
            i++;
          }
        }
      }
      typesRepeater.DataSource = values;
      typesRepeater.DataBind();
    }
    public void getElementInfo(Object sender, EventArgs e)
    {
      int id = int.Parse(hiElementId.Value);

      using (var connection = new MySqlConnection(_Default.SQLconnection))
      {
        connection.Open();
        string commandText = "SELECT doctors.LastName, doctors.FirstName, doctors.Patronymic, doctors.Born, doctors.Phone, " +
          "doctors.University, doctors.Experience, types.Type, rooms.Room " +
          "FROM hospital.doctors, hospital.types, hospital.rooms " +
          "WHERE doctors.DoctorID = types.DoctorID AND doctors.DoctorID = rooms.DoctorID " +
          "AND doctors.DoctorID = " + id.ToString() + ";";
        using (var command = new MySqlCommand(commandText, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            docLastName.Value = reader.GetString(0);
            docFirstName.Value = reader.GetString(1);
            docPatronymic.Value = reader.GetString(2);
            docYear.Value = reader.GetInt32(3).ToString();
            docPhone.Value = reader.GetString(4);
            docUniversity.Value = reader.GetString(5);
            docExperience.Value = reader.GetInt32(6).ToString();
            docType.Value = reader.GetString(7);
            docRoom.Value = reader.GetInt32(8).ToString();
          }
        }
      }
    }
    public void saveValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(_Default.SQLconnection))
      {
        connection.Open();
        string commandText = "UPDATE hospital.doctors, hospital.types, hospital.rooms" + 
          " SET doctors.LastName = '" + docLastName.Value + 
          "', doctors.FirstName = '" + docFirstName.Value + 
          "', doctors.Patronymic = '" + docPatronymic.Value + 
          "', doctors.Born = " + int.Parse(docYear.Value) + 
          ", doctors.Phone = '" + docPhone.Value +
          "', doctors.University = '" + docUniversity.Value +
          "', doctors.Experience = " + int.Parse(docExperience.Value) + 
          ", types.Type = '" + docType.Value +
          "', rooms.Room = " + int.Parse(docRoom.Value) +
          " WHERE doctors.DoctorID = types.DoctorID AND doctors.DoctorID = rooms.DoctorID" +
          " AND doctors.DoctorID = " + int.Parse(hiElementId.Value) + ";";
        Console.WriteLine(commandText);
        using (var command = new MySqlCommand(commandText, connection))
          command.ExecuteReader();
      }
      readData();
    }
    public void selectUpdate(Object sender, EventArgs e)
    {
      string val = hiSelect.Value;
      readData(val);
    }
    public void addNewElement(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(_Default.SQLconnection))
      {
        connection.Open();
        int index = 0;
        string query = "SELECT DoctorID FROM hospital.doctors;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            index = reader.GetInt32(0) > index ? reader.GetInt32(0) : index;
        index += 1;
        //string commandText = "INSERT INTO hospital.doctors (DoctorID, LastName, FirstName, Patronymic, Room, Type) VALUES (" + index + ", ' ', ' ', ' ', ' ', ' ');";
        //using (var command = new MySqlCommand(commandText, connection))
        //  command.ExecuteReader();
        //hiSelect.Value = index.ToString();
        readData(index.ToString());
      }
    }
    private class ElementData
    {
      public ElementData(string lastName = null, string firstName = null, string patronymic = null
        , int cabinet = 0, string type = null, int id = 0)
      {
        Fio = lastName + " " + firstName + " " + patronymic;
        Cabinet = cabinet;
        DocType = type;
        Id = id;
      }
      public ElementData(string type = null)
      {
        DocType = type;
      }

      public string Fio { get; }
      public int Id { get; }
      public int Cabinet { get; }
      public string DocType { get; }
    }
  }
}