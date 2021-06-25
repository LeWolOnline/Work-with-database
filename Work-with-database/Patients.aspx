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
      <button type="button" class="leftPanelButton btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Добавить карточку нового пациента</button>
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
      <div class="row mb-4">
        <div class="col-3">
          <asp:Button runat="server" class="btn btn-primary" Text="Сохранить изменения" OnClick="saveValue"></asp:Button>
        </div>
        <div class="col-3">
          <asp:Button runat="server" ID="btnDelete" class="btn btn-primary" Text="Удалить пациента" OnClick="deleteValue"></asp:Button>
        </div>
      </div>
    </div>
    <div class="modalSide">

      <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Добавление нового пациента</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="mb-3">
                <label for="newPatNumber" class="col-form-label">Номер страхового полиса:</label>
                <input runat="server" type="text" class="form-control" id="newPatNumber">
              </div>
              <div class="mb-3">
                <label runat="server" id="validNewPolicyNumber" style="color: red" visible="false">
                  Пользователя с таким полисом уже есть в системе
                </label>
              </div>
              <div class="mb-3">
                <label for="newPatFio" class="col-form-label">ФИО:</label>
                <input runat="server" type="text" class="form-control" id="newPatFio">
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Закрыть</button>
              <asp:Button type="button" class="btn btn-primary" runat="server" OnClick="addNewElement" Text="Сохранить"></asp:Button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
