<%@ Page Title="Расписание" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Documents.aspx.cs" Inherits="Work_with_database.Schedule" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="leftPanel container">
      <h2><%: Title %></h2>
      <%--<div class="leftPanelFilter">
        <div class="form-group">
          <input id="inputDate" type="date" class="form-control">
        </div>
      </div>--%>
      <div class="leftPanelElements">
        
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBack" OnClick="getElementInfo" />
        </div>
        
        <asp:Repeater ID="elementsRepeater" runat="server">
          <ItemTemplate>
            <div class="leftPanelElementBlock" id="<%# DataBinder.Eval(Container.DataItem, "Id")%>">
              <div class="leftPanelElementDataRow">
                <div class="Sсhedule_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "Data")%></div>
                <div class="Sсhedule_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "Time")%></div>
              </div>
              <div class="leftPanelElementDataRowLine">
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Sсhedule_DocFio">Врач: </div>
                <div class="Sсhedule_DocCabinet">Кабинет: <%# DataBinder.Eval(Container.DataItem, "DocCabinet")%></div>
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Sсhedule_DocFio"><%# DataBinder.Eval(Container.DataItem, "DocFio")%></div>
              </div>
              <div class="leftPanelElementDataRowLine">
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Sсhedule_PatientFio">Пациент:</div>
                <div class="Sсhedule_PatientAge">Возраст: <%# DataBinder.Eval(Container.DataItem, "PatientAge")%></div>
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Sсhedule_PatientFio"><%# DataBinder.Eval(Container.DataItem, "PatientFio")%></div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>  
      </div>
    </div>

    <div class="rightPanel container">
      <h2 class="mb-5">Информация о записи</h2>
      <input type="hidden" id="hiElementId" runat="server" clientidmode="static" />
      <input type="hidden" id="hiPatientId" runat="server" clientidmode="static" />
      <div class="row mb-4">
        <div class="col">
          <label runat="server" class="form-label" id="appDateTime">Дата и время записи</label>
        </div>
      </div>

      <div class="row mb-3">
        <h4>Информация о пациенте</h4>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label runat="server" class="form-label" id="patFio">ФИО пациента</label>
        </div>
        <div class="col-4">
          <label runat="server" class="form-label" id="patYear">Возраст пациента</label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label runat="server" class="form-label" id="patId">Номер страхового полиса</label>
        </div>
        <div class="col-4">
          <label runat="server" class="form-label" id="patCart">Карточка пациента</label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label runat="server" class="form-label" id="patAddress">Адрес пациента</label>
        </div>
        <div class="col-4">
          <label runat="server" class="form-label" id="patDistrict">Район города</label>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <ASP:Button runat="server" type="button" class="btn btn-primary" id="btPrintPatientFile" Text="Распечатать карточку пациента" OnClick="printPatientFile"></ASP:Button>
          <input type="hidden" id="hiPrintPatientFile" runat="server" clientidmode="static" />
        </div>
      </div>

      <div class="row mb-3">
        <h4>Информация о враче</h4>
      </div>
      <div class="row mb-4">
        <div class="col">
          <label runat="server" class="form-label" id="docFio">Имя врача, к кому запись</label>
        </div>
        <div class="col-4">
          <label runat="server" class="form-label" id="docRoom">Номер кабинета</label>
        </div>
      </div>

      <div class="row mb-3">
        <h4>Информация об оплате</h4>
      </div>
      <div class="row mb-3" id="blockIsWorker" runat="server">
        <div class="col">
          <label runat="server" class="form-label">Работник предприятия</label>
        </div>
      </div>
      <div class="row mb-3" id="blockPay" runat="server">
        <div class="col">
          <label class="form-label" for="exeSumm">Стоимость лечения:</label>
          <input type="text" runat="server" class="form-control" placeholder="Сумма лечения" aria-label="Сумма лечения" id="exeSumm">
        </div>
      </div>
      <div class="row mb-3" id="blockPayBtn" runat="server">
        <div class="col">
          <button runat="server" type="button" class="btn btn-primary" id="btPrintPaymentDocument">Распечатать платежный документ</button>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <label for="appComment">Комментарий: </label>
          <textarea runat="server" class="form-control" id="appComment" rows="3"></textarea>
        </div>
      </div>
      <ASP:Button runat="server" class="btn btn-primary" Text="Сохранить изменения" OnClick="saveValue"></ASP:Button>
    </div>
  </div>
</asp:Content>
