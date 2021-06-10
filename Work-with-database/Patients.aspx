<%@ Page Title="Пациенты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="Work_with_database.Patients" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="leftPanel container">
      <h2><%: Title %></h2>
      <div class="leftPanelFilter">
        <select class="form-select" aria-label="Выбрать район">
          <option value="" selected>Выбрать район</option>
          <asp:Repeater ID="districtsRepeater" runat="server">
            <ItemTemplate>
              <option value="<%# DataBinder.Eval(Container.DataItem, "District")%>"><%# DataBinder.Eval(Container.DataItem, "District")%></option>
            </ItemTemplate>
          </asp:Repeater>
        </select>
      </div>
      <div class="leftPanelElements">

        <%--<input type="hidden" id="hiMachineId" runat="server" clientidmode="static" />
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBack" OnClick="getMachineInfo" />
        </div>--%>
        
        <asp:Repeater ID="elementsRepeater" runat="server">
          <ItemTemplate>
            <div class="leftPanelElementBlock" id="<%# DataBinder.Eval(Container.DataItem, "Id")%>">
              <div class="leftPanelElementDataRow">
                <div class="Patients_Fio"><%# DataBinder.Eval(Container.DataItem, "Fio")%></div>
                <div class="Patients_Age"><%# DataBinder.Eval(Container.DataItem, "Age")%> лет.</div>
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Patients_District"><%# DataBinder.Eval(Container.DataItem, "District")%></div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>  
      </div>
      <a href="./Settings" class="leftPanelButton">Добавить карточку нового пациента</a>
    </div>

    <div class="rightPanel container">
      <h2 class="mb-5">Информация о пациенте</h2>
      <div class="row mb-3">
        <div class="col">
          <input type="text" class="form-control" placeholder="ФИО" aria-label="ФИО">
        </div>
        <div class="col-4">
          <input type="text" class="form-control" placeholder="Год рождения" aria-label="Год рождения">
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <input type="text" class="form-control" placeholder="Номер страхового полиса" aria-label="Номер страхового полиса">
        </div>
        <div class="col-4">
          <input type="text" class="form-control" placeholder="Телефон" aria-label="Телефон">
        </div>
      </div>
      <div class="row mb-4">
        <div class="col-4">
          <input type="text" class="form-control" placeholder="Район города" aria-label="Район города">
        </div>
        <div class="col">
          <input type="text" class="form-control" placeholder="Адрес" aria-label="Адрес">
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <div class="form-check form-switch">
            <input class="form-check-input" type="checkbox" id="flexSwitchCheckDefault">
            <label class="form-check-label" for="flexSwitchCheckDefault">Работник предприятия</label>
          </div>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <input type="text" class="form-control" placeholder="Отдел, в котором работает" aria-label="Отдел, в котором работает">
        </div>
      </div>
      <button type="button" class="btn btn-primary">Сохранить изменения</button>
    </div>
  </div>
</asp:Content>
