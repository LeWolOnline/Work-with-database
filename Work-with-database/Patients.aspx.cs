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

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
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
            values.Add(new ElementData(connectToDB.SafeGetString(reader, 0)
              , connectToDB.SafeGetString(reader, 1)
              , connectToDB.SafeGetString(reader, 2)
              , connectToDB.SafeGetString(reader, 3)));
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

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        string query = "SELECT DISTINCT districts.District FROM hospital.districts;";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(connectToDB.SafeGetString(reader, 0)));
            i++;
          }
        }
      }
      districtsRepeater.DataSource = values;
      districtsRepeater.DataBind();
    }


    public void getElementInfo(Object sender, EventArgs e)
    {
      getElementInfo();
    }
    private void getElementInfo()
    {
      string id = hiElementId.Value;

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "SELECT patients.Fio, patients.Year, patients.PolicyNumber, patients.Number, districts.District, patients.Address, patients.Sign, patients.Department " +
          "FROM hospital.patients, hospital.districts " +
          "WHERE patients.PolicyNumber = districts.PolicyNumber " +
          "AND patients.PolicyNumber = '" + id.ToString() + "';";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            patFio.Value = connectToDB.SafeGetString(reader, 0);
            patYear.Value = connectToDB.SafeGetString(reader, 1);
            patId.Value = connectToDB.SafeGetString(reader, 2);
            patCart.Value = connectToDB.SafeGetString(reader, 3);
            patDistrict.Value = connectToDB.SafeGetString(reader, 4);
            patAddress.Value = connectToDB.SafeGetString(reader, 5);
            patWorker.Checked = connectToDB.ParseInt(connectToDB.SafeGetString(reader, 6)) > 0;
            patDepartment.Value = connectToDB.ParseInt(connectToDB.SafeGetString(reader, 6)) > 0 ? connectToDB.SafeGetString(reader, 7) : "";
          }
        }
      }

      btnDelete.Attributes.Add("OnClick", "return confirm('Уверены?');");
    }
    public void saveValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "UPDATE hospital.patients, hospital.districts" +
          " SET patients.Fio = '" + patFio.Value +
          "', patients.Year = " + connectToDB.ParseIntReturnString(patYear.Value) +
          ", patients.PolicyNumber = '" + patId.Value +
          "', patients.Number = '" + patCart.Value +
          "', districts.District = '" + patDistrict.Value +
          "', patients.Address = '" + patAddress.Value +
          "', patients.Sign = '" + patWorker.Checked.ToString() +
          "', patients.Department = '" + patDepartment.Value +
          " WHERE patients.PolicyNumber = districts.PolicyNumber" +
          " AND patients.PolicyNumber = " + connectToDB.ParseIntReturnString(hiElementId.Value) + ";";
        Console.WriteLine(query);
        using (var command = new MySqlCommand(query, connection))
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
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string patientNumber = newPatNumber.Value;
        string patientFio = newPatFio.Value;

        string[] policyNumbers = new string[] { };
        string query = "SELECT PolicyNumber FROM hospital.patients;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            policyNumbers = policyNumbers.Append(reader.GetString(0)).ToArray();
        if (!policyNumbers.Contains(patientNumber))
        {
          validNewPolicyNumber.Visible = false;
        }
        else
        {
          validNewPolicyNumber.Visible = true;
          return;
        }

        query = "INSERT INTO hospital.patients (PolicyNumber, Fio) VALUES (" + patientNumber + ", '" + patientFio + "');";
        query += "\nINSERT INTO hospital.districts (PolicyNumber) VALUES (" + patientNumber + ");";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        hiElementId.Value = patientNumber.ToString();
        readData();
        getElementInfo();
      }
    }

    public void deleteValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "DELETE FROM hospital.patients WHERE (PolicyNumber = '" + hiElementId.Value + "');";
        query += "\nDELETE FROM hospital.districts WHERE (PolicyNumber = '" + hiElementId.Value + "');";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        connection.Close();

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
      }
    }
    private class ElementData
    {
      public ElementData(string id = null, string fio = null, string year = null, string district = null)
      {
        Fio = fio;
        Age = (DateTime.Now.Year - connectToDB.ParseInt(year)).ToString();
        District = district;
        Id = id;
      }
      public ElementData(string type = null)
      {
        District = type;
      }

      public string Fio { get; }
      public string Id { get; }
      public string Age { get; }
      public string District { get; }
    }
  }
}