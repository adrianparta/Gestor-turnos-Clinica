<%@ Page Title="TurnosMed" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosMed.aspx.cs" Inherits="Clinic.TurnosMed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-dark-subtle">

        <br />
        <br />
        <div class="row">
            <div class="col-1"></div>
            <div class="col-3">
                <asp:Button Text="<-- Semana Anterior" ID="btnSemanaAnterior" CssClass="btn btn-primary" OnClick="btnSemanaAnterior_Click" runat="server" />
            </div>
            <div class="col-4"></div>
            <div class="col-1"></div>
            <div class="col-3">
                <asp:Button Text="Semana Siguiente -->" ID="btnSemanaSiguiente" CssClass="btn btn-primary" OnClick="btnSemanaSiguiente_Click" runat="server" />
            </div>
        </div>
        <br />
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
                                        <asp:ListBox CssClass="form-select" Rows="10" ID="lbTurnos" DataSource='<%#Eval("Turnos")%>' DataTextField="TurnoEnTexto" DataValueField="IdTurno" OnSelectedIndexChanged="lbTurnos_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        </asp:ListBox>
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
    </div>

</asp:Content>
