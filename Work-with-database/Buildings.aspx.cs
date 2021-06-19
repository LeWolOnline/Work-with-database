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
  public partial class Buildings : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      readDistrects();
      readData();
      ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:initElements(); ", true);
    }

    private void readData(string select = "")
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(connectToDB.SQLconnection)) 
      {
        string quType = select != "" ? (" AND District = '" + select + "'") : ("");
        string query = "SELECT buildings.Kadastr, District, Address " +
          "FROM buildings, infoAboutBuilding, districts " +
          "WHERE buildings.Kadastr = infoAboutBuilding.Kadastr AND buildings.Kadastr = districts.Kadastr" + quType + ";";
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
              , connectToDB.SafeGetString(reader, 2)));
            i++;
          }
        }
      }
      elementsRepeater.DataSource = values;
      elementsRepeater.DataBind();
    }
    private void readDistrects()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        string query = "SELECT DISTINCT District FROM districts;";
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
      typesRepeater.DataSource = values;
      typesRepeater.DataBind();
    }
    public void getElementInfo(Object sender, EventArgs e)
    {
      getElementInfo();
    }
    private void getElementInfo()
    {
      int id = int.Parse(hiElementId.Value);

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "SELECT buildings.Kadastr, District, Address, Flow, Flats, " +
          "Elevator, Land, Line, Year, Material, Base, Wear, Square, Comment " +
          "FROM buildings, infoAboutBuilding, districts " +
          "WHERE buildings.Kadastr = infoAboutBuilding.Kadastr AND buildings.Kadastr = districts.Kadastr " +
          "AND buildings.Kadastr = " + id.ToString() + ";";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            bool elevator;
            try
            { elevator = reader.GetBoolean(5); }
            catch
            { elevator = false; }
            Kadastr.Value = connectToDB.SafeGetString(reader, 0);
            District.Value = connectToDB.SafeGetString(reader, 1);
            Address.Value = connectToDB.SafeGetString(reader, 2);
            Flow.Value = connectToDB.SafeGetString(reader, 3);
            Flats.Value = connectToDB.SafeGetString(reader, 4);
            Elevator.Checked = elevator;
            Land.Value = connectToDB.SafeGetString(reader, 6);
            Line.Value = connectToDB.SafeGetString(reader, 7);
            Year.Value = connectToDB.SafeGetString(reader, 8);
            Material.Value = connectToDB.SafeGetString(reader, 9);
            Base.Value = connectToDB.SafeGetString(reader, 10);
            Wear.Value = connectToDB.SafeGetString(reader, 11);
            Square.Value = connectToDB.SafeGetString(reader, 12);
            Comment.Value = connectToDB.SafeGetString(reader, 13);
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

        string[] flatNumbers = new string[] { };
        string query = "SELECT Kadastr FROM buildings;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            flatNumbers = flatNumbers.Append(connectToDB.SafeGetString(reader, 0)).ToArray();
        if (flatNumbers.Contains(Kadastr.Value))
        {
          validPolicyNumber.Visible = false;
        }
        else
        {
          validPolicyNumber.Visible = true;
          return;
        }

        query = "UPDATE buildings, infoAboutBuilding, districts" +
          " SET buildings.Kadastr = '" + Kadastr.Value +
          "', District = '" + District.Value +
          "', Address = '" + Address.Value +
          "', Flow = '" + Flow.Value +
          "', Flats = '" + Flats.Value +
          "', Elevator = '" + (Elevator.Checked ? "1" : "0") +
          "', Land = '" + Land.Value +
          "', Line = '" + Line.Value +
          "', Year = '" + Year.Value +
          "', Material = '" + Material.Value +
          "', Base = '" + Base.Value +
          "', Wear = '" + Wear.Value +
          "', Square = '" + Square.Value +
          "', Comment = '" + Comment.Value +
          "' WHERE buildings.Kadastr = infoAboutBuilding.Kadastr AND buildings.Kadastr = districts.Kadastr" +
          " AND buildings.Kadastr = " + connectToDB.ParseIntReturnString(hiElementId.Value) + ";";
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


        string[] flatNumbers = new string[] { };
        string query = "SELECT Kadastr FROM buildings;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            flatNumbers = flatNumbers.Append(connectToDB.SafeGetString(reader, 0)).ToArray();
        if (flatNumbers.Contains(newKadastr.Value))
        {
          validPolicyNumber.Visible = false;
        }
        else
        {
          validPolicyNumber.Visible = true;
          return;
        }

        string Kadastr = newKadastr.Value;
        string Address = newAdress.Value;
        query = "INSERT INTO buildings (Kadastr, Address) VALUES (" + Kadastr + ", '" + Address + "');";
        query += "\nINSERT INTO infoaboutbuilding (Kadastr) VALUES (" + Kadastr + ");";
        query += "\nINSERT INTO districts (Kadastr) VALUES (" + Kadastr + ");";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        hiElementId.Value = Kadastr.ToString();

        readData();
        getElementInfo();
      }
    }
    public void deleteValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "SET SQL_SAFE_UPDATES = 0;";
        query += "DELETE FROM buildings WHERE (Kadastr = '" + hiElementId.Value + "');";
        query += "\nDELETE FROM infoAboutBuilding WHERE (Kadastr = '" + hiElementId.Value + "');";
        query += "\nDELETE FROM districts WHERE (Kadastr = '" + hiElementId.Value + "');";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        connection.Close();

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
      }
    }
    private class ElementData
    {
      public ElementData(string kadastr = null, string district = null, string address = null)
      {
        Kadastr = kadastr;
        District = district;
        Address = address;
      }
      public ElementData(string district = null)
      {
        District = district;
      }

      public string Kadastr { get; }
      public string Address { get; }
      public string District { get; }
    }
  }
}