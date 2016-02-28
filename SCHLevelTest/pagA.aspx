<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="pagA.aspx.cs" Inherits="SCHLevelTest.pagA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" runat="server">
    <form id="form1" runat="server">
    <div>
        Hola <%= nombreUsuario %>
    </div>
    </form>
</asp:Content>
