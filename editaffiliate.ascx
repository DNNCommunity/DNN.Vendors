<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Control Language="C#" AutoEventWireup="True" Inherits="Dnn.Modules.Vendors.EditAffiliate" CodeBehind="EditAffiliate.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<link rel="stylesheet" href="/Resources/Shared/components/TimePicker/Themes/jquery-ui.css"/>
<link rel="stylesheet" href="/Resources/Shared/components/TimePicker/Themes/jquery.ui.theme.css"/>
<div class="dnnForm dnnEditAffiliate dnnClear" id="dnnEditAffiliate">
    <fieldset>        
        <div class="dnnFormItem">
            <dnn:label id="plStartDate" runat="server" controlname="txtStartDate" />
            <asp:TextBox ID="StartDatePicker" runat="server" maxlength="10" Columns="30" CssClass="datepick" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plEndDate" runat="server" controlname="txtEndDate" />
            <asp:TextBox ID="EndDatePicker" runat="server" maxlength="10" Columns="30" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plCPC" runat="server" controlname="txtCPC" cssclass="dnnFormRequired" />
			<asp:textbox id="txtCPC" runat="server" maxlength="7" columns="30"  />
			<asp:requiredfieldvalidator id="valCPC1" resourcekey="CPC.ErrorMessage" runat="server" controltovalidate="txtCPC" display="Dynamic" cssclass="dnnFormMessage dnnFormError" />
			<asp:comparevalidator id="valCPC2" resourcekey="CPC.ErrorMessage" runat="server" controltovalidate="txtCPC" display="Dynamic" type="Double" operator="DataTypeCheck" cssclass="dnnFormMessage dnnFormError" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plCPA" runat="server" controlname="txtCPA" cssclass="dnnFormRequired" />
			<asp:textbox id="txtCPA" runat="server" maxlength="7" columns="30"  />
			<asp:requiredfieldvalidator id="valCPA1" resourcekey="CPA.ErrorMessage" runat="server" controltovalidate="txtCPA" display="Dynamic" cssclass="dnnFormMessage dnnFormError" />
			<asp:comparevalidator id="valCPA2" resourcekey="CPA.ErrorMessage" runat="server" controltovalidate="txtCPA" display="Dynamic" type="Double" operator="DataTypeCheck" cssclass="dnnFormMessage dnnFormError" />
        </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
    	<li><asp:LinkButton id="cmdUpdate" runat="server" CssClass="dnnPrimaryAction" resourcekey="cmdUpdate" /></li>
        <li><asp:LinkButton id="cmdDelete" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdDelete" Causesvalidation="False" /></li>
        <li><asp:LinkButton id="cmdCancel" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdCancel" Causesvalidation="False" /></li>
        <li><asp:LinkButton id="cmdSend" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdSend" Causesvalidation="False" /></li>
    </ul>
</div>
<script language="javascript" type="text/javascript">
/*globals jQuery, window, Sys */
(function ($, Sys) {
    function setUpDnnEditAffiliate() {
        var yesText = '<%= Localization.GetSafeJSString("Yes.Text", Localization.SharedResourceFile) %>';
        var noText = '<%= Localization.GetSafeJSString("No.Text", Localization.SharedResourceFile) %>';
        var titleText = '<%= Localization.GetSafeJSString("Confirm.Text", Localization.SharedResourceFile) %>';
        $('#<%= cmdDelete.ClientID %>').dnnConfirm({
            text: '<%= Localization.GetSafeJSString("DeleteItem.Text", Localization.SharedResourceFile) %>',
            yesText: yesText,
            noText: noText,
            title: titleText
        });
    }
    $(document).ready(function () {
        setUpDnnEditAffiliate();
	    $("#<%= StartDatePicker.ClientID %>").datepicker();
        $("#<%= EndDatePicker.ClientID %>").datepicker();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            setUpDnnEditAffiliate();
        });
    });
} (jQuery, window.Sys));
</script>