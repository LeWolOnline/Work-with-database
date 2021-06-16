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
  public partial class Patients : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      readDistricts();
      readData();
      ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:initElements(); ", true);
    }
    private void readData(string select = "")
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(_Default.SQLconnection))
      {
        string quType = select != "" ? (" AND districts.District = '" + select + "'") : ("");
        string query = "SELECT patients.PolicyNumber, patients.Fio, patients.Year, districts.District " +
          "FROM hospital.patients, hospital.districts " +
          "WHERE patients.PolicyNumber = districts.PolicyNumber" + quType + ";";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3)));
            i++;
          }
        }
      }
      elementsRepeater.DataSource = values;
      elementsRepeater.DataBind();
    }
    private void readDistricts()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection("Server=localhost;User ID=root;Password=2000;Database=hospital")) //LeWol/root
      {
        string query = "SELECT DISTINCT districts.District FROM hospital.districts;";
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
      districtsRepeater.DataSource = values;
      districtsRepeater.DataBind();
    }


    public void getElementInfo(Object sender, EventArgs e)
    {
      string id = hiElementId.Value;

      using (var connection = new MySqlConnection(_Default.SQLconnection))
      {
        connection.Open();
        string commandText = "SELECT patients.Fio, patients.Year, patients.PolicyNumber, patients.Number, districts.District, patients.Address, patients.Sign, patients.Department " +
          "FROM hospital.patients, hospital.districts " +
          "WHERE patients.PolicyNumber = districts.PolicyNumber " +
          "AND patients.PolicyNumber = '" + id.ToString() + "';";
        using (var command = new MySqlCommand(commandText, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            patFio.Value = reader.GetString(0);
            patYear.Value = reader.GetInt32(1).ToString();
            patId.Value = reader.GetString(2);
            patCart.Value = reader.GetString(3);
            patDistrict.Value = reader.GetString(4);
            patAddress.Value = reader.GetString(5);
            patWorker.Checked = reader.GetInt32(6) > 0;
            patDepartment.Value = reader.GetInt32(6) > 0 ? reader.GetString(7) : "";
          }
        }
      }
    }
    public void saveValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(_Default.SQLconnection))
      {
        connection.Open();
        string commandText = "UPDATE hospital.patients, hospital.districts" +
          " SET patients.Fio = '" + patFio.Value +
          "', patients.Year = " + int.Parse(patYear.Value) +
          ", patients.PolicyNumber = '" + patId.Value +
          "', patients.Number = '" + patCart.Value +
          "', districts.District = '" + patDistrict.Value +
          "', patients.Address = '" + patAddress.Value +
          "', patients.Sign = '" + patWorker.Checked.ToString() +
          "', patients.Department = '" + patDepartment.Value +
          " WHERE patients.PolicyNumber = districts.PolicyNumber" +
          " AND patients.PolicyNumber = " + int.Parse(hiElementId.Value) + ";";
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
    private class ElementData
    {
      public ElementData(string id = null, string fio = null, int year = 0, string district = null)
      {
        Fio = fio;
        Age = DateTime.Now.Year - year;
        District = district;
        Id = id;
      }
      public ElementData(string type = null)
      {
        District = type;
      }

      public string Fio { get; }
      public string Id { get; }
      public int Age { get; }
      public string District { get; }
    }
  }
}