<%@ Page Title="ListarUsuarios" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="Clinic.ListarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="justify-content-center d-flex m-4">
        <h1>Listar Usuarios</h1>
    </div>
    <div class="justify-content-center d-flex m-4">
        <asp:DropDownList CssClass="btn btn-light dropdown-toggle text-start" ID="ddlTipoUsuario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
        <asp:Button ID="BotonAgregar" CssClass="btn-primary btn mx-2" Text="Agregar usuario" runat="server" OnClick="BotonAgregar_Click" />
    </div>
    <div class="justify-content-center d-flex">

        <div class="row col-4">
            <ul class="list-group">
                <asp:Repeater runat="server" ID="repeaterLista">
                    <ItemTemplate>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div class="ms-2 me-auto">
                                <div class="fw-bold"><%#Eval("Nombre")%> <%#Eval("Apellido")%></div>
                                <%#Eval("Email")%>
                            </div>
                            <asp:Button ID="BotonModificar" CssClass="btn btn-primary btn-sm mx-1" Text="Modificar" runat="server" CommandArgument='<%#Eval("IdUsuario") %>' CommandName="Id" OnClick="BotonModificar_Click" />
                            <asp:Button ID="BotonELiminar" CssClass="btn btn-primary btn-sm mx-1" Text="Eliminar" runat="server" CommandArgument='<%#Eval("IdUsuario") %>' CommandName="Id" OnClick="BotonELiminar_Click" />

                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
