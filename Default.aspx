<%@ Page Title="The Augur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CloudAugurTest._Default"EnableViewState="true"ViewStateMode="Enabled"ViewStateEncryptionMode="Always" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>The Augur (Prototype)&nbsp; </h1>
    </div>

    &nbsp;<br />
&nbsp;&nbsp;
    <asp:Label ID="LBEnterWarehouseNum" runat="server" Text="Enter Warehouse # and press [Enter]:   "></asp:Label>
    <asp:TextBox ID="TBWarehouseNum" runat="server" OnTextChanged="TBWarehouseNum_TextChanged"></asp:TextBox>
&nbsp;<asp:Button ID="BtnEnterWarehouseNum" runat="server" OnClick="BtnEnterWarehouseNum_Click" Text="Submit" />
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Current selected warehouse:   "></asp:Label>
    &nbsp;&nbsp;&nbsp;<asp:Label ID="LBWHNumSign" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF" Visible="False"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <asp:FileUpload ID="FileUpload" runat="server" Visible="False" />
    &nbsp;&nbsp;<br />
    &nbsp;&nbsp;&nbsp;
    <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" Visible="False" />
    &nbsp;&nbsp;<asp:Label ID="LB1" runat="server" Text="Uploaded File:   " Visible="False"></asp:Label>
    <asp:Label ID="LBUploadedFile" runat="server" BackColor="Black" Font-Bold="True" ForeColor="#66FF33" Font-Family="Fixedsys" Visible="False"></asp:Label>
    &nbsp;<asp:Label ID="LBAssignedTo" runat="server" Text="sales figures assigned to Warehouse " Visible="False"></asp:Label>
    <asp:Label ID="LBWarehouseNum" runat="server" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Panel ID="pnlCalendar" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="5px" Height="242px" Visible="False" Width="350px" Wrap="False">
        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" Visible="False"></asp:Calendar>
        <asp:Image ID="imgMVM" runat="server" src="https://i.imgur.com/r9vrRPT.jpg" Height="70px" Visible="False" Width="140px"/>
        <asp:Image ID="imgNFL" runat="server" src="https://i.imgur.com/W9anilh.png" Height="70px" Visible="False" Width="70px"/>
        <asp:Image ID="imgDAHoliday" runat="server" src="https://i.imgur.com/rzT1pij.png" Height="70px" Visible="False" Width="70px"/>
        <asp:Image ID="imgInclimateWeather" runat="server" src="https://i.imgur.com/JtEcrcv.png" Height="70px" Visible="False" Width="70px"/>
    </asp:Panel>
    <br />
&nbsp;&nbsp;
    <asp:Label ID="LB2" runat="server" Text="Last Year's Sales:  " Visible="False"></asp:Label>
    <asp:TextBox ID="TBLastYearSales" runat="server" Visible="False"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;
    <asp:Label ID="LB3" runat="server" Text="Planned Sales:  " Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TBPlannedSales" runat="server" Visible="False"></asp:TextBox>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

</asp:Content>
