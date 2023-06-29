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

                <% if (idUsuarioModificar == 0 || idUsuarioModificar == idUsuarioActual)
                    {%>

                <label for="txtPassword" class="d-flex form-label">Ingrese contraseña</label>
                <asp:TextBox TextMode="Password" ID="txtPassword" CssClass="form-control" placeholder="Password" runat="server" />

                <%  } %>

                <%if (esAdmin || idUsuarioModificar == idUsuarioActual)
                    {%>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <%if (esAdmin)
                            { %>
                        <label for="ddlTipoUsuario" class="d-flex form-label">Seleccione Tipo de Usuario</label>
                        <asp:DropDownList CssClass="form-select" ID="ddlTipoUsuario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
                        <%} %>
                        <%  switch (tipoUsuarioRegistro)
                            {
                                case TipoUsuario.Doctor: %>
                        <label for="ddlDia" class="d-flex form-label">Seleccione Horario Laboral (Entrada - Salida)</label>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-3">
                                        <asp:DropDownList ID="ddlDia" CssClass="form-select" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtHorarioEntrada" CssClass="form-control" TextMode="Number" Text="8" min="8" max="20" runat="server" />
                                            <span class="input-group-text">:00</span>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtHorarioSalida" CssClass="form-control" TextMode="Number" Text="9" min="9" max="21" runat="server" />
                                            <span class="input-group-text">:00</span>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <asp:Button ID="btnAgregarHorario" CssClass="btn btn-primary" Text="Agregar" runat="server" OnClick="btnAgregarHorario_Click" />
                                    </div>
                                </div>
                                <label for="lbHorario" class="d-flex form-label">Horarios Seleccionados</label>
                                <asp:ListBox ID="lbHorario" CssClass="form-select" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                <div class="row">
                                    <div class="col-9"></div>
                                    <div class="col-3">
                                        <asp:Button ID="btnEliminarHorario" CssClass="btn btn-primary" Text="Eliminar" runat="server" OnClick="btnEliminarHorario_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <label for="ddlEspecialidad" class="d-flex form-label">Seleccione Especialidad</label>
                                <div class="row">
                                    <div class="col-9">
                                        <asp:DropDownList ID="ddlEspecialidad" CssClass="form-select" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-3">
                                        <asp:Button ID="btnAgregarEspecialidad" CssClass="btn btn-primary" Text="Agregar" runat="server" OnClick="btnAgregarEspecialidad_Click" />
                                    </div>
                                </div>
                                <label for="lbEspecialidad" class="d-flex form-label">Especialidades Seleccionadas</label>
                                <asp:ListBox ID="lbEspecialidad" CssClass="form-select" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                <div class="row">
                                    <div class="col-9"></div>
                                    <div class="col-3">
                                        <asp:Button ID="btnEliminarEspecialidad" CssClass="btn btn-primary" Text="Eliminar" runat="server" OnClick="btnEliminarEspecialidad_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <% break;
                            case TipoUsuario.Paciente: %>

                        <label for="txtDni" class="d-flex form-label">Ingrese Dni</label>
                        <asp:TextBox ID="txtDniAdmin" TextMode="Number" CssClass="form-control" placeholder="Dni" runat="server" />

                        <label for="txtDireccion" class="d-flex form-label">Ingrese Direccion</label>
                        <asp:TextBox ID="txtDireccionAdmin" CssClass="form-control" placeholder="Direccion" runat="server" />

                        <label for="txtObraSocial" class="d-flex form-label">Ingrese Obra Social</label>
                        <asp:TextBox ID="txtObraSocialAdmin" CssClass="form-control" placeholder="Obra Social" runat="server" />

                        <label for="txtFechaNacimiento" class="d-flex form-label">Seleccione Fecha de Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimientoAdmin" TextMode="Date" CssClass="form-control" placeholder="Fecha de Nacimiento" runat="server" />

                        <label for="ddlSexo" class="d-flex form-label">Seleccione Sexo</label>
                        <asp:DropDownList ID="ddlSexoAdmin" CssClass="form-select" runat="server"></asp:DropDownList>

                        <% break; } %>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <% } else { %>

                <label for="txtDni" class="d-flex form-label">Ingrese Dni</label>
                <asp:TextBox ID="txtDni" TextMode="Number" CssClass="form-control" placeholder="Dni" runat="server" />

                <label for="txtDireccion" class="d-flex form-label">Ingrese Direccion</label>
                <asp:TextBox ID="txtDireccion" CssClass="form-control" placeholder="Direccion" runat="server" />

                <label for="txtObraSocial" class="d-flex form-label">Ingrese Obra Social</label>
                <asp:TextBox ID="txtObraSocial" CssClass="form-control" placeholder="Obra Social" runat="server" />

                <label for="txtFechaNacimiento" class="d-flex form-label">Seleccione Fecha de Nacimiento</label>
                <asp:TextBox ID="txtFechaNacimiento" TextMode="Date" CssClass="form-control" placeholder="Fecha de Nacimiento" runat="server" />

                <label for="ddlSexo" class="d-flex form-label">Seleccione Sexo</label>
                <asp:DropDownList ID="ddlSexo" CssClass="form-select" runat="server"></asp:DropDownList>
                <%  } %>
                <br />
                <div class="position-absolute start-50">
                    <asp:Button Text="Cancelar" ID="btnCancelar" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnCancelar_Click" />
                    <asp:Button Text="Registrar" ID="btnRegistrar" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnRegistrar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
