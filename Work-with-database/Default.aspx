<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Work_with_database._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2 class="mb-5">Запись к врачу</h2>
  <div class="row">
    <div class="col-4">
      <input type="hidden" id="hiSelect" runat="server" clientidmode="static" />
      <div class="row g-3">
        <div class="col-md-12">
          <label for="inputPolicyNumber" class="form-label">Введите номер страхового полиса</label>
          <input runat="server" type="text" class="form-control" id="inputPolicyNumber" placeholder="1111111111111111(16)" required>
          <label runat="server" id="validPolicyNumber" style="color:red" visible="false">
            Пользователя с таким полисом нет в системе
          </label>
        </div>
        <%--<div class="col-12">
          <label for="inputDoctorType" class="form-label">Выберите специализацию врача</label>
          <select id="inputDoctorType" class="form-select">
            <option selected>Choose...</option>
            <option>...</option>
          </select>
        </div>--%>
        <div class="col-12">
          <label for="selector" class="form-label">Выберите врача</label>
          <select id="selector" class="form-select" required>
            <option selected>Выберите врача</option>
            <asp:Repeater ID="typesRepeater" runat="server">
              <ItemTemplate>
                <option value="<%# DataBinder.Eval(Container.DataItem, "Id")%>"><%# DataBinder.Eval(Container.DataItem, "Fio")%></option>
              </ItemTemplate>
            </asp:Repeater>
          </select>
        </div>
        <div class="col-12">
          <div class="form-group">
            <label for="inputDate" class="form-label">Введите дату:</label>
            <input runat="server" id="inputDate" type="date" class="form-control" required>
          </div>
        </div>
        <div class="col-12 mb-5">
          <label for="inputTime" class="form-label">Выберите доступное время</label>
          <select runat="server" id="inputTime" class="form-select" required>
            <option selected>Выберите...</option>
            <option value="8:00">8:00</option>
            <option value="9:00">9:00</option>
            <option value="10:00">10:00</option>
            <option value="11:00">11:00</option>
            <option value="12:00">12:00</option>
            <option value="13:00">13:00</option>
            <option value="14:00">14:00</option>
            <option value="15:00">15:00</option>
            <option value="16:00">16:00</option>
          </select>
        </div>
        <div class="col-12">
          <asp:Button runat="server" class="btn btn-primary" Text="Записаться" OnClick="saveValue"></asp:Button>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
