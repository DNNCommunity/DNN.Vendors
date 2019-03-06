<%@ Control Language="C#" Inherits="Dnn.Modules.Vendors.DisplayBanners" AutoEventWireup="True" CodeBehind="DisplayBanners.ascx.cs" %>
<asp:DataList ID="lstBanners" runat="server" summary="Banner Design Table">

    <ItemTemplate>
        <asp:Label ID="lblItem" runat="server" Text='<%# FormatItem((int)DataBinder.Eval(Container.DataItem,"VendorId"), (int)DataBinder.Eval(Container.DataItem,"BannerId"), (int)DataBinder.Eval(Container.DataItem,"BannerTypeId"),(string)DataBinder.Eval(Container.DataItem,"BannerName"),(string)DataBinder.Eval(Container.DataItem,"ImageFileUrl"),(string)DataBinder.Eval(Container.DataItem,"Description"), (string)DataBinder.Eval(Container.DataItem,"Url"), (int)DataBinder.Eval(Container.DataItem,"Width"), (int)DataBinder.Eval(Container.DataItem,"Height")) %>'></asp:Label></ItemTemplate>
</asp:DataList>
<asp:PlaceHolder ID="phBanner" runat="server"></asp:PlaceHolder>
