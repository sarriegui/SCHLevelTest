<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userlist.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="SCHLevelTest.userlist" %>


<asp:Content ID="Content1" ContentPlaceHolderID="phContent" runat="server">
    <script src="/script/userlist.js"></script>
    <form id="form1" runat="server">
        <input type="hidden" id="hdUser" value="<%= Session["cryptuser"].ToString() %>" />
        <input type="hidden" id="hdPwd" value="<%= Session["cryptpwd"].ToString() %>" />
    <div>
        <p><a href="#" class="add">Nuevo user</a></p>
        <ul id="usuarios"></ul>
    </div>
    <div id="form">
        <span class="label">Id</span><input type="text" id="txid" />
        <span class="label">Nombre</span><input type="text" id="txnombre" />
        <span class="label">Home</span><input type="text" id="txhome" />
        <span class="label">Roles</span><select size="4" multiple id="cbroles" ><asp:Literal ID="litRoles" runat="server"></asp:Literal></select>
        <input type="button" id="btnAdd" value="Save" />
        <input type="button" id="btnClose" value="Close" />
    </div>
    </form>
</asp:Content>
