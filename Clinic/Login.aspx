<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Clinic.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <div class="row">
            <div class="col-md-4 mx-auto">
                <img class="mb-4" src="..\..\Content\ClinicaIcono.png" alt="" width="72" height="72">
                <h1 class="h3 mb-3 font-weight-normal">Bienvenido a la Clinica</h1>
                <label for="txtEmail" class="d-flex form-label">Ingrese su Email</label>
                <asp:TextBox  TextMode="Email" ID="txtEmail" CssClass="form-control" placeholder="Email" runat="server" />
                <label for="txtPassword" class="d-flex form-label">Ingrese su contraseña</label>
                <asp:TextBox TextMode="Password" ID="txtPassword" CssClass="form-control" placeholder="Password" runat="server" />
                <br />
                <div class="position-absolute start-50">
                    <asp:button text="Registrarse" ID="btnRegistrarse" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnRegistrarse_Click" />
                    <asp:button text="Ingresar" TabIndex="1" ID="btnIngresar" CssClass="btn btn-lg btn-primary" runat="server" OnClick="btnIngresar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
