﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SongMetaData.ascx.cs" Inherits="OpenChords.Web.Controls.SongMetaData" %>

<asp:Panel ID="pnlSongMetaData" runat="server" GroupingText="Song Meta Data" CssClass="BoxPanel gradientBoxesWithOuterShadows SongMetaPanel">
    <asp:Panel ID="MetaPane1" runat="server" CssClass="Inline MetaSubPanel">
        <label>Title</label>
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
        <label>Order</label>
        <asp:TextBox ID="txtOrder" runat="server" OnTextChanged="txtOrder_TextChanged" AutoPostBack="true"></asp:TextBox><br />
        <label>Author</label>
        <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox><br />
        <label>Copyright</label>
        <asp:TextBox ID="txtCopyright" runat="server"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="MetaPane2" runat="server" CssClass="Inline MetaSubPanel">
        <label>Tempo</label>
        <asp:DropDownList ID="ddlTempo" runat="server"></asp:DropDownList><br />
        <label>Signature</label>
        <asp:DropDownList ID="ddlSignature" runat="server"></asp:DropDownList><br />
        <label>ccli</label>
        <asp:TextBox ID="txtCcli" runat="server"></asp:TextBox><br />
        <label>Reference</label>
        <asp:TextBox ID="txtReference" runat="server"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="MetaPane3" runat="server" CssClass="Inline">
        <asp:Panel ID="pnlKey" runat="server">
            <label>Key</label>
            <asp:TextBox ID="txtKey" runat="server" CssClass="BigOneLineTextbox"></asp:TextBox>
            <asp:Panel ID="pnlKeyButtons" runat="server" CssClass="Inline SmallButtonPanel">
                <asp:ImageButton ID="imgKeyUp" runat="server" SkinID="imgUp" OnClick="imgKeyUp_Click" />
                <asp:ImageButton ID="ImageKeyDown" runat="server" SkinID="imgDown" OnClick="ImageKeyDown_Click" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlCapo" runat="server">
            <label>Capo</label>
            <asp:TextBox ID="txtCapo" runat="server" CssClass="BigOneLineTextbox"></asp:TextBox>
            <asp:Panel ID="pnlCapoButtons" runat="server" CssClass="Inline SmallButtonPanel">
                <asp:ImageButton ID="imgCapoUp" runat="server" SkinID="imgUp" OnClick="imgCapoUp_Click" />
                <asp:ImageButton ID="imgCapoDown" runat="server" SkinID="imgDown" OnClick="imgCapoDown_Click" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
