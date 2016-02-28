<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagC.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="SCHLevelTest.pagC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="phContent" runat="server">
    <form id="form1" runat="server">
    <div>
        Hola <%=nombreUsuario %>
    </div>
    </form>
</asp:Content>