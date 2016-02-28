<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SCHLevelTest.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        label{float:left;clear:left;width:100px}
        input {float:left;clear:right}
        .Ctr{float:left;clear:both}
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
</head>
<body>
    <form id="frmLogin" runat="server" class="ValidateForm">
        <div class="Lg Rgt">
            <p>
                <label class="lbForm" for="<%= txtUsuario.ClientID%>">E-mail</label>
                <input class="txForm err_obligatorio err_mail" id="txtUsuario" type="text" maxlength="50" name="txtUsuario" runat="server" />
                <label class="lbForm" for="<%= txtPassword.ClientID%>">Contraseña</label>
                <input class="txForm err_obligatorio" id="txtPassword" type="password" maxlength="50" name="txtPassword" runat="server" />
	            <div class="Ctr">
		            <input id="btnLogin" runat="server" type="submit" value="Entrar" onserverclick="btnLogin_Click" />
	            </div>
            </p>
        </div>
    </form>
</body>
    <script language="javascript" type="text/javascript">
        $(function () { $("#<%= txtUsuario.ClientID%>").focus(); });
    </script>
</html>
