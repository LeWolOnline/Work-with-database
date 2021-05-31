<%@ Page Title="Врачи" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Doctors.aspx.cs" Inherits="Work_with_database.Doctors" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2><%: Title %></h2>
  <div class="page">
    <div class="leftPanel">
      <div class="leftPanelFilter">
        <select class="form-select" aria-label="Выбрать специальзацию врача">
          <option selected>Выбрать специальзацию врача</option>
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

    <%--<div class="Workspace_content">
      <div class="Workspace_contentSetting">
        <h2 class="Workspace_contentHeader">Станок #<span id="machineId" runat="server"></span></h2>
        <div class="row">
          <div class="col Workspace_contentSpkText">Номер СПК</div>
        </div>
        <div class="row Workspace_spk">
          <div class="col">
            <input type="number" class="Workspace_contentSpkInput" id="machineSPK" runat="server" />
            <asp:Button type="button" class="btn btn-primary btn-sm Workspace_contentSpkButton" runat="server" OnClick="saveSpkNumber" Text="Сохранить" />
          </div>
        </div>
        <div class="row">
          <div class="col">
            <input class="Workspace_progressBar" type="range" min="0" max="100" step="1" value="0" runat="server" id="machineValue" />
            <asp:Button type="button" class="btn btn-primary btn-sm Workspace_contentSpkButton" runat="server" OnClick="saveValue" Text="Сохранить" />
          </div>
        </div>
      </div>
      <div class="Workspace_filter">
        <div class="Workspace_filterHeader">Выбрать период:</div>
        <div class="row">
          <div class="col">
            <div class="Workspace_filterText">От: </div>
            <div class="form-group">
              <input type="date" class="form-control">
            </div>
            <div class="Workspace_filterChange">
              <select class="form-select" aria-label="Default select example">
                <option value="1" selected>Первая смена</option>
                <option value="2">Вторая смена</option>
              </select>
            </div>
          </div>
          <div class="col">
            <div class="Workspace_filterText">До: </div>
            <div class="form-group">
              <input type="date" class="form-control">
            </div>
            <div class="Workspace_filterChange">
              <select class="form-select" aria-label="Default select example">
                <option value="1" selected>Первая смена</option>
                <option value="2">Вторая смена</option>
              </select>
            </div>
          </div>
        </div>
        <div>
          <button type="button" class="btn btn-primary">Вывести данные</button>
        </div>
      </div>
      <div class="Workspace_contentData">
        <div class="Workspace_contentDataBlock">
          <div class="Workspace_contentDataHeader">
            Время работы станка 
            <div class="Workspace_contentDataSubHeader">За период без простоя</div>
          </div>
          <div class="">8 дней 16 часов</div>
        </div>
        <div class="Workspace_contentDataBlock">
          <div class="Workspace_contentDataHeader">Наработка инструмента</div>
          <div class="">52</div>
        </div>
        <div class="Workspace_contentDataBlock">
          <div class="Workspace_contentDataHeader">Кол-во деталей</div>
          <div class="">18</div>
        </div>
        <div class="Workspace_contentDataBlock">
          <div class="Workspace_contentDataHeader">Время возмущения</div>
          <div class="">Скачать файл</div>
        </div>
        <div class="Workspace_contentDataBlock">
          <div class="Workspace_contentDataHeader">Коэффициент загрузки</div>
          <div class="">9 дней 2 часа</div>
        </div>
      </div>
    </div>--%>
  </div>
</asp:Content>
