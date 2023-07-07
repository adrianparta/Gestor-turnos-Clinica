<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosPaciente.aspx.cs" Inherits="Clinic.TurnosPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h1 class="text-center">Bienvenido/a a la Clinica!</h1>
    <br />
    <div class="row">
        <div class="col-1"></div>
        <div class="col-5">
            <asp:GridView runat="server" ID="dgvTurnos" AutoGenerateSelectButton="true" OnSelectedIndexChanged="dgvTurnos_SelectedIndexChanged" CssClass="table table-bordered table-striped" OnRowDataBound="dgvTurnos_RowDataBound" ></asp:GridView>
        </div>
        <div class="col-5">
            <div class="row">
                <label for="txtCausas" class="form-label">Causas:</label>
                <textarea class="form-control" rows="5" id="txtCausas" readonly runat="server"></textarea>
            </div>
            <br />
            <div class="row">
                <label for="txtObservaciones" class="form-label">Observaciones:</label>
                <textarea class="form-control" rows="5" id="txtObservaciones" readonly runat="server"></textarea>
            </div>
            <br />
            <div class="row">
                <div class="col-9"></div>
                <div class="col">
                    <asp:Button Text="Cancelar Turno" ID="btnCancelarTurno" OnClick="btnCancelarTurno_Click" CssClass="btn btn-primary" runat="server" />
                </div>
            </div>
        </div>
        <div class="col-1"></div>
    </div>
        <div class="modal fade" id="modalCancelarTurno" tabindex="-1" aria-labelledby="modalCancelarTurno" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Estado del turno</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    El turno ha sido cancelado!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        function openModal() {
            $('#modalCancelarTurno').modal('show');
        }
        $(document).ready(function () {
            $('#<%= btnCancelarTurno.ClientID %>').on('click', openModal);
        });
    </script>
</asp:Content>
