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
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Insert BLOB into database" />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Image ID="Image1" runat="server" Height="211px" Width="193px" />
            <br />
            <br />
          <asp:FileUpload ID="FileUpload1" runat="server" />
<asp:Button Text="Upload" runat="server" OnClick="UploadFile" />
            <br />
<hr />
<asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="gvImages_SelectedIndexChanged" Width="850px">
    <Columns>
        <asp:BoundField HeaderText="File Id" DataField="FileId" />
        <asp:BoundField HeaderText="File Name" DataField="FileName" />
        <asp:TemplateField HeaderText = "Image">
            <ItemTemplate>
                <asp:Image ID="Image1" runat="server" Height="80" Width="80" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>



        </div>
    </form>
</body>
</html>
