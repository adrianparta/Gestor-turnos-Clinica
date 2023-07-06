﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosRecepcionista.aspx.cs" Inherits="Clinic.TurnosRecepcionista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Nuevo turno</h1>
    <asp:DropDownList CssClass="form-select" ID="DropDownListEspecialidad" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListEspecialidad_SelectedIndexChanged"></asp:DropDownList>

    <h3>Buscador de paciente</h3>
    <asp:DropDownList CssClass="form-select" ID="DropDownListPacientes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPacientes_SelectedIndexChanged"></asp:DropDownList>

    <h3>agregar paciente nuevo</h3>
    <h3>turno sugerido</h3>
    <h3>turno sugerido</h3>
    <h3>turno sugerido</h3>

    <asp:DropDownList CssClass="form-select" ID="DropDownListMedicos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListMedicos_SelectedIndexChanged"></asp:DropDownList>
    <%
        if (DropDownListMedicos.SelectedIndex > 0)
        {
            foreach (Dominio.HorarioLaboral i in ((List<Dominio.HorarioLaboral>)Session["horariolaboral"]))
            {
    %>

    <h5><%: i.ToString() %></h5>

    <%}
        }%>
    <h3>Calendario filtrado por horarios del medico</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Calendar ID="Calendario" runat="server" OnDayRender="Calendario_DayRender" OnSelectionChanged="Calendario_SelectionChanged" AutoPostBack="false" FirstDayOfWeek="Sunday"></asp:Calendar>
    <h3>Horarios filtrado por disponibilidad del medico</h3>
    <asp:DropDownList CssClass="form-select" ID="DropDownListHorarios" runat="server" AutoPostBack="true" OnDataBound="DropDownListHorarios_DataBound" OnSelectedIndexChanged="DropDownListHorarios_SelectedIndexChanged"></asp:DropDownList>
    <asp:Label id="hola" Text="text" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button Text="Aceptar" runat="server" />
    <asp:Button Text="Cancelar" runat="server" />

</asp:Content>
