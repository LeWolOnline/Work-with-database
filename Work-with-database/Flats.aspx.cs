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
  public partial class Flats : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack && !IsCallback)
      {
        readKadastrs();
        readData();
      }
      ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:initElements(); ", true);
    }
    private void readData(string select = "")
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        string quType = select != "" ? (" AND flats.Kadastr = '" + select + "'") : ("");
        string query = "SELECT flats.Kadastr, Storey, flat.Flat, Address " +
          "FROM flat, flats, buildings " +
          "WHERE flat.Flat = flats.Flat AND flats.Kadastr = buildings.Kadastr" + quType + ";";
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
    private void readKadastrs()
    {
      ArrayList values = new ArrayList();

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        string query = "SELECT DISTINCT Kadastr, Address FROM buildings;";
        connection.Open();
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          int i = 1;
          while (reader.Read())
          {
            values.Add(new ElementData(connectToDB.SafeGetString(reader, 0),
                                        connectToDB.SafeGetString(reader, 1)));
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
        string query = "SELECT flat.Flat, Storey, flats.Kadastr, Address, Rooms, Height, SquareFlat, Dwell, Branch, Balcony " +
          "FROM flat, flats, buildings " +
          "WHERE flat.Flat = flats.Flat AND flats.Kadastr = buildings.Kadastr " +
          "AND flat.Flat = '" + id.ToString() + "';";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            Flat.Value = connectToDB.SafeGetString(reader, 0);
            Storey.Value = connectToDB.SafeGetString(reader, 1);
            Kadastr.Value = connectToDB.SafeGetString(reader, 2);
            Address.InnerText = connectToDB.SafeGetString(reader, 3);
            Rooms.Value = connectToDB.SafeGetString(reader, 4);
            Height.Value = connectToDB.SafeGetString(reader, 5);
            SquareFlat.Value = connectToDB.SafeGetString(reader, 6);
            Dwell.Value = connectToDB.SafeGetString(reader, 7);
            Branch.Value = connectToDB.SafeGetString(reader, 8);
            Balcony.Value = connectToDB.SafeGetString(reader, 9);
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
        string query = "SELECT Flat FROM flat;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            flatNumbers = flatNumbers.Append(connectToDB.SafeGetString(reader, 0)).ToArray();
        if (!flatNumbers.Contains(Flat.Value))
        {
          validFlat.Visible = false;
        }
        else
        {
          validFlat.Visible = true;
          return;
        }

        query = "UPDATE flat, flats" +
          " SET flat.Flat = '" + Flat.Value +
          "', Storey = '" + Storey.Value +
          "', flats.Kadastr = '" + Kadastr.Value +
          "', Rooms = '" + Rooms.Value +
          "', Height = '" + connectToDB.FloatToString(Height.Value) +
          "', SquareFlat = '" + connectToDB.FloatToString(SquareFlat.Value) +
          "', Dwell = '" + connectToDB.FloatToString(Dwell.Value) +
          "', Branch = '" + connectToDB.FloatToString(Branch.Value) +
          "', Balcony = '" + connectToDB.FloatToString(Balcony.Value) +
          "' WHERE flat.Flat = flats.Flat" +
          " AND flat.Flat = " + connectToDB.ParseIntReturnString(hiElementId.Value) + ";";
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
        string query = "SELECT Flat FROM flat;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            flatNumbers = flatNumbers.Append(connectToDB.SafeGetString(reader, 0)).ToArray();
        if (!flatNumbers.Contains(newFlatNumber.Value))
        {
          validNewFlat.Visible = false;
        }
        else
        {
          validNewFlat.Visible = true;
          return;
        }

        string flatNumber = newFlatNumber.Value;
        string kadastr = newFlatKadastr.Value;
        query = "INSERT INTO flats (Kadastr, Flat) VALUES (" + kadastr + ", '" + flatNumber + "');";
        query += "\nINSERT INTO flat (Flat) VALUES (" + flatNumber + ");";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        hiElementId.Value = flatNumber.ToString();
        readData();
        getElementInfo();
      }
    }

    public void deleteValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "DELETE FROM flats WHERE (Kadastr = '" + hiElementId.Value + "');";
        query += "\nDELETE FROM flat WHERE (Kadastr = '" + hiElementId.Value + "');";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        connection.Close();

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
      }
    }
    private class ElementData
    {
      public ElementData(string kadastr = null, string storey = null, string flat = null, string address = null)
      {
        Kadastr = kadastr;
        Storey = storey;
        Flat = flat;
        Address = address;
      }
      public ElementData(string kadastr = null, string address = null)
      {
        Kadastr = kadastr;
        Address = address;
      }

      public string Storey { get; }
      public string Flat { get; }
      public string Kadastr { get; }
      public string Address { get; }
    }
  }
}