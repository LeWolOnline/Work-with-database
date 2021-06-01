<%@ Page Title="Врачи" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Doctors.aspx.cs" Inherits="Work_with_database.Doctors" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="leftPanel container">
      <h2><%: Title %></h2>
      <div class="leftPanelFilter">
        <select class="form-select" aria-label="Выбрать специализацию врача">
          <option selected>Выбрать специализацию врача</option>
          <option value="1">One</option>
          <option value="2">Two</option>
          <option value="3">Three</option>
        </select>
      </div>
      <div class="leftPanelElements">

        <%--<input type="hidden" id="hiMachineId" runat="server" clientidmode="static" />
        <div style="display: none;">
          <asp:Button runat="server" ClientIDMode="static" ID="btnCallBack" OnClick="getMachineInfo" />
        </div>--%>

        <%--<asp:Repeater ID="machineRepeater" runat="server">
          <ItemTemplate>
            <div class="Workspace_machineBlock" id="machine<%# DataBinder.Eval(Container.DataItem, "MachineIdNumber") %>">
              <input data-machine-id="<%# DataBinder.Eval(Container.DataItem, "MachineId") %>"
                id="hiMachine<%# DataBinder.Eval(Container.DataItem, "MachineIdNumber") %>"
                type="hidden" />
              <div class="Workspace_machineRow">
                <div class="Workspace_machineID">Станок #<%# DataBinder.Eval(Container.DataItem, "MachineIdNumber") %></div>
                <div class="Workspace_machineInventoryNumber">ID: <%# DataBinder.Eval(Container.DataItem, "MachineInventoryNumber") %></div>
              </div>

              <div class="Workspace_machineValue">
                <div class="progress">
                  <div class="progress-bar" role="progressbar" style="width: <%# DataBinder.Eval(Container.DataItem, "MachineValue") %>%" aria-valuenow="<%# DataBinder.Eval(Container.DataItem, "MachineValue") %>" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>--%>

        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Артуров Артур Артурович</div>
            <div class="Doctors_DocCabinet">708 каб.</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Doctors_DocFio">Терапевт</div>
          </div>
        </div>
      </div>
      <a href="./Settings" class="leftPanelButton">Добавить нового врача</a>
    </div>

    <div class="rightPanel container">
      <h2 class="mb-5">Информация о враче</h2>
      <div class="row mb-5">
        <div class="col-4">
          <img src="~/Photo/NoPhoto.png" runat="server" class="float-end img-thumbnail" alt="no photo">
        </div>
        <div class="col">
          <div class="row mb-3">
            <div class="col">
              <input type="text" class="form-control" placeholder="Имя" aria-label="Имя">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" class="form-control" placeholder="Фамилия" aria-label="Фамилия">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" class="form-control" placeholder="Отчество" aria-label="Отчество">
            </div>
          </div>
          <div class="row mb-3">
            <div class="col">
              <input type="text" class="form-control" placeholder="Год рождения" aria-label="Год рождения">
            </div>
            <div class="col-8">
              <input type="text" class="form-control" placeholder="Телефон" aria-label="Телефон">
            </div>
          </div>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <h4>Образование</h4>
        </div>
      </div>

      <div class="row mb-5">
        <div class="col">
          <input type="text" class="form-control" placeholder="Университет" aria-label="Университет">
        </div>
        <div class="col">
          <input type="text" class="form-control" placeholder="Опыт (полных лет)" aria-label="Опыт (полных лет)">
        </div>
      </div>

      <div class="row mb-3">
        <div class="col">
          <h4>Рабочие данные</h4>
        </div>
      </div>

      <div class="row mb-5">
        <div class="col">
          <input type="text" class="form-control" placeholder="Специализация" aria-label="Специализация">
        </div>
        <div class="col">
          <input type="text" class="form-control" placeholder="Кабинет" aria-label="Кабинет">
        </div>
      </div>
      <button type="button" class="btn btn-primary">Сохранить изменения</button>
    </div>
  </div>
</asp:Content>
