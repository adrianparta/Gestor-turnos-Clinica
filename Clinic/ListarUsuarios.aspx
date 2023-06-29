<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="Clinic.ListarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList CssClass="form-select" ID="ddlTipoUsuario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
    <ul class="list-group-item ">
        <asp:Repeater runat="server" ID="repeaterLista">
            <ItemTemplate>
                <li>
                    <div class="ms-2 me-auto">
                        <div class="fw-bold"><%#Eval("Nombre")%> <%#Eval("Apellido")%></div>
                        <%#Eval("Email")%>

                    </div>
                    <asp:Button ID="BotonModificar" cssClass="btn btn-primary" Text="Modificar" runat="server" CommandArgument='<%#Eval("IdUsuario") %>' CommandName="Id" OnClick="BotonModificar_Click"/>
                    <asp:Button ID="BotonELiminar" cssClass="btn btn-primary" Text="Eliminar" runat="server" CommandArgument='<%#Eval("IdUsuario") %>' CommandName="Id" OnClick="BotonELiminar_Click"/>

                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
