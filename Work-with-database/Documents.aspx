<%@ Page Title="Документы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Documents.aspx.cs" Inherits="Work_with_database.Documents" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="rightPanel container">
      <h2 class="mb-5">Информация о записи</h2>
      <input type="hidden" id="hiElementId" runat="server" clientidmode="static" />
      <div class="row mb-3">
        <div class="col">
          <label runat="server" id="Record">Номер записи о праве собственности</label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label runat="server" id="Flat">Номер квартиры</label>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <label runat="server" id="DateDoc">Дата документа о собственности</label>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <label runat="server" id="Document">Документ на право собственности</label>
        </div>
      </div>

      <div class="row mb-3">
        <h4>Информация о собственнике</h4>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label runat="server" id="FioHost">ФИО собственника</label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label runat="server" id="Passport">Номер паспорта</label>
        </div>
        <div class="col">
          <label runat="server" id="Born">Год рождения собственника</label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label runat="server" id="Part">Принадлежащая ему доля, %</label>
        </div>
      </div>
    </div>
    <div class="leftPanel container">
      <h2><%: Title %></h2>
      <div class="leftPanelElements">

        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBack" OnClick="getElementInfo" />
        </div>

        <asp:Repeater ID="elementsRepeater" runat="server">
          <ItemTemplate>
            <div class="leftPanelElementBlock" id="<%# DataBinder.Eval(Container.DataItem, "Record")%>">
              <div class="leftPanelElementDataRow">
                <div class="Sсhedule_DocCabinet">#<%# DataBinder.Eval(Container.DataItem, "Record")%></div>
                <div class="Sсhedule_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "Data")%></div>
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Sсhedule_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "Flat")%> кв.</div>
                <div class="Sсhedule_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "FioHost")%></div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>

  </div>
</asp:Content>
