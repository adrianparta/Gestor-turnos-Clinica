<%@ Page Title="RegistroUsuario" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="Clinic.RegistroUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="text-center">
        <div class="row">
            <div class="col-md-4 mx-auto">
                <img class="mb-4" src="..\..\Content\ClinicaIcono.png" alt="" width="72" height="72">
                <h1 class="h3 mb-3 font-weight-normal">Registro de Usuario para la Clinica</h1>
               
                <label for="txtNombre" class="d-flex form-label">Ingrese Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server" />
                
                <label for="txtApellido" class="d-flex form-label">Ingrese Apellido</label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="Apellido" runat="server" />
                
                <label for="txtEmail" class="d-flex form-label">Ingrese Email</label>
                <asp:TextBox TextMode="Email" ID="txtEmail" CssClass="form-control" placeholder="Email" runat="server" />
                
                <% if(idUsuarioModificar == 0) {%>

                <label for="txtPassword" class="d-flex form-label">Ingrese contraseña</label>
                <asp:TextBox TextMode="Password" ID="txtPassword" CssClass="form-control" placeholder="Password" runat="server" />
               
                <%  } %>

                <%if (esAdmin) {%>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <label for="ddlTipoUsuario" class="d-flex form-label">Seleccione Tipo de Usuario</label>
                        <asp:DropDownList CssClass="form-select" ID="ddlTipoUsuario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged" ></asp:DropDownList>
                        <% switch(tipoUsuarioRegistro)
                            {
                            case TipoUsuario.Doctor: %>

                                <% break;
                            case TipoUsuario.Paciente: %> 
                                
                                <label for="txtDni" class="d-flex form-label">Ingrese Dni</label>
                                <asp:TextBox ID="txtDniAdmin" CssClass="form-control" placeholder="Dni" runat="server" />

                                <label for="txtDireccion" class="d-flex form-label">Ingrese Direccion</label>
                                <asp:TextBox ID="txtDireccionAdmin" CssClass="form-control" placeholder="Direccion" runat="server" />

                                <label for="txtObraSocial" class="d-flex form-label">Ingrese Obra Social</label>
                                <asp:TextBox ID="txtObraSocialAdmin" CssClass="form-control" placeholder="Obra Social" runat="server" />

                                <label for="txtFechaNacimiento" class="d-flex form-label">Seleccione Fecha de Nacimiento</label>
                                <asp:TextBox ID="txtFechaNacimientoAdmin" TextMode="Date" CssClass="form-control" placeholder="Fecha de Nacimiento" runat="server" />
                
                                <label for="ddlSexo" class="d-flex form-label">Seleccione Sexo</label>
                                <asp:DropDownList ID="ddlSexoAdmin" CssClass="form-select" runat="server"> </asp:DropDownList>
                        
                                <% break;
                            } %>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <% }else{ %>

                <label for="txtDni" class="d-flex form-label">Ingrese Dni</label>
                <asp:TextBox ID="txtDni" CssClass="form-control" placeholder="Dni" runat="server" />

                <label for="txtDireccion" class="d-flex form-label">Ingrese Direccion</label>
                <asp:TextBox ID="txtDireccion" CssClass="form-control" placeholder="Direccion" runat="server" />

                <label for="txtObraSocial" class="d-flex form-label">Ingrese Obra Social</label>
                <asp:TextBox ID="txtObraSocial" CssClass="form-control" placeholder="Obra Social" runat="server" />

                <label for="txtFechaNacimiento" class="d-flex form-label">Seleccione Fecha de Nacimiento</label>
                <asp:TextBox ID="txtFechaNacimiento" TextMode="Date" CssClass="form-control" placeholder="Fecha de Nacimiento" runat="server" />
                
                <label for="ddlSexo" class="d-flex form-label">Seleccione Sexo</label>
                <asp:DropDownList ID="ddlSexo" CssClass="form-select" runat="server"> </asp:DropDownList>
                <%  } %>
                <br />
                <div class="position-absolute start-50">
                    <asp:button text="Cancelar" ID="btnCancelar" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnCancelar_Click" />
                    <asp:button text="Registrar" ID="btnRegistrar" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnRegistrar_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
