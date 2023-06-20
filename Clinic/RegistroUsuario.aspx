<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="Clinic.RegistroUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="text-center">
        <div class="row">
            <div class="col-md-4 mx-auto">
                <img class="mb-4" src="..\..\Content\ClinicaIcono.png" alt="" width="72" height="72">
                <h1 class="h3 mb-3 font-weight-normal">Registro de Usuario para la Clinica</h1>
               
                <label for="txtNombre" class="d-flex form-label">Ingrese su Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server" />
                
                <label for="txtApellido" class="d-flex form-label">Ingrese su Apellido</label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="Apellido" runat="server" />
                
                <label for="txtEmail" class="d-flex form-label">Ingrese su Email</label>
                <asp:TextBox TextMode="Email" ID="txtEmail" CssClass="form-control" placeholder="Email" runat="server" />
                
                <label for="txtPassword" class="d-flex form-label">Ingrese su contraseña</label>
                <asp:TextBox TextMode="Password" ID="txtPassword" CssClass="form-control" placeholder="Password" runat="server" />
                <%if (esAdmin)
                    {%>
                <label for="txtPassword" class="d-flex form-label">Seleccione su Tipo de Usuario</label>
                <asp:DropDownList CssClass="form-select" ID="ddlTipoUsuario" runat="server"></asp:DropDownList>
                <%  } %>
                <br />
                <div class="position-absolute start-50">
                    <asp:button text="Cancelar" ID="btnCancelar" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnCancelar_Click" />
                    <asp:button text="Aceptar" ID="btnAceptar" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnAceptar_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
