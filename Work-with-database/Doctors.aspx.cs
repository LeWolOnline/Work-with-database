using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MySqlConnector;
using System.IO;
using System.Data;

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
      getPicture();
    }

    private void getPicture()
    {
      bool isGetPicture = false;
      int id = int.Parse(hiElementId.Value);
      string sFileName = HttpContext.Current.Server.MapPath(@"~/Files/img.jpg");

      using (var connection = new MySqlConnection(connectToDB.SQLconnection))
      {
        connection.Open();
        string query = "SELECT Picture FROM doctors WHERE doctors.DoctorID = " + id.ToString() + ";";
        var command = new MySqlCommand(query, connection);
        // Writes the BLOB to a file.  
        FileStream stream;
        // Streams the BLOB to the FileStream object.  
        BinaryWriter writer;

        // Size of the BLOB buffer.  
        int bufferSize = 100;
        // The BLOB byte[] buffer to be filled by GetBytes.  
        byte[] outByte = new byte[bufferSize];
        // The bytes returned from GetBytes.  
        long retval;
        // The starting position in the BLOB output.  
        long startIndex = 0;

        // Open the connection and read data into the DataReader. 
        MySqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);

        while (reader.Read())
        {
          // Create a file to hold the output.  
          stream = new FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Write);
          writer = new BinaryWriter(stream);

          // Reset the starting byte for the new BLOB.  
          startIndex = 0;

          // Read bytes into outByte[] and retain the number of bytes returned.  
          retval = reader.GetBytes(0, startIndex, outByte, 0, bufferSize);

          // Continue while there are bytes beyond the size of the buffer.  
          while (retval == bufferSize)
          {
            isGetPicture = true;
            writer.Write(outByte);
            writer.Flush();

            // Reposition start index to end of last buffer and fill buffer.  
            startIndex += bufferSize;
            retval = reader.GetBytes(0, startIndex, outByte, 0, bufferSize);
          }

          // Write the remaining buffer.  
          writer.Write(outByte, 0, (int)retval);
          writer.Flush();

          // Close the output file.  
          writer.Close();
          stream.Close();

          if (isGetPicture)
          {
            image.Src = "/Files/img.jpg";
          }
          else
          {
            image.Src = "/Photo/NoPhoto.png";
          }
        }
      }
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

    public void uploadPhoto(Object sender, EventArgs e)
    {
      HttpPostedFile file = inputImgUploader.PostedFile;
      byte[] fileData = null;
      using (var binaryReader = new BinaryReader(file.InputStream))
      {
        fileData = binaryReader.ReadBytes(file.ContentLength);
      }

      string queryStmt = "UPDATE doctors" +
            " SET Picture = @Content" +
            " WHERE doctors.DoctorID = " + hiElementId.Value + ";";

      using (MySqlConnection connection = new MySqlConnection(connectToDB.SQLconnection))
      using (MySqlCommand _cmd = new MySqlCommand(queryStmt, connection))
      {
        MySqlParameter param = _cmd.Parameters.Add("@Content", MySqlDbType.VarBinary);
        param.Value = fileData;

        connection.Open();
        _cmd.ExecuteNonQuery();
        connection.Close();
      }

      getPicture();
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