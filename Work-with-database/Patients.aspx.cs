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
      if (!IsPostBack)
      {
        readDistricts();
        readData();
      }
    }
    private void readData()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection("Server=localhost;User ID=root;Password=2000;Database=hospital")) //LeWol/root
      {
        string query = "SELECT patients.PolicyNumber, patients.Fio, patients.Year, districts.District " +
          "FROM hospital.patients, hospital.districts " +
          "WHERE patients.PolicyNumber = districts.PolicyNumber;";
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