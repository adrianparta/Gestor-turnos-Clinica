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
                <asp:Button Text="<-- Semana Anterior" CssClass="btn btn-primary" runat="server" />
            </div>
            <div class="col-4">
                <asp:DropDownList CssClass="form-select" ID="ddlFecha" runat="server"></asp:DropDownList>
            </div>
            <div class="col-1"></div>
            <div class="col-3">
                <asp:Button Text="Semana Siguiente -->" CssClass="btn btn-primary" runat="server" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-1"></div>
            <div class="col-10">
                <div class="row row-cols-md-5 g-4">
                    <div class="col">
                        <div class="card">
                            <div class="card-header">
                                Lunes 12/06
                            </div>
                            <div class="card-body">
                                <asp:ListBox CssClass="form-select" ID="lbTurnos" runat="server">
                                    <asp:ListItem Text="11:30 Sbernini Agustin" />
                                    <asp:ListItem Text="11:30 Sbernini Agustin" />
                                    <asp:ListItem Text="11:30 Sbernini Agustin" />
                                    <asp:ListItem Text="11:30 Sbernini Agustin" />
                                    <asp:ListItem Text="11:30 Sbernini Agustin" />
                                </asp:ListBox>
                            </div>
                        </div>
                    </div>
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
                        <asp:Button Text="Guardar Observacion" ID="btnGuardarObservacion" CssClass="btn btn-primary" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-4">
                <label class="form-label justify-content-center row">Reasignar turno:</label>
                <div class="row">
                    <div class="col-1"></div>
                    <div class="col-5">
                        <asp:TextBox CssClass="form-control" ID="txtDia" TextMode="Date" runat="server" />
                    </div>
                    <div class="col-5">
                        <asp:DropDownList CssClass="form-select" ID="ddlHorario" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-7"></div>
                    <div class="col-3">
                        <asp:Button Text="Reasignar" ID="btnReasignarTurno" CssClass="btn btn-primary" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
