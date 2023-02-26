<%@ Control Language="C#" AutoEventWireup="false" Inherits="Dnn.Modules.Vendors.Vendors" Codebehind="Vendors.ascx.cs" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<div class="dnnForm dnnVendors dnnClear" id="dnnVendors">
	<div class="dnnFormItem">
		<asp:TextBox id="txtSearch" Runat="server" CssClass="dnnFixedSizeComboBox" />
        <asp:DropDownList id="ddlSearchType" Runat="server" CssClass="dnnFixedSizeComboBox">
            <asp:ListItem Value="name" Text="Name" />
            <asp:ListItem Value="email" Text="Email" />
        </asp:DropDownList>
		<asp:LinkButton ID="btnSearch" Runat="server" resourcekey="Search" CssClass="dnnSecondaryAction" />
	</div>
    <div class="dnnClear"></div>
	<ul class="vLetterSearch dnnClear">
		<asp:Repeater id="rptLetterSearch" Runat="server">
			<ItemTemplate>
				<li><asp:HyperLink runat="server" NavigateUrl='<%# FilterURL(Container.DataItem.ToString(),"1") %>' Text='<%# Container.DataItem %>' ID="Hyperlink2" /></li>
			</ItemTemplate>
		</asp:Repeater>
	</ul>
	<asp:DataGrid id="grdVendors" AutoGenerateColumns="False" EnableViewState="True" runat="server" AllowPaging="True" AllowCustomPaging="True" CssClass="dnnBannersGrid" >  
        <headerstyle cssclass="dnnGridHeader" verticalalign="Top"/>
        <itemstyle cssclass="dnnGridItem" horizontalalign="Left" />
        <alternatingitemstyle cssclass="dnnGridAltItem" />
        <edititemstyle cssclass="dnnFormInput" />
        <selecteditemstyle cssclass="dnnFormError" />
        <footerstyle cssclass="dnnGridFooter" />
        <pagerstyle cssclass="dnnGridPager" />
		    <Columns>
                <asp:TemplateColumn>
				    <ItemStyle Width="20px"></ItemStyle>
				    <ItemTemplate>
					    <asp:HyperLink NavigateUrl='<%# FormatURL("VendorID",DataBinder.Eval(Container.DataItem,"VendorID").ToString()) %>' Visible="<%#  CanEdit() %>" runat="server" ID="Hyperlink1">
						    <dnn:DnnImage IconKey="Edit" AlternateText="Edit" runat="server" ID="Hyperlink1Image" resourcekey="Edit"/>
					    </asp:HyperLink>
				    </ItemTemplate>
			    </asp:TemplateColumn>
                <asp:BoundColumn DataField="VendorName" HeaderText="Name" />
			    <asp:TemplateColumn HeaderText="Address">
				    <ItemTemplate>
					    <asp:Label ID="lblAddress" Runat="server" Text='<%# DisplayAddress(DataBinder.Eval(Container.DataItem, "Unit"),DataBinder.Eval(Container.DataItem, "Street"), DataBinder.Eval(Container.DataItem, "City"), DataBinder.Eval(Container.DataItem, "Region"), DataBinder.Eval(Container.DataItem, "Country"), DataBinder.Eval(Container.DataItem, "PostalCode")) %>'>
					    </asp:Label>
				    </ItemTemplate>
			    </asp:TemplateColumn>
			    <asp:BoundColumn DataField="Telephone" HeaderText="Telephone" />
			    <asp:BoundColumn DataField="Fax" HeaderText="Fax" />
			    <asp:BoundColumn DataField="Email" HeaderText="Email" />
			    <asp:TemplateColumn HeaderText="Authorized">
				    <ItemTemplate>
					    <dnn:DnnImage Runat="server" ID="Image1" IconKey="Checked" Visible='<%# DataBinder.Eval(Container.DataItem,"Authorized") %>'/>
					    <dnn:DnnImage Runat="server" ID="Image2" IconKey="Unchecked" Visible='<%# !(bool)DataBinder.Eval(Container.DataItem,"Authorized") %>'/>
				    </ItemTemplate>
			    </asp:TemplateColumn>
			    <asp:BoundColumn DataField="Banners" HeaderText="Banners" />
		    </Columns>
	</asp:DataGrid>
    <ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="cmdAddVendor" runat="server" CssClass="dnnPrimaryAction" resourcekey="AddContent.Action"  /></li>
		<li><asp:LinkButton id="cmdDeleteUnAuthorized" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdDelete.Text" /></li>
	</ul>

</div>
<script language="javascript" type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function setUpDnnVendors() {
            $('#<%= cmdDeleteUnAuthorized.ClientID %>').dnnConfirm({
                text: '<%= Localization.GetSafeJSString("DeleteItems.Text", Localization.SharedResourceFile) %>',
                yesText: '<%= Localization.GetSafeJSString("Yes.Text", Localization.SharedResourceFile) %>',
                noText: '<%= Localization.GetSafeJSString("No.Text", Localization.SharedResourceFile) %>',
                title: '<%= Localization.GetSafeJSString("Confirm.Text", Localization.SharedResourceFile) %>'
            });
        }

        $(document).ready(function () {
            setUpDnnVendors();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setUpDnnVendors();
            });
        });
    }(jQuery, window.Sys));
</script>