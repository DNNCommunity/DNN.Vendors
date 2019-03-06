#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;
using System.Drawing;
using System.Web.UI.WebControls;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using Dnn.Modules.Vendors.Components;
using System.Web.UI;
using System.Text;
using System.Collections;

#endregion

namespace Dnn.Modules.Vendors
{
    public partial class DisplayBanners : PortalModuleBase, IActionable
    {
        int intPortalId = 0;
        int intBannerTypeId = 0;
        string strBannerGroup;
        int intBanners = 0;

        #region IActionable Members

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(),
                            Localization.GetString(ModuleActionType.AddContent, LocalResourceFile),
                            ModuleActionType.AddContent,
                            "",
                            "",
                            EditUrl(),
                            false,
                            SecurityAccessLevel.Edit,
                            true,
                            false);
                return Actions;
            }
        }

        #endregion

        /// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataReader of banner information from the Banners
        /// table, and then databind the results to a templated DataList
        /// server control.  It uses the DotNetNuke.BannerDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //exit without displaying banners to crawlers
            if (Request.Browser.Crawler)
            {
                return;
            }
            try
            {


                //banner parameters
                switch (Convert.ToString(Settings["bannersource"]))
                {
                    case "L": //local
                    case "":
                        intPortalId = PortalId;
                        break;
                    case "G": //global
                        intPortalId = Null.NullInteger;
                        break;
                }
                if (!String.IsNullOrEmpty(Convert.ToString(Settings["bannertype"])))
                {
                    intBannerTypeId = Int32.Parse(Convert.ToString(Settings["bannertype"]));
                }
                strBannerGroup = Convert.ToString(Settings["bannergroup"]);
                if (!String.IsNullOrEmpty(Convert.ToString(Settings["bannercount"])))
                {
                    intBanners = Int32.Parse(Convert.ToString(Settings["bannercount"]));
                }
                switch (Convert.ToString(Settings["bannerdisplay"]))
                {
                    case "L": //local
                        //this.phBanner.Controls.Add(new LiteralControl(LegacyBannerString().ToString()));
                        LegacyBannerSettings();
                        break;
                    case "T":
                        TemplateBanner();
                        break;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        private string LegacyBannerString()
        {/*< asp:DataList id = lstBanners runat = "server" summary = "Banner Design Table" >
     
         < ItemTemplate >< asp:Label ID = "lblItem" Runat = "server" Text = '<%# FormatItem((int)DataBinder.Eval(Container.DataItem,"VendorId"), (int)DataBinder.Eval(Container.DataItem,"BannerId"), (int)DataBinder.Eval(Container.DataItem,"BannerTypeId"),(string)DataBinder.Eval(Container.DataItem,"BannerName"),(string)DataBinder.Eval(Container.DataItem,"ImageFileUrl"),(string)DataBinder.Eval(Container.DataItem,"Description"), (string)DataBinder.Eval(Container.DataItem,"Url"), (int)DataBinder.Eval(Container.DataItem,"Width"), (int)DataBinder.Eval(Container.DataItem,"Height")) %>' ></ asp:Label ></ ItemTemplate >
                </ asp:DataList >*/
            StringBuilder strHtlm = new StringBuilder(2000);
            strHtlm.Append("<asp:DataList id=lstBanners runat='server' summary='Banner Design Table' >");
            strHtlm.Append("< ItemTemplate >< asp:Label ID = 'lblItem' Runat = 'server' ");

            strHtlm.Append("Text = '<%# FormatItem((int)DataBinder.Eval(Container.DataItem,'VendorId'),");
            strHtlm.Append("(int)DataBinder.Eval(Container.DataItem,'BannerId'), ");
            strHtlm.Append("(int)DataBinder.Eval(Container.DataItem,'BannerTypeId'),");
            strHtlm.Append("(string)DataBinder.Eval(Container.DataItem,'BannerName'),   ");
            strHtlm.Append("(string)DataBinder.Eval(Container.DataItem,'ImageFileUrl'),  ");
            strHtlm.Append("(string)DataBinder.Eval(Container.DataItem,'Description'),   ");
            strHtlm.Append("(string)DataBinder.Eval(Container.DataItem,'Url'), ");
            strHtlm.Append("(int)DataBinder.Eval(Container.DataItem,'Width'), ");
            strHtlm.Append("(int)DataBinder.Eval(Container.DataItem,'Height')) %>' >");
            strHtlm.Append("</ asp:Label ></ ItemTemplate >");
            strHtlm.Append("</ asp:DataList > ");
            return strHtlm.ToString();
        }
        private void LegacyBannerSettings()
        {
            DataList lstBanners = (DataList)FindControl("lstBanners");
            if (!String.IsNullOrEmpty(Convert.ToString(Settings["padding"])))
            {
                lstBanners.CellPadding = Int32.Parse(Convert.ToString(Settings["padding"]));
            }

            //load banners
            if (intBanners != 0)
            {
                var objBanners = new BannerController();
                lstBanners.DataSource = objBanners.LoadBanners(intPortalId, ModuleId, intBannerTypeId, strBannerGroup, intBanners);
                lstBanners.DataBind();
            }

            //set banner display characteristics
            if (lstBanners.Items.Count != 0)
            {
                //container attributes
                lstBanners.RepeatLayout = RepeatLayout.Table;
                if (!String.IsNullOrEmpty(Convert.ToString(Settings["orientation"])))
                {
                    switch (Convert.ToString(Settings["orientation"]))
                    {
                        case "H":
                            lstBanners.RepeatDirection = RepeatDirection.Horizontal;
                            break;
                        case "V":
                            lstBanners.RepeatDirection = RepeatDirection.Vertical;
                            break;
                    }
                }
                else
                {
                    lstBanners.RepeatDirection = RepeatDirection.Vertical;
                }
                if (!String.IsNullOrEmpty(Convert.ToString(Settings["border"])))
                {
                    lstBanners.ItemStyle.BorderWidth = Unit.Parse(Convert.ToString(Settings["border"]) + "px");
                }
                if (!String.IsNullOrEmpty(Convert.ToString(Settings["bordercolor"])))
                {
                    var objColorConverter = new ColorConverter();
                    lstBanners.ItemStyle.BorderColor = (Color)objColorConverter.ConvertFrom(Convert.ToString(Settings["bordercolor"]));
                }

                //item attributes
                if (!String.IsNullOrEmpty(Convert.ToString(Settings["rowheight"])))
                {
                    lstBanners.ItemStyle.Height = Unit.Parse(Convert.ToString(Settings["rowheight"]) + "px");
                }
                if (!String.IsNullOrEmpty(Convert.ToString(Settings["colwidth"])))
                {
                    lstBanners.ItemStyle.Width = Unit.Parse(Convert.ToString(Settings["colwidth"]) + "px");
                }
            }
            else
            {
                lstBanners.Visible = false;
            }
        }

        private void TemplateBanner()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Settings["bannertype"])))
            {
                intBannerTypeId = Int32.Parse(Convert.ToString(Settings["bannertype"]));
            }
            strBannerGroup = Convert.ToString(Settings["bannergroup"]);
            var objBanners = new BannerController();
            ArrayList arrBanners = objBanners.LoadBanners(intPortalId, ModuleId, intBannerTypeId, strBannerGroup, intBanners);

            string strHeader, strContent, strFooter;
            if (!String.IsNullOrEmpty(Convert.ToString(Settings["bannerheader"])))
            {
                strHeader = Convert.ToString(Settings["bannerheader"]);
            }
            else
            {
                strHeader = "<div>";
            }
            if (!String.IsNullOrEmpty(Convert.ToString(Settings["bannercontent"])))
            {
                strContent = Convert.ToString(Settings["bannercontent"]);
            }
            else
            {
                strContent = "<a href=[LINKIMAGE] target=_self rel=nofollow><img src = [URLIMAGE] alt = [ALTERNATE] /></a > ";
            }
            if (!String.IsNullOrEmpty(Convert.ToString(Settings["bannerfooter"])))
            {
                strFooter = Convert.ToString(Settings["bannerfooter"]);
            }
            else
            {
                strFooter = "</div>";
            }
            StringBuilder strHtml = new StringBuilder(2000);
            strHtml.Append(strHeader.Trim());

            foreach (object objbanner in arrBanners)
            {
                BannerInfo banner = (BannerInfo)objbanner;
                string linkImage = objBanners.FormatURL(banner.VendorId, banner.BannerId, banner.URL, Convert.ToString(Settings["bannerclickthroughurl"]));
                string strContent2 = strContent;
                strContent2 = strContent2.Replace("[LINKIMAGE]", linkImage);
                strContent2 = strContent2.Replace("[URLIMAGE]", PortalSettings.HomeDirectory + banner.ImageFile);
                strHtml.Append(strContent2.Trim());

            }



            strHtml.Append(strFooter.Trim());

            phBanner.Controls.Add(new LiteralControl(Convert.ToString(strHtml)));
        }
        public string FormatItem(int VendorId, int BannerId, int BannerTypeId, string BannerName, string ImageFile, string Description, string URL, int Width, int Height)
        {
            var objBanners = new BannerController();
            return objBanners.FormatBanner(VendorId,
                                           BannerId,
                                           BannerTypeId,
                                           BannerName,
                                           ImageFile,
                                           Description,
                                           URL,
                                           Width,
                                           Height,
                                           Convert.ToString(Settings["bannersource"]),
                                           PortalSettings.HomeDirectory,
                                           Convert.ToString(Settings["bannerclickthroughurl"]));
        }
    }
}