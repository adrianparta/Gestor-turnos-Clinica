<%@ Page Title="TurnosRecepcionista" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosRecepcionista.aspx.cs" Inherits="Clinic.TurnosRecepcionista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center">
        <h1 class="mt-4 mb-4">NUEVO TURNO</h1>
    </div>
    <div class="row justify-content-center">
        <div class="col-4 row">
            <div class="col-8 p-0">
                <asp:DropDownList CssClass="form-select" ID="DropDownListPacientes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPacientes_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-4 p-0">
                <asp:Button CssClass="btn btn-primary" ID="BotonAgregarPaciente" Text="Agregar paciente" runat="server" OnClick="BotonAgregarPaciente_Click" />
            </div>
        </div>
        <div class="col-4">
            <asp:DropDownList CssClass="form-select" ID="DropDownListEspecialidad" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListEspecialidad_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>



    <div class="row justify-content-center">
        <div class="col-4 row">
            <h2 class="d-flex justify-content-left mt-4 mb-3">Turnos Sugeridos</h2>
            <asp:Button CssClass="btn btn-primary mb-1" ID="TurnoSugerido1" Text=" -- Turno sugerido 1 -- " OnClick="TurnoSugerido_Click" runat="server" />
            <asp:Button CssClass="btn btn-primary mb-1" ID="TurnoSugerido2" Text=" -- Turno sugerido 2 -- " OnClick="TurnoSugerido_Click" runat="server" />
            <asp:Button CssClass="btn btn-primary mb-4" ID="TurnoSugerido3" Text=" -- Turno sugerido 3 -- " OnClick="TurnoSugerido_Click" runat="server" />
        </div>
        <div class="col-4"></div>
    </div>


    <div class="row justify-content-center">
        <div class="col-2">
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
        </div>
        <div class="col-6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-9 p-0">
                            <asp:Calendar ID="Calendario" runat="server" OnDayRender="Calendario_DayRender" OnSelectionChanged="Calendario_SelectionChanged" AutoPostBack="false" FirstDayOfWeek="Sunday" Height="400px" Width="100%"></asp:Calendar>
                        </div>
                        <div class="col p-0">
                            <ul class="px-1">
                                <asp:Repeater ID="RepeaterHorarios" runat="server">
                                    <ItemTemplate>
                                        <asp:Button CssClass="d-block" width="100%" ID="boton" Text="<%#Container.DataItem.ToString() %>" runat="server" CommandArgument='<%#Container.DataItem.ToString() %>' CommandName="numero" OnPreRender="boton_PreRender" OnClick="Repeater_Click"/>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>

                        </div>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <asp:TextBox runat="server" ID="TextBoxCausas" />
    <asp:Button ID="Aceptar" Text="Aceptar" runat="server" OnClick="Aceptar_Click" />
    <asp:Button ID="Cancelar" Text="Cancelar" runat="server" OnClick="Cancelar_Click" />

</asp:Content>
