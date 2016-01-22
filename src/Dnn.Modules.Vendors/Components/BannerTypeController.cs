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

using System.Collections;
using DotNetNuke.Services.Localization;

#endregion

namespace Dnn.Modules.Vendors.Components
{
    public class BannerTypeController
    {
        public ArrayList GetBannerTypes()
        {
            var arrBannerTypes = new ArrayList();
            arrBannerTypes.Add(new BannerTypeInfo((int) BannerType.Banner, Localization.GetString("BannerType.Banner.String", Localization.GlobalResourceFile)));
            arrBannerTypes.Add(new BannerTypeInfo((int) BannerType.MicroButton, Localization.GetString("BannerType.MicroButton.String", Localization.GlobalResourceFile)));
            arrBannerTypes.Add(new BannerTypeInfo((int) BannerType.Button, Localization.GetString("BannerType.Button.String", Localization.GlobalResourceFile)));
            arrBannerTypes.Add(new BannerTypeInfo((int) BannerType.Block, Localization.GetString("BannerType.Block.String", Localization.GlobalResourceFile)));
            arrBannerTypes.Add(new BannerTypeInfo((int) BannerType.Skyscraper, Localization.GetString("BannerType.Skyscraper.String", Localization.GlobalResourceFile)));
            arrBannerTypes.Add(new BannerTypeInfo((int) BannerType.Text, Localization.GetString("BannerType.Text.String", Localization.GlobalResourceFile)));
            arrBannerTypes.Add(new BannerTypeInfo((int) BannerType.Script, Localization.GetString("BannerType.Script.String", Localization.GlobalResourceFile)));
            return arrBannerTypes;
        }
    }
}