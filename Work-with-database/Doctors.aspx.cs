﻿using System;
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

      using (var connection = new MySqlConnection(connectToDB.SQLconnection)) 
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
            values.Add(new ElementData(
              connectToDB.SafeGetString(reader, 0)
              , connectToDB.SafeGetString(reader, 1)
              , connectToDB.SafeGetString(reader, 2)
              , connectToDB.SafeGetString(reader, 3)
              , connectToDB.SafeGetString(reader, 4)
              , connectToDB.SafeGetString(reader, 5)));
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

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        string query = "SELECT DISTINCT types.Type FROM hospital.types;";
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
        string query = "SELECT doctors.LastName, doctors.FirstName, doctors.Patronymic, doctors.Born, doctors.Phone, " +
          "doctors.University, doctors.Experience, types.Type, rooms.Room " +
          "FROM hospital.doctors, hospital.types, hospital.rooms " +
          "WHERE doctors.DoctorID = types.DoctorID AND doctors.DoctorID = rooms.DoctorID " +
          "AND doctors.DoctorID = " + id.ToString() + ";";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            docLastName.Value = connectToDB.SafeGetString(reader, 0);
            docFirstName.Value = connectToDB.SafeGetString(reader, 1);
            docPatronymic.Value = connectToDB.SafeGetString(reader, 2);
            docYear.Value = connectToDB.SafeGetString(reader, 3);
            docPhone.Value = connectToDB.SafeGetString(reader, 4);
            docUniversity.Value = connectToDB.SafeGetString(reader, 5);
            docExperience.Value = connectToDB.SafeGetString(reader, 6);
            docType.Value = connectToDB.SafeGetString(reader, 7);
            docRoom.Value = connectToDB.SafeGetString(reader, "Room");
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
        string query = "UPDATE hospital.doctors, hospital.types, hospital.rooms" +
          " SET doctors.LastName = '" + docLastName.Value +
          "', doctors.FirstName = '" + docFirstName.Value +
          "', doctors.Patronymic = '" + docPatronymic.Value +
          "', doctors.Born = " + connectToDB.ParseIntReturnString(docYear.Value) +
          ", doctors.Phone = '" + docPhone.Value +
          "', doctors.University = '" + docUniversity.Value +
          "', doctors.Experience = " + connectToDB.ParseIntReturnString(docExperience.Value) +
          ", types.Type = '" + docType.Value +
          "', rooms.Room = " + connectToDB.ParseIntReturnString(docRoom.Value) +
          " WHERE doctors.DoctorID = types.DoctorID AND doctors.DoctorID = rooms.DoctorID" +
          " AND doctors.DoctorID = " + connectToDB.ParseIntReturnString(hiElementId.Value) + ";";
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
        int index = 0;
        string query = "SELECT DoctorID FROM hospital.doctors;";
        using (var command = new MySqlCommand(query, connection))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
            index = reader.GetInt32(0) > index ? reader.GetInt32(0) : index;
        index += 1;

        query = "INSERT INTO hospital.doctors (DoctorID) VALUES (" + index + ");";
        query += "\nINSERT INTO hospital.rooms (DoctorID) VALUES (" + index + ");";
        query += "\nINSERT INTO hospital.types (DoctorID) VALUES (" + index + ");";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        hiElementId.Value = index.ToString();
        readData();
        getElementInfo();
      }
    }
    public void deleteValue(Object sender, EventArgs e)
    {
      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "DELETE FROM hospital.doctors WHERE (DoctorID = '" + hiElementId.Value + "');";
        query += "\nDELETE FROM hospital.rooms WHERE (DoctorID = '" + hiElementId.Value + "');";
        query += "\nDELETE FROM hospital.types WHERE (DoctorID = '" + hiElementId.Value + "');";
        using (var command = new MySqlCommand(query, connection))
          command.ExecuteReader();
        connection.Close();

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
      }
    }
    private class ElementData
    {
      public ElementData(string lastName = null, string firstName = null, string patronymic = null
        , string cabinet = null, string type = null, string id = null)
      {
        Fio = lastName + " " + firstName + " " + patronymic;
        try
        {
          Cabinet = int.Parse(cabinet);
        }
        catch
        {
          Cabinet = null;
        }
        DocType = type;
        Id = int.Parse(id);
      }
      public ElementData(string type = null)
      {
        DocType = type;
      }

      public string Fio { get; }
      public int Id { get; }
      public int? Cabinet { get; }
      public string DocType { get; }
    }
  }
}