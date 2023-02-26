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
using System.Collections;

using DotNetNuke.Common.Utilities;
using Dnn.Modules.Vendors.Data;

#endregion

namespace Dnn.Modules.Vendors.Components
{
    public class AffiliateController
    {
        private IDataService _dataService = new DataService();

        public void AddAffiliate(AffiliateInfo affiliate)
        {
            _dataService.AddAffiliate(affiliate.VendorId, affiliate.StartDate, affiliate.EndDate, affiliate.CPC, affiliate.CPA);
        }

        public void DeleteAffiliate(int affiliateId)
        {
            _dataService.DeleteAffiliate(affiliateId);
        }

        public ArrayList GetAffiliates(int vendorId)
        {
            return CBO.FillCollection(_dataService.GetAffiliates(vendorId), typeof(AffiliateInfo));
        }

        public AffiliateInfo GetAffiliate(int affiliateId)
        {
            return CBO.FillObject<AffiliateInfo>(_dataService.GetAffiliate(affiliateId));
        }

        public void UpdateAffiliate(AffiliateInfo affiliate)
        {
            _dataService.UpdateAffiliate(affiliate.AffiliateId, affiliate.StartDate, affiliate.EndDate, affiliate.CPC, affiliate.CPA);
        }

        public void UpdateAffiliateStats(int affiliateId, int clicks, int acquisitions)
        {
            _dataService.UpdateAffiliateStats(affiliateId, clicks, acquisitions);
        }

        [Obsolete("Deprecated in Version 7.3.0.  Replaced by single parameter overload as affiliateId is unique")]
        public AffiliateInfo GetAffiliate(int affiliateId, int vendorId, int portalId)
        {
            return GetAffiliate(affiliateId);
        }
    }
}