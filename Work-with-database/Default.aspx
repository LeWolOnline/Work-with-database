<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Work_with_database._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2 class="mb-5">Добавление договора в систему</h2>
  <div class="row">
    <div class="col-4">
      <div class="row g-3">
        <div class="col-md-12">
          <label for="inputFlatNumber" class="form-label">Номер квартиры</label>
          <input runat="server" type="text" class="form-control" id="inputFlatNumber" placeholder="123" required>
          <label runat="server" id="validPolicyNumber" style="color:red" visible="false">
            Такой квартиры нет в системе
          </label>
        </div>
        <div class="col-12">
          <label for="inputDocType" class="form-label">Документ на право собственности</label>
          <select runat="server" id="inputDocType" class="form-select" required>
            <option selected>Выберите тип договора</option>
            <option value="Договор дарения">Договор дарения</option>
            <option value="Договор купли-продажи">Договор купли-продажи</option>
            <option value="Свидетельство">Свидетельство</option>
          </select>
        </div>
        <div class="col-12 mb-5">
          <div class="form-group">
            <label for="inputDate" class="form-label">Дата документа о собственности</label>
            <input runat="server" id="inputDate" type="date" class="form-control" required>
          </div>
        </div>
        <div class="col-12">
          <label for="inputFio" class="form-label">ФИО собственника</label>
          <input runat="server" type="text" class="form-control" id="inputFio" placeholder="Иванов Иван Иванович" required>
        </div>
        <div class="col-12">
          <label for="inputPassport" class="form-label">Паспорт собственника</label>
          <input runat="server" type="text" class="form-control" id="inputPassport" placeholder="0000 000000" required>
        </div>
        <div class="col-12">
          <label for="inputYear" class="form-label">Год рождения собственника</label>
          <input runat="server" type="text" class="form-control" id="inputYear" placeholder="1900" required>
        </div>
        <div class="col-12 mb-5">
          <label for="inputPart" class="form-label">Доля собственника (%)</label>
          <input runat="server" type="text" class="form-control" id="inputPart" placeholder="50" required>
        </div>
        <div class="col-12">
          <asp:Button runat="server" class="btn btn-primary" Text="Добавить в базу" OnClick="saveValue"></asp:Button>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
