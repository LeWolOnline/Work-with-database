<%@ Page Title="Квартиры" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Flats.aspx.cs" Inherits="Work_with_database.Flats" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
  
    <div class="rightPanel container">
      <h2 class="mb-5">Информация о квартире</h2>
      <input type="hidden" id="hiElementId" runat="server" clientidmode="static" />
      <div class="row mb-3">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Кадастровый номер дома" aria-label="Кадастровый номер дома" id="Kadastr">
            <label for="Kadastr">Кадастровый номер дома</label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Этаж" aria-label="Этаж" id="Storey">
            <label for="Storey">Этаж</label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Номер квартиры" aria-label="Номер квартиры" id="Flat">
            <label for="Flat">Номер квартиры</label>
          </div>
          <label runat="server" id="validFlat" style="color:red" visible="false">
            Квартира с таким номером уже существует
          </label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <label id="Address" runat="server">Адрес</label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Количество комнат" aria-label="Количество комнат" id="Rooms">
            <label for="Rooms">Количество комнат</label>
          </div>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Высота потолков" aria-label="Высота потолков" id="Height">
            <label for="Height">Высота потолков</label>
          </div>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Общая площадь квартиры" aria-label="Общая площадь квартиры" id="SquareFlat">
            <label for="SquareFlat">Общая площадь квартиры (м²)</label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Площадь балкона" aria-label="Площадь балкона" id="Balcony">
            <label for="Balcony">Площадь балкона (м²)</label>
          </div>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Жилая площадь квартиры" aria-label="Жилая площадь квартиры" id="Dwell">
            <label for="Dwell">Жилая площадь квартиры (м²)</label>
          </div>
        </div>
        <div class="col">
          <div class="form-floating">
            <input type="text" runat="server" class="form-control" placeholder="Вспомогательная площадь квартиры" aria-label="Вспомогательная площадь квартиры" id="Branch">
            <label for="Branch">Вспомогательная площадь квартиры (м²)</label>
          </div>
        </div>
      </div>
      <div class="row mb-4">
        <div class="col-3">
          <asp:Button runat="server" class="btn btn-primary" Text="Сохранить изменения" OnClick="saveValue"></asp:Button>
        </div>
        <div class="col-3">
          <asp:Button runat="server" ID="btnDelete" class="btn btn-primary" Text="Удалить квартиру" OnClick="deleteValue"></asp:Button>
        </div>
      </div>
    </div>

      <div class="leftPanel container">

      <h2><%: Title %></h2>
      <div class="leftPanelFilter">
        <input type="hidden" id="hiSelect" runat="server" clientidmode="static" />
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBackSelect" OnClick="selectUpdate" />
        </div>
        <select id="selector" class="form-select" aria-label="Выбрать здание">
          <option selected>Выбрать здание</option>
          <option value="">Все</option>
          <asp:Repeater ID="districtsRepeater" runat="server">
            <ItemTemplate>
              <option value="<%# DataBinder.Eval(Container.DataItem, "Kadastr")%>"><%# DataBinder.Eval(Container.DataItem, "Address")%></option>
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
            <div class="leftPanelElementBlock" id="<%# DataBinder.Eval(Container.DataItem, "Flat")%>">
              <div class="leftPanelElementDataRow">
                <div class="Buildings_DocFio">Номер квартиры: <%# DataBinder.Eval(Container.DataItem, "Flat")%></div>
                <div class="Buildings_DocCabinet"><%# DataBinder.Eval(Container.DataItem, "Storey")%> этаж</div>
              </div>
              <div class="leftPanelElementDataRow">
                <div class="Buildings_DocType">Адрес: <%# DataBinder.Eval(Container.DataItem, "Address")%></div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>
      </div>
      <button type="button" class="leftPanelButton btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Добавить новую квартиру в базу</button>
    </div>

    <div class="modalSide">
      <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Добавление новой квартиры</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="mb-3">
                <label for="newPatNumber" class="col-form-label">Номер квартиры:</label>
                <input runat="server" type="text" class="form-control" id="newFlatNumber">
              </div>
              <div class="mb-3">
                <label runat="server" id="validNewFlat" style="color:red" visible="false">
                  Квартира с таким номером уже существует
                </label>
              </div>
              <div class="mb-3">
                <label for="newPatFio" class="col-form-label">Кадастровый номер здания:</label>
                <input runat="server" type="text" class="form-control" id="newFlatKadastr">
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
