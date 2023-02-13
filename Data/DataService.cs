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
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Entities.Content.Data;

#endregion

namespace Dnn.Modules.Vendors.Data
{
    /// <summary>
	/// Persistent data of content with DataProvider instance.
	/// </summary>
    public class DataService : IDataService
    {
        private readonly DataProvider _provider = DataProvider.Instance();

        #region Affiliates

        public virtual int AddAffiliate(int vendorId, DateTime startDate, DateTime endDate, double CPC, double CPA)
        {
            return _provider.ExecuteScalar<int>("AddAffiliate", vendorId, _provider.GetNull(startDate), _provider.GetNull(endDate), CPC, CPA);
        }

        public virtual void DeleteAffiliate(int affiliateId)
        {
            _provider.ExecuteNonQuery("DeleteAffiliate", affiliateId);
        }

        public virtual IDataReader GetAffiliate(int affiliateId)
        {
            return _provider.ExecuteReader("GetAffiliate", affiliateId);
        }

        public virtual IDataReader GetAffiliates(int vendorId)
        {
            return _provider.ExecuteReader("GetAffiliates", vendorId);
        }

        public virtual void UpdateAffiliate(int affiliateId, DateTime startDate, DateTime endDate, double CPC, double CPA)
        {
            _provider.ExecuteNonQuery("UpdateAffiliate", affiliateId, _provider.GetNull(startDate), _provider.GetNull(endDate), CPC, CPA);
        }

        public virtual void UpdateAffiliateStats(int affiliateId, int clicks, int acquisitions)
        {
            _provider.ExecuteNonQuery("UpdateAffiliateStats", affiliateId, clicks, acquisitions);
        }
        
        public virtual IDataReader GetVendorClassifications(int VendorId)
        {
            return _provider.ExecuteReader("GetVendorClassifications", _provider.GetNull(VendorId));
        }
        
        public virtual void DeleteVendorClassifications(int VendorId)
        {
            _provider.ExecuteNonQuery("DeleteVendorClassifications", VendorId);
        }
        
        public virtual int AddVendorClassification(int VendorId, int ClassificationId)
        {
            return _provider.ExecuteScalar<int>("AddVendorClassification", VendorId, ClassificationId);
        }

        #endregion

        #region Vendors

        public virtual int AddVendor(int PortalId, string VendorName, string Unit, string Street, string City,
                                     string Region, string Country, string PostalCode, string Telephone, string Fax,
                                     string Cell, string Email, string Website, string FirstName, string LastName,
                                     string UserName, string LogoFile, string KeyWords, string Authorized)
        {
            return _provider.ExecuteScalar<int>("AddVendor",
                                            _provider.GetNull(PortalId),
                                            VendorName,
                                            Unit,
                                            Street,
                                            City,
                                            Region,
                                            Country,
                                            PostalCode,
                                            Telephone,
                                            Fax,
                                            Cell,
                                            Email,
                                            Website,
                                            FirstName,
                                            LastName,
                                            UserName,
                                            LogoFile,
                                            KeyWords,
                                            bool.Parse(Authorized));
        }

        public virtual void DeleteVendor(int VendorId)
        {
            _provider.ExecuteNonQuery("DeleteVendor", VendorId);
        }

        public virtual IDataReader GetVendor(int VendorId, int PortalId)
        {
            return _provider.ExecuteReader("GetVendor", VendorId, _provider.GetNull(PortalId));
        }

        public virtual IDataReader GetVendors(int PortalId, bool UnAuthorized, int PageIndex, int PageSize)
        {
            return _provider.ExecuteReader("GetVendors", _provider.GetNull(PortalId), UnAuthorized, _provider.GetNull(PageSize), _provider.GetNull(PageIndex));
        }

        public virtual IDataReader GetVendorsByEmail(string Filter, int PortalId, int PageIndex, int PageSize)
        {
            return _provider.ExecuteReader("GetVendorsByEmail", Filter, _provider.GetNull(PortalId), _provider.GetNull(PageSize), _provider.GetNull(PageIndex));
        }

        public virtual IDataReader GetVendorsByName(string Filter, int PortalId, int PageIndex, int PageSize)
        {
            return _provider.ExecuteReader("GetVendorsByName", Filter, _provider.GetNull(PortalId), _provider.GetNull(PageSize), _provider.GetNull(PageIndex));
        }

        public virtual void UpdateVendor(int VendorId, string VendorName, string Unit, string Street, string City,
                                         string Region, string Country, string PostalCode, string Telephone, string Fax,
                                         string Cell, string Email, string Website, string FirstName, string LastName,
                                         string UserName, string LogoFile, string KeyWords, string Authorized)
        {
            _provider.ExecuteNonQuery("UpdateVendor",
                                      VendorId,
                                      VendorName,
                                      Unit,
                                      Street,
                                      City,
                                      Region,
                                      Country,
                                      PostalCode,
                                      Telephone,
                                      Fax,
                                      Cell,
                                      Email,
                                      Website,
                                      FirstName,
                                      LastName,
                                      UserName,
                                      LogoFile,
                                      KeyWords,
                                      bool.Parse(Authorized));
        }

        #endregion

        #region Banners

        public virtual int AddBanner(string BannerName, int VendorId, string ImageFile, string URL, int Impressions,
                                     double CPM, DateTime StartDate, DateTime EndDate, string UserName,
                                     int BannerTypeId, string Description, string GroupName, int Criteria, int Width,
                                     int Height)
        {
            return _provider.ExecuteScalar<int>("AddBanner",
                                            BannerName,
                                            VendorId,
                                            _provider.GetNull(ImageFile),
                                            _provider.GetNull(URL),
                                            Impressions,
                                            CPM,
                                            _provider.GetNull(StartDate),
                                            _provider.GetNull(EndDate),
                                            UserName,
                                            BannerTypeId,
                                            _provider.GetNull(Description),
                                            _provider.GetNull(GroupName),
                                            Criteria,
                                            Width,
                                            Height);
        }

        public virtual void DeleteBanner(int BannerId)
        {
            _provider.ExecuteNonQuery("DeleteBanner", BannerId);
        }

        public virtual IDataReader FindBanners(int PortalId, int BannerTypeId, string GroupName)
        {
            return _provider.ExecuteReader("FindBanners", _provider.GetNull(PortalId), _provider.GetNull(BannerTypeId), _provider.GetNull(GroupName));
        }

        public virtual IDataReader GetBanner(int BannerId)
        {
            return _provider.ExecuteReader("GetBanner", BannerId);
        }

        public virtual DataTable GetBannerGroups(int PortalId)
        {
            return Globals.ConvertDataReaderToDataTable(_provider.ExecuteReader("GetBannerGroups", _provider.GetNull(PortalId)));
        }

        public virtual IDataReader GetBanners(int VendorId)
        {
            return _provider.ExecuteReader("GetBanners", VendorId);
        }

        public virtual void UpdateBanner(int BannerId, string BannerName, string ImageFile, string URL, int Impressions,
                                         double CPM, DateTime StartDate, DateTime EndDate, string UserName,
                                         int BannerTypeId, string Description, string GroupName, int Criteria, int Width,
                                         int Height)
        {
            _provider.ExecuteNonQuery("UpdateBanner",
                                      BannerId,
                                      BannerName,
                                      _provider.GetNull(ImageFile),
                                      _provider.GetNull(URL),
                                      Impressions,
                                      CPM,
                                      _provider.GetNull(StartDate),
                                      _provider.GetNull(EndDate),
                                      UserName,
                                      BannerTypeId,
                                      _provider.GetNull(Description),
                                      _provider.GetNull(GroupName),
                                      Criteria,
                                      Width,
                                      Height);
        }

        public virtual void UpdateBannerClickThrough(int BannerId, int VendorId)
        {
            _provider.ExecuteNonQuery("UpdateBannerClickThrough", BannerId, VendorId);
        }

        public virtual void UpdateBannerViews(int BannerId, DateTime StartDate, DateTime EndDate)
        {
            _provider.ExecuteNonQuery("UpdateBannerViews", BannerId, _provider.GetNull(StartDate), _provider.GetNull(EndDate));
        }

        #endregion
    }
}