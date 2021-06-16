<%@ Page Title="Врачи" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Doctors.aspx.cs" Inherits="Work_with_database.Doctors" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="leftPanel container">

      <h2><%: Title %></h2>
      <div class="leftPanelFilter">
        <input type="hidden" id="hiSelect" runat="server" clientidmode="static" />
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBackSelect" OnClick="selectUpdate" />
        </div>
        <select id="selector" class="form-select" aria-label="Выбрать специализацию врача">
          <option selected>Выбрать специализацию врача</option>
          <option value="">Все</option>
          <asp:Repeater ID="typesRepeater" runat="server">
            <ItemTemplate>
              <option value="<%# DataBinder.Eval(Container.DataItem, "DocType")%>"><%# DataBinder.Eval(Container.DataItem, "DocType")%></option>
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
                <div class="Doctors_DocFio"><%# DataBinder.Eval(Container.DataItem, "Fio")%></div>
                <div class="Doctors_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "Cabinet")%> каб.</div>
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Doctors_DocType"><%# DataBinder.Eval(Container.DataItem, "DocType")%></div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>

      </div>
      <ASP:Button runat="server" class="leftPanelButton" Text="Добавить нового врача" OnClick="addNewElement"></ASP:Button>
    </div>

    <div class="rightPanel container">
      <h2 class="mb-5">Информация о враче</h2>
      <input type="hidden" id="hiElementId" runat="server" clientidmode="static" />
      <div class="row mb-4">
        <div class="col-4">
          <img src="~/Photo/NoPhoto.png" runat="server" class="float-end img-thumbnail" alt="no photo">
        </div>
        <div class="col">
          <div class="row mb-3">
            <div class="col">
              <input type="text" runat="server" class="form-control" placeholder="Имя" aria-label="Имя" id="docLastName">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" runat="server" class="form-control" placeholder="Фамилия" aria-label="Фамилия" id="docFirstName">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" runat="server" class="form-control" placeholder="Отчество" aria-label="Отчество" id="docPatronymic">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" runat="server" class="form-control" placeholder="Год рождения" aria-label="Год рождения" id="docYear">
            </div>
            <div class="col-8">
              <input type="text" runat="server" class="form-control" placeholder="Телефон" aria-label="Телефон" id="docPhone">
            </div>
          </div>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <h4>Образование</h4>
        </div>
      </div>

      <div class="row mb-4">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Университет" aria-label="Университет" id="docUniversity">
            <label for="docUniversity">Университет: </label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Опыт (полных лет)" aria-label="Опыт (полных лет)" id="docExperience">
            <label for="docExperience">Опыт (полных лет): </label>
          </div>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <h4>Рабочие данные</h4>
        </div>
      </div>

      <div class="row mb-4">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Специализация" aria-label="Специализация" id="docType">
            <label for="docType">Специализация: </label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Кабинет" aria-label="Кабинет" id="docRoom">
            <label for="docRoom">Кабинет: </label>
          </div>
        </div>
      </div>
      <ASP:Button runat="server" class="btn btn-primary" Text="Сохранить изменения" OnClick="saveValue"></ASP:Button>
    </div>
  </div>
</asp:Content>
