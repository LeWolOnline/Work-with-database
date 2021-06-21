<%@ Page Title="Здания" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Buildings.aspx.cs" Inherits="Work_with_database.Buildings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="leftPanel container">

      <h2><%: Title %></h2>
      <div class="leftPanelFilter">
        <input type="hidden" id="hiSelect" runat="server" clientidmode="static" />
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBackSelect" OnClick="selectUpdate" />
        </div>
        <select id="selector" class="form-select" aria-label="Район города">
          <option selected>Выбрать район</option>
          <option value="">Все</option>
          <asp:Repeater ID="typesRepeater" runat="server">
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
            <div class="leftPanelElementBlock" id="<%# DataBinder.Eval(Container.DataItem, "Kadastr")%>">
              <div class="leftPanelElementDataRow">
                <div class="Buildings_DocFio">Кадастр: <%# DataBinder.Eval(Container.DataItem, "Kadastr")%></div>
                <div class="Buildings_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "District")%> район</div>
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Buildings_DocType"><%# DataBinder.Eval(Container.DataItem, "Address")%></div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>

      </div>
      <button type="button" class="leftPanelButton btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Добавить новое здание в базу</button>
    </div>

    <div class="rightPanel container">
      <h2 class="mb-5">Информация о здании</h2>
      <input type="hidden" id="hiElementId" runat="server" clientidmode="static" />
      <div class="row mb-4">
        <div class="col-4">
          <div class="row mb-1">
            <div class="col">
              <img runat="server" id="image" src="~/Photo/NoPhoto.png" runat="server" class="float-end img-thumbnail" alt="no photo">
            </div>
          </div>
        </div>
        <div class="col">
          <div class="row mb-3">
            <div class="col">
              <input type="text" runat="server" class="form-control" placeholder="Кадастровый номер" aria-label="Кадастровый номер" id="Kadastr">
              <label runat="server" id="validPolicyNumber" style="color: red" visible="false">
                Такой номер уже есть в базе
              </label>
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" runat="server" class="form-control" placeholder="Район города" aria-label="Район города" id="District">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" runat="server" class="form-control" placeholder="Адрес" aria-label="Адрес" id="Address">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <div class="form-floating">
                <input type="text" runat="server" class="form-control" placeholder="Число этажей в здании" aria-label="Число этажей в здании" id="Flow">
                <label for="Flow">Число этажей в здании: </label>
              </div>
            </div>
            <div class="col">
              <div class="form-floating">
                <input type="text" runat="server" class="form-control" placeholder="Количество квартир в здании" aria-label="Количество квартир в здании" id="Flats">
                <label for="Flats">Количество квартир в здании: </label>
              </div>
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <div class="form-check form-switch">
                <input runat="server" class="form-check-input" type="checkbox" id="Elevator">
                <label class="form-check-label" for="Elevator">Лифт</label>
              </div>
            </div>
          </div>
        </div>
      </div>


      <div class="row mb-3">
        <div class="col">
          <input type="file" accept=".jpg" class="form-control" id="inputImgUploader" runat="server"/>
        </div>
        <div class="col">
          <asp:Button runat="server" class="btn btn-primary" ID="imgUploader" Text="Загрузить" OnClick="uploadPhoto"/>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Площадь земельного участка" aria-label="Площадь земельного участка" id="Land">
            <label for="Land">Площадь земельного участка (м²): </label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Площадь нежилых помещений" aria-label="Площадь нежилых помещений" id="Square">
            <label for="Square">Площадь нежилых помещений (м²): </label>
          </div>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Расстояние от центра города" aria-label="Расстояние от центра города" id="Line">
            <label for="Line">Расстояние от центра города (км): </label>
          </div>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Год постройки здания" aria-label="Год постройки здания" id="Year">
            <label for="Year">Год постройки здания: </label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Износ в процентах" aria-label="Износ в процентах" id="Wear">
            <label for="Wear">Износ (%): </label>
          </div>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Материал стен здания" aria-label="Материал стен здания" id="Material">
            <label for="Material">Материал стен здания: </label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Материал фундамента" aria-label="Материал фундамента" id="Base">
            <label for="Base">Материал фундамента: </label>
          </div>
        </div>
      </div>

      <div class="row mb-4">
        <div class="col">
          <label for="Comment">Комментарий: </label>
          <textarea runat="server" class="form-control" id="Comment" rows="3"></textarea>
        </div>
      </div>

      <div class="row mb-4">
        <div class="col-3">
          <asp:Button runat="server" class="btn btn-primary" Text="Сохранить изменения" OnClick="saveValue"></asp:Button>
        </div>
        <div class="col-3">
          <asp:Button runat="server" ID="btnDelete" class="btn btn-primary" Text="Удалить здание" OnClick="deleteValue"></asp:Button>
        </div>
      </div>
    </div>

    <div class="modalSide">
      <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Добавление здания</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="mb-3">
                <label for="newKadastr" class="col-form-label">Кадастровый номер здания:</label>
                <input runat="server" type="text" class="form-control" id="newKadastr">
              </div>
              <div class="mb-3">
                <label for="newAdress" class="col-form-label">Адрес здания:</label>
                <input runat="server" type="text" class="form-control" id="newAdress">
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
