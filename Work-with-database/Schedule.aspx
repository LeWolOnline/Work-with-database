﻿<%@ Page Title="Расписание" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="Work_with_database.Schedule" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page">
    <div class="leftPanel container">
      <h2><%: Title %></h2>
      <div class="leftPanelFilter">
        <div class="form-group">
          <input id="inputDate" type="date" class="form-control">
        </div>
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
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
        <div class="leftPanelElementBlock">
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Врач: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">9:00</div>
          </div>
          <div class="leftPanelElementDataRow">
            <div class="Patients_PatientFio">Пациент: Артуров Артур Артурович</div>
            <div class="Patients_PatientAge">78 лет</div>
          </div>
        </div>
      </div>
      <a href="./Settings" class="leftPanelButton">Добавить карточку нового пациента</a>
    </div>

    <div class="rightPanel container">
      <h2 class="mb-5">Информация о записи</h2>
      <div class="row mb-3">
        <div class="col">
          <label class="form-label">ФИО пациента</label>
        </div>
        <div class="col-4">
          <label class="form-label">Возраст пациента</label>
        </div>
      </div>
      <div class="row mb-3">
        <div class="col">
          <input type="text" class="form-control" placeholder="Номер страхового полиса" aria-label="Номер страхового полиса">
        </div>
        <div class="col-4">
          <label class="form-label">Телефон пациента</label>
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
      <button type="button" class="btn btn-primary">Сохранить изменения</button>
    </div>
  </div>
</asp:Content>
