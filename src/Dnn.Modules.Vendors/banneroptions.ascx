<%@ Control Language="C#" AutoEventWireup="True" Inherits="Dnn.Modules.Vendors.BannerOptions" CodeBehind="BannerOptions.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.WebControls" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web.Deprecated" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<div class="dnnForm dnnBannerOptions dnnClear">
    <div class="dnnFormItem">
        <dnn:label id="plDisplay" runat="server" controlname="optSource" suffix=":" />
        <asp:RadioButtonList ID="optDisplay" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="dnnFormRadioButtons" OnSelectedIndexChanged="optDisplay_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="T" resourcekey="Template">Host</asp:ListItem>
            <asp:ListItem Value="L" resourcekey="Legacy">Site</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="dnnFormItem">
        <dnn:label id="plSource" runat="server" controlname="optSource" suffix=":" />
        <asp:RadioButtonList ID="optSource" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="dnnFormRadioButtons">
            <asp:ListItem Value="G" resourcekey="Host">Host</asp:ListItem>
            <asp:ListItem Value="L" resourcekey="Site">Site</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="dnnFormItem">
        <dnn:label id="plType" runat="server" controlname="cboType" suffix=":" />
        <asp:DropDownList ID="cboType" runat="server" DataTextField="BannerTypeName" DataValueField="BannerTypeId" />
    </div>
    <div class="dnnFormItem">
        <dnn:label id="plGroup" runat="server" controlname="DNNTxtBannerGroup" suffix=":" />
        <asp:TextBox ID="DNNTxtBannerGroup" runat="server"></asp:TextBox>
    </div>
    <div class="dnnFormItem">
        <dnn:label id="plCount" runat="server" controlname="txtCount" suffix=":" />
        <asp:TextBox ID="txtCount" runat="server" />
        <asp:RegularExpressionValidator ID="valCount" ControlToValidate="txtCount" ValidationExpression="^[0-9]*$" Display="Dynamic" resourcekey="valCount.ErrorMessage" runat="server" CssClass="dnnFormMessage dnnFormError" />
    </div>
    <asp:Panel ID="pnlTemplate" runat="server" Visible="False">
        <div class="dnnFormItem">
            <dnn:label id="plHeader" runat="server" controlname="txtHeader" suffix=":" />
            <asp:TextBox ID="txtHeader" runat="server" Rows="4" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plContent" runat="server" controlname="txtContent" suffix=":" />
            <asp:TextBox ID="txtContent" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plFooter" runat="server" controlname="txtFooter" suffix=":" />
            <asp:TextBox ID="txtFooter" runat="server" Rows="4" TextMode="MultiLine"></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlLegacy" runat="server">
        <div class="dnnFormItem">
            <dnn:label id="plOrientation" runat="server" controlname="optOrientation" suffix=":" />
            <asp:RadioButtonList ID="optOrientation" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="dnnFormRadioButtons">
                <asp:ListItem Value="V" resourcekey="Vertical">Vertical</asp:ListItem>
                <asp:ListItem Value="H" resourcekey="Horizontal">Horizontal</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plBorder" runat="server" controlname="txtBorder" suffix=":" />
            <asp:TextBox ID="txtBorder" runat="server" />
            <asp:RegularExpressionValidator ID="valBorder" ControlToValidate="txtBorder" ValidationExpression="^[0-9]*$" Display="Dynamic" resourcekey="valBorder.ErrorMessage" runat="server" CssClass="dnnFormMessage dnnFormError" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plBorderColor" runat="server" controlname="txtBorderColor" suffix=":" />
            <asp:TextBox ID="txtBorderColor" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plPadding" suffix=":" controlname="txtPadding" runat="server" />
            <asp:TextBox ID="txtPadding" runat="server" />
            <asp:CompareValidator ID="valPadding" runat="server" resourcekey="valPadding.ErrorMessage" Display="Dynamic" ControlToValidate="txtPadding" Operator="DataTypeCheck" Type="Integer" CssClass="dnnFormMessage dnnFormError" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plRowHeight" runat="server" controlname="txtRowHeight" suffix=":" />
            <asp:TextBox ID="txtRowHeight" runat="server" />
            <asp:RegularExpressionValidator ID="valRowHeight" ControlToValidate="txtRowHeight" ValidationExpression="^[0-9]*$" Display="Dynamic" resourcekey="valRowHeight.ErrorMessage" runat="server" CssClass="dnnFormMessage dnnFormError" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plColWidth" runat="server" controlname="txtColWidth" suffix=":" />
            <asp:TextBox ID="txtColWidth" runat="server" />
            <asp:RegularExpressionValidator ID="valColWidth" ControlToValidate="txtColWidth" ValidationExpression="^[0-9]*$" Display="Dynamic" resourcekey="valColWidth.ErrorMessage" runat="server" CssClass="dnnFormMessage dnnFormError" />
        </div>
    </asp:Panel>
    <div class="dnnFormItem">
        <dnn:label id="plBannerClickThroughURL" runat="server" controlname="txtBannerClickThroughURL" suffix=":" />
        <asp:TextBox ID="txtBannerClickThroughURL" runat="server" />
        <asp:RegularExpressionValidator ID="valBannerClickThroughURL" ControlToValidate="txtBannerClickThroughURL" ValidationExpression="(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" Display="Dynamic" resourcekey="valBannerClickThroughURL.ErrorMessage" runat="server" CssClass="dnnFormMessage dnnFormError" />
    </div>
    <ul class="dnnActions dnnClear">
        <li>
            <asp:LinkButton ID="cmdUpdate" Text="Update" resourcekey="cmdUpdate" runat="server" class="dnnPrimaryAction" /></li>
        <li>
            <asp:LinkButton ID="cmdCancel" Text="Cancel" resourcekey="cmdCancel" CausesValidation="False" runat="server" class="dnnSecondaryAction" /></li>
    </ul>
</div>
