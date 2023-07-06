﻿<%@ Page Title="TurnosDoctor" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosDoctor.aspx.cs" Inherits="Clinic.TurnosDoctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-dark-subtle">

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
                <asp:TextBox ID="txtPacienteSeleccionado" CssClass="form-control text-center" Text="Seleccione un paciente por favor" runat="server" />
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
                                            <div class="card-header">
                                                <asp:Label Text='<%#Eval("Fecha")%>' runat="server" />
                                            </div>
                                            <div class="card-body">
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
                            <label for="txtCausas" class="form-label">Causas:</label>
                            <textarea class="form-control" rows="5" id="txtCausas" readonly runat="server"></textarea>
                        </div>
                        <div class="row">
                            <label for="txtObservaciones" class="form-label">Observaciones:</label>
                            <textarea class="form-control" rows="5" id="txtObservaciones" runat="server"></textarea>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-9"></div>
                            <div class="col-2">
                                <asp:Button Text="Guardar Observacion" ID="btnGuardarObservacion" CssClass="btn btn-primary" OnClick="btnGuardarObservacion_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <label class="form-label justify-content-center row">Reasignar turno:</label>
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
                            <div class="col-7"></div>
                            <div class="col-3">
                                <asp:Button Text="Reasignar" ID="btnReasignarTurno" OnClick="btnReasignarTurno_Click" CssClass="btn btn-primary" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
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
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        function openModal() {
            $('#modalObservaciones').modal('show');
        }
        $(document).ready(function () {
            $('#<%= btnGuardarObservacion.ClientID %>').on('click', openModal);
        });
        function openModal() {
            $('#modalReasignar').modal('show');
        }
        $(document).ready(function () {
            $('#<%= btnReasignarTurno.ClientID %>').on('click', openModal);
        });
    </script>
</asp:Content>
