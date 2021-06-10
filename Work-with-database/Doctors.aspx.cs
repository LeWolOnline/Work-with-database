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
      if (!IsPostBack)
      {
        readTypes();
        readData();
      }
    }
    private void readData()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection("Server=localhost;User ID=root;Password=2000;Database=hospital")) //LeWol/root
      {
        string query = "SELECT doctors.LastName, doctors.FirstName, doctors.Patronymic, rooms.Room, types.Type, doctors.DoctorID " +
          "FROM hospital.doctors, hospital.rooms, hospital.types " +
          "WHERE doctors.DoctorID = rooms.DoctorID AND doctors.DoctorID = types.DoctorID;";
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

      using (var connection = new MySqlConnection("Server=localhost;User ID=root;Password=2000;Database=hospital")) //LeWol/root
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
    //public void getMachineInfo(Object sender, EventArgs e)
    //{
    //  int id = int.Parse(hiMachineId.Value);

    //  using (var connection = new MySqlConnection("Server=localhost;User ID=LeWol;Password=2000;Database=curse"))
    //  {
    //    connection.Open();
    //    string commandText = "SELECT * FROM curse.machines WHERE id = " + id.ToString() + ";";
    //    using (var command = new MySqlCommand(commandText, connection))
    //    using (var reader = command.ExecuteReader())
    //    {
    //      while (reader.Read())
    //      {
    //        machineSPK.Value = reader.GetString(1);
    //        machineId.InnerText = reader.GetString(3);
    //        machineValue.Value = reader.GetString(2);
    //        //hiMachineParam3.Value = reader.GetString(0);
    //        //hiMachineParam4.Value = reader.GetString(0);
    //        //hiMachineParam5.Value = reader.GetString(0);
    //      }
    //    }
    //  }
    //  //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Javascript", "javascript:refreshRightPanel(); ", true);
    //}

    //public void saveSpkNumber(Object sender, EventArgs e)
    //{
    //  int id = int.Parse(hiMachineId.Value);
    //  int Spk = int.Parse(machineSPK.Value);

    //  using (var connection = new MySqlConnection("Server=localhost;User ID=LeWol;Password=2000;Database=curse"))
    //  {
    //    connection.Open();
    //    string commandText = "UPDATE `curse`.`machines` SET `inventory_number` = '" + Spk + "' WHERE (`id` = '" + id + "');";
    //    using (var command = new MySqlCommand(commandText, connection))
    //      command.ExecuteReader();
    //  }
    //  readData();
    //}

    //public void saveValue(Object sender, EventArgs e)
    //{
    //  int id = int.Parse(hiMachineId.Value);
    //  int Spk = int.Parse(machineValue.Value);

    //  using (var connection = new MySqlConnection("Server=localhost;User ID=LeWol;Password=2000;Database=curse"))
    //  {
    //    connection.Open();
    //    string commandText = "UPDATE `curse`.`machines` SET `value` = '" + Spk + "' WHERE (`id` = '" + id + "');";
    //    using (var command = new MySqlCommand(commandText, connection))
    //      command.ExecuteReader();
    //  }
    //  readData();
    //}

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