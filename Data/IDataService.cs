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
using System.Data;

#endregion

namespace Dnn.Modules.Vendors.Data
{
    /// <summary>
    /// Interface of DataService.
    /// </summary>
    /// <seealso cref="Dnn.Modules.Vendors.Data.DataService"/>
    public interface IDataService
    {
        int AddAffiliate(int vendorId, DateTime startDate, DateTime endDate, double CPC, double CPA);

        void DeleteAffiliate(int affiliateId);

        IDataReader GetAffiliate(int affiliateId);

        IDataReader GetAffiliates(int vendorId);

        void UpdateAffiliate(int affiliateId, DateTime startDate, DateTime endDate, double CPC, double CPA);

        void UpdateAffiliateStats(int affiliateId, int clicks, int acquisitions);

        IDataReader GetVendorClassifications(int VendorId);

        void DeleteVendorClassifications(int VendorId);

        int AddVendorClassification(int VendorId, int ClassificationId);

        int AddVendor(int PortalId, string VendorName, string Unit, string Street, string City,
            string Region, string Country, string PostalCode, string Telephone, string Fax,
            string Cell, string Email, string Website, string FirstName, string LastName,
            string UserName, string LogoFile, string KeyWords, string Authorized);

        void DeleteVendor(int VendorId);

        IDataReader GetVendor(int VendorId, int PortalId);

        IDataReader GetVendors(int PortalId, bool UnAuthorized, int PageIndex, int PageSize);

        IDataReader GetVendorsByEmail(string Filter, int PortalId, int PageIndex, int PageSize);

        IDataReader GetVendorsByName(string Filter, int PortalId, int PageIndex, int PageSize);

        void UpdateVendor(int VendorId, string VendorName, string Unit, string Street, string City,
            string Region, string Country, string PostalCode, string Telephone, string Fax,
            string Cell, string Email, string Website, string FirstName, string LastName,
            string UserName, string LogoFile, string KeyWords, string Authorized);

        int AddBanner(string BannerName, int VendorId, string ImageFile, string URL, int Impressions,
            double CPM, DateTime StartDate, DateTime EndDate, string UserName,
            int BannerTypeId, string Description, string GroupName, int Criteria, int Width,
            int Height);

        void DeleteBanner(int BannerId);

        IDataReader FindBanners(int PortalId, int BannerTypeId, string GroupName);

        IDataReader GetBanner(int BannerId);

        DataTable GetBannerGroups(int PortalId);

        IDataReader GetBanners(int VendorId);

        void UpdateBanner(int BannerId, string BannerName, string ImageFile, string URL, int Impressions,
            double CPM, DateTime StartDate, DateTime EndDate, string UserName,
            int BannerTypeId, string Description, string GroupName, int Criteria, int Width,
            int Height);

        void UpdateBannerClickThrough(int BannerId, int VendorId);

        void UpdateBannerViews(int BannerId, DateTime StartDate, DateTime EndDate);
    }
}