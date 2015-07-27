<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BobExample2.aspx.cs" Inherits="BobExample1.BobExample2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-family: Georgia, "Times New Roman", Times, serif;
        }
        .auto-style2 {
            color: #FF0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>Heading 1</h1>
        <h2>Heading 2</h2>
        <h3>Heading 3</h3>
        <h4>Heading 4</h4>
        <h5>
            <br />
            Heading 5</h5>
        <h6>Heading 6</h6>
        <p>
            &nbsp;</p>
        <p class="auto-style1">
            <span class="auto-style2">Style</span> this page</p>
        <p class="auto-style1">
            <a href="http://www.google.com">Hyperlink 1</a></p>
        <p class="auto-style1">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://www.google.com" Target="_blank">HyperLink</asp:HyperLink>
        </p>
        <p class="auto-style1">
            &nbsp;</p>
    
    </div>
    </form>
</body>
</html>
