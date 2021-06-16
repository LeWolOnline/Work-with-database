<%@ Page Title="Пациенты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="Work_with_database.Patients" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="leftPanel container">

      <h2><%: Title %></h2>
      <div class="leftPanelFilter">
        <input type="hidden" id="hiSelect" runat="server" clientidmode="static" />
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBackSelect" OnClick="selectUpdate" />
        </div>
        <select id="selector" class="form-select" aria-label="Выбрать район">
          <option selected>Выбрать район</option>
          <option value="">Все</option>
          <asp:Repeater ID="districtsRepeater" runat="server">
            <ItemTemplate>
              <option value="<%# DataBinder.Eval(Container.DataItem, "District")%>"><%# DataBinder.Eval(Container.DataItem, "District")%></option>
            </ItemTemplate>
          </asp:Repeater>
        </select>
      </div>
      <div class="leftPanelElements">
        
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBack" OnClick="getElementInfo" />
        </div>
        
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
      <input type="hidden" id="hiElementId" runat="server" clientidmode="static" />
      <div class="row mb-3">
        <div class="col">
          <input type="text" runat="server" class="form-control" placeholder="ФИО" aria-label="ФИО" id="patFio">
        </div>
        <div class="col-4">
          <input type="text" runat="server" class="form-control" placeholder="Год рождения" aria-label="Год рождения" id="patYear">
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <input type="text" runat="server" class="form-control" placeholder="Номер страхового полиса" aria-label="Номер страхового полиса" id="patId">
        </div>
        <div class="col-4">
          <input type="text" runat="server" class="form-control" placeholder="Карточка пациента" aria-label="Телефон" id="patCart">
        </div>
      </div>
      <div class="row mb-4">
        <div class="col-4">
          <input type="text" runat="server" class="form-control" placeholder="Район города" aria-label="Район города" id="patDistrict">
        </div>
        <div class="col">
          <input type="text" runat="server" class="form-control" placeholder="Адрес" aria-label="Адрес" id="patAddress">
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <div class="form-check form-switch">
            <input runat="server" class="form-check-input" type="checkbox" id="patWorker">
            <label class="form-check-label" for="patWorker">Работник предприятия</label>
          </div>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <input type="text" runat="server" class="form-control" placeholder="Отдел, в котором работает" aria-label="Отдел, в котором работает" id="patDepartment">
        </div>
      </div>
      <ASP:Button runat="server" class="btn btn-primary" Text="Сохранить изменения" OnClick="saveValue"></ASP:Button>
    </div>
  </div>
</asp:Content>
