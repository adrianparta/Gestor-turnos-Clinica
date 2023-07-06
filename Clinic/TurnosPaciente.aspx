<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosPaciente.aspx.cs" Inherits="Clinic.TurnosPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h1 class="text-center">Bienvenido/a a la Clinica!</h1>
    <div class="row">
        <div class="col-1"></div>
        <div class="col-5">
            <asp:ListBox CssClass="form-select" SelectionMode="Multiple" runat="server" ID="lbTurnos" OnSelectedIndexChanged="lbTurnos_SelectedIndexChanged"></asp:ListBox>
            
        </div>
        <div class="col-5">

        </div>
        <div class="col-1"></div>
    </div>
</asp:Content>
