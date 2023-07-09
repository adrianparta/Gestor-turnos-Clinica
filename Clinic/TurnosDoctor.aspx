<%@ Page Title="TurnosDoctor" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosDoctor.aspx.cs" Inherits="Clinic.TurnosDoctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
        <br />
        <div class="row">
            <div class="col-1"></div>
            <div class="col-10">
                <div class="col d-md-flex justify-content-between">
                    <asp:Button Text="<-- Semana Anterior" ID="btnSemanaAnterior" CssClass="btn btn-primary" OnClick="btnSemanaAnterior_Click" runat="server" />
                    <asp:Button Text="--> Ir al Día Actual <--" ID="btnHoy" CssClass="btn btn-primary" OnClick="btnHoy_OnClick" runat="server" />
                    <asp:Button Text="Semana Siguiente -->" ID="btnSemanaSiguiente" CssClass="btn btn-primary" OnClick="btnSemanaSiguiente_Click" runat="server" />
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-3"></div>
            <div class="col d-md-flex justify-content-center">
                <asp:Label ID="txtPacienteSeleccionado" CssClass="text-center" Font-Bold="true" Font-Size="Large" Text="Seleccione un paciente por favor" runat="server" />
            </div>
            <div class="col-3"></div>
        </div>
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-1"></div>
                    <div class="col-10">
                        <div class="row row-cols-md-4 g-4">
                            <asp:Repeater runat="server" ID="repeaterTurnos">
                                <ItemTemplate>
                                    <div class="col">
                                        <div class="card">
                                            <div class="card-header bg-secondary-subtle text-center">
                                                <asp:Label Font-Bold="true" Text='<%#Eval("Fecha")%>' runat="server" />
                                            </div>
                                            <div class="card-body bg-dark-subtle">
                                                <asp:ListBox CssClass="form-select" Rows="10" ID="lbTurnos" DataSource='<%#Eval("Turnos")%>' DataTextField="TurnoEnTexto" DataValueField="IdTurno" OnSelectedIndexChanged="lbTurnos_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-1"></div>
                    <div class="col-7">
                        <div class="row">
                            <asp:Label Text="Causas:" Font-Bold="true" CssClass="p-2" runat="server" />
                            <textarea class="form-control" rows="5" id="txtCausas" readonly runat="server"></textarea>
                        </div>
                        <br />
                        <div class="row">
                            <asp:Label Text="Observaciones:" Font-Bold="true" CssClass="p-2" runat="server" />
                            <textarea class="form-control" rows="5" id="txtObservaciones" runat="server"></textarea>
                        </div>
                        <br />
                        <div class="row">
                            <div class="d-flex justify-content-end mt-3">
                                <asp:Button Text="Guardar Observacion" ID="btnGuardarObservacion" CssClass="btn btn-primary" OnClick="btnGuardarObservacion_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <asp:Label Text="Reasignar turno:" CssClass="form-label justify-content-center row" Font-Bold="true" runat="server" />
                        <div class="row">
                            <div class="col-1"></div>
                            <div class="col-5">
                                <asp:TextBox CssClass="form-control" ID="txtDia" OnTextChanged="txtDia_TextChanged" TextMode="Date" runat="server" />
                            </div>
                            <div class="col-5">
                                <asp:DropDownList CssClass="form-select" ID="ddlHorario" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-8"></div>
                            <div class="col-3">
                                <asp:Button Text="Reasignar" ID="btnReasignarTurno" OnClick="btnReasignarTurno_Click" CssClass="btn btn-primary" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    <div class="modal fade" id="modalObservaciones" tabindex="-1" aria-labelledby="modalObservaciones" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Exito!</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Las observaciones fueron guardadas con exito!
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Cerrar" CssClass="btn btn-primary" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalReasignar" tabindex="-1" aria-labelledby="modalReasignar" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Exito!</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    El paciente ha sido reasignado con exito!
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Cerrar" CssClass="btn btn-primary" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <script>
        function openModalObservaciones() {
            $('#modalObservaciones').modal('show');
        }
        $(document).ready(function () {
            $('#<%= btnGuardarObservacion.ClientID %>').on('click', openModalObservaciones);
        });
        function openModalReasignar() {
            $('#modalReasignar').modal('show');
        }
        $(document).ready(function () {
            $('#<%= btnReasignarTurno.ClientID %>').on('click', openModalReasignar);
        });
    </script>
</asp:Content>
