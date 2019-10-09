<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadimage.aspx.cs" Inherits="propertyimaging.uploadimage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Get data" />
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Get BLOB data" />
            <br />
        </div>
    </form>
</body>
</html>
