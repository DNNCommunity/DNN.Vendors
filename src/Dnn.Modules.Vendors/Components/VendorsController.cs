#region Copyright
// 
// DotNetNuke® - http://www.dnnsoftware.com
// Copyright (c) 2002-2015
// by DNN Corp.
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

using System;
using System.Collections;
using System.Data;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Services.Upgrade;
using Dnn.Modules.Vendors.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;

namespace Dnn.Modules.Vendors.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class VendorsController : IUpgradeable
    {
        private IDataService _dataService = new DataService();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public string UpgradeModule(string version)
        {
            try
            {
                switch (version)
                {
                    case "08.00.00":
                        ModuleDefinitionInfo moduleDefinition = ModuleDefinitionController.GetModuleDefinitionByFriendlyName("Vendors");
                        if (moduleDefinition != null)
                        {
                            var hostPage = Upgrade.AddHostPage("Vendors",
                                                    "Manage vendor accounts, banner advertising and affiliate referrals within the portal.",
                                                     "~/Icons/Sigma/Vendors_16X16_Standard.png",
                                                    "~/Icons/Sigma/Vendors_32X32_Standard.png",
                                                    true);

                            //Add module to page
                            Upgrade.AddModuleToPage(hostPage, moduleDefinition.ModuleDefID, "Vendors", "~/Icons/Sigma/Vendors_32X32_Standard.png", true);

                            //Add Module to Admin Page for all Portals
                            Upgrade.AddAdminPages("Vendors",
                                                    "Manage vendor accounts, banner advertising and affiliate referrals within the portal.",
                                                    "~/Icons/Sigma/Vendors_16X16_Standard.png",
                                                    "~/Icons/Sigma/Vendors_32X32_Standard.png",
                                                    true,
                                                    moduleDefinition.ModuleDefID,
                                                    "Vendors",
                                                    "~/Icons/Sigma/Vendors_32X32_Standard.png",
                                                    true);
                        }
                        break;
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        public int AddVendor(VendorInfo objVendor)
        {
            return _dataService.AddVendor(objVendor.PortalId,
                                                     objVendor.VendorName,
                                                     objVendor.Unit,
                                                     objVendor.Street,
                                                     objVendor.City,
                                                     objVendor.Region,
                                                     objVendor.Country,
                                                     objVendor.PostalCode,
                                                     objVendor.Telephone,
                                                     objVendor.Fax,
                                                     objVendor.Cell,
                                                     objVendor.Email,
                                                     objVendor.Website,
                                                     objVendor.FirstName,
                                                     objVendor.LastName,
                                                     objVendor.UserName,
                                                     objVendor.LogoFile,
                                                     objVendor.KeyWords,
                                                     objVendor.Authorized.ToString());
        }

        public void DeleteVendor(int VendorID)
        {
            _dataService.DeleteVendor(VendorID);
            var objBanners = new BannerController();
            objBanners.ClearBannerCache();
        }

        public void DeleteVendors()
        {
            DeleteVendors(Null.NullInteger);
        }

        public void DeleteVendors(int PortalID)
        {
            int TotalRecords = 0;
            foreach (VendorInfo vendor in GetVendors(PortalID, true, Null.NullInteger, Null.NullInteger, ref TotalRecords))
            {
                if (vendor.Authorized == false)
                {
                    DeleteVendor(vendor.VendorId);
                }
            }
            var objBanners = new BannerController();
            objBanners.ClearBannerCache();
        }

        public void UpdateVendor(VendorInfo objVendor)
        {
            _dataService.UpdateVendor(objVendor.VendorId,
                                                 objVendor.VendorName,
                                                 objVendor.Unit,
                                                 objVendor.Street,
                                                 objVendor.City,
                                                 objVendor.Region,
                                                 objVendor.Country,
                                                 objVendor.PostalCode,
                                                 objVendor.Telephone,
                                                 objVendor.Fax,
                                                 objVendor.Cell,
                                                 objVendor.Email,
                                                 objVendor.Website,
                                                 objVendor.FirstName,
                                                 objVendor.LastName,
                                                 objVendor.UserName,
                                                 objVendor.LogoFile,
                                                 objVendor.KeyWords,
                                                 objVendor.Authorized.ToString());
        }

        public VendorInfo GetVendor(int VendorID, int PortalId)
        {
            return CBO.FillObject<VendorInfo>(_dataService.GetVendor(VendorID, PortalId));
        }

        public ArrayList GetVendors(int PortalId, string Filter)
        {
            int TotalRecords = 0;
            return GetVendorsByName(Filter, PortalId, 0, 100000, ref TotalRecords);
        }

        public ArrayList GetVendors(int PortalId, bool UnAuthorized, int PageIndex, int PageSize, ref int TotalRecords)
        {
            IDataReader dr = _dataService.GetVendors(PortalId, UnAuthorized, PageIndex, PageSize);
            ArrayList retValue = null;
            try
            {
                while (dr.Read())
                {
                    TotalRecords = Convert.ToInt32(dr["TotalRecords"]);
                }
                dr.NextResult();
                retValue = CBO.FillCollection(dr, typeof(VendorInfo));
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return retValue;
        }

        public ArrayList GetVendorsByEmail(string Filter, int PortalId, int Page, int PageSize, ref int TotalRecords)
        {
            IDataReader dr = _dataService.GetVendorsByEmail(Filter, PortalId, Page, PageSize);
            try
            {
                while (dr.Read())
                {
                    TotalRecords = Convert.ToInt32(dr["TotalRecords"]);
                }
                dr.NextResult();
                return CBO.FillCollection(dr, typeof(VendorInfo));
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
        }

        public ArrayList GetVendorsByName(string Filter, int PortalId, int Page, int PageSize, ref int TotalRecords)
        {
            IDataReader dr = _dataService.GetVendorsByName(Filter, PortalId, Page, PageSize);
            try
            {
                while (dr.Read())
                {
                    TotalRecords = Convert.ToInt32(dr["TotalRecords"]);
                }
                dr.NextResult();
                return CBO.FillCollection(dr, typeof(VendorInfo));
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
        }
    }
}
