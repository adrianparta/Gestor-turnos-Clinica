<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="Clinic.ListarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList CssClass="form-select" ID="ddlTipoUsuario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
    <asp:DropDownList CssClass="form-select" ID="ddlAux" runat="server" AutoPostBack="true"></asp:DropDownList>

</asp:Content>
