﻿namespace PIMS.Tests.Automation.Classes
{
    public class Property
    {
        public string PID { get; set; } = null!;
        public string PlanNumber { get; set; } = null!;
        public string PropertyName { get; set; } = null!;
        public Address Address { get; set; } = new Address();
        public string GeneralLocation { get; set; } = null!;
        public int HistoricalFileStartRow { get; set; } = 0;
        public int HistoricalFileCount { get; set; } = 0;
        public List<HistoricalFile> PropertyHistoricalFiles { get; set; } = new List<HistoricalFile>();
        public string LegalDescription { get; set; } = null!;
        public string MOTIRegion { get; set; } = null!;
        public string HighwaysDistrict { get; set; } = null!;
        public string ElectoralDistrict { get; set; } = null!;
        public string AgriculturalLandReserve { get; set; } = null!;
        public string RailwayBelt { get; set; } = null!;
        public string LandParcelType { get; set; } = null!;
        public string MunicipalZoning { get; set; } = null!;
        public List<string> Anomalies { get; set; } = new List<string>();
        public List<string> TenureStatus { get; set; } = new List<string>();
        public string ProvincialPublicHwy { get; set; } = null!;
        public List<string> HighwayEstablishedBy { get; set; } = new List<string>();
        public string SqrMeters { get; set; } = null!;
        public Boolean IsVolumetric { get; set; } = false;
        public string Volume { get; set; } = null!;
        public string VolumeType { get; set; } = null!;
        public string PropertyNotes { get; set; } = null!;
    }

    public class LeaseProperty
    {
        public string PID { get; set; } = null!;
        public string HistoricalFile { get; set; } = null!;
        public string DescriptiveName { get; set; } = null!;
        public string Area { get; set; } = null!;
        public Address Address { get; set; } = new Address();
        public string LegalDescription { get; set; } = null!;
    }

    public class Address
    {
        public string AddressLine1 { get; set; } = null!;
        public string AddressLine2 { get; set; } = null!;
        public string AddressLine3 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string ProvinceView { get; set; } = null!;
        public string CityProvinceView { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string OtherCountry { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
    }

    public class HistoricalFile
    {
        public string HistoricalFileNumber { get; set; } = null!;
        public string HistoricalFileType { get; set; } = null!;
        public string HistoricalFileOtherDetails { get; set; } = null!;
    }

    public class SearchProperty
    {
        public string PID { get; set; } = null!;
        public string PIN { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PlanNumber { get; set; } = null!;
        public string LegalDescription { get; set; } = null!;
        public List<string> MultiplePIDS { get; set; } = new List<string>();
        public PropertyLatitudeLongitude LatitudeLongitude { get; set; } = new PropertyLatitudeLongitude();
    }

    public class PropertyManagement
    {
        public List<string> ManagementPropertyPurpose { get; set; } = new List<string>();
        public string ManagementUtilitiesPayable { get; set; } = null!;
        public string ManagementTaxesPayable { get; set; } = null!;
        public string ManagementPropertyAdditionalDetails { get; set; } = null!;
        public int ManagementPropertyContactsStartRow { get; set; } = 0;
        public int ManagementPropertyContactsStartCount { get; set; } = 0;
        public List<PropertyContact> ManagementPropertyContacts { get; set; } = new List<PropertyContact>();
        public int ManagementPropertyActivitiesStartRow { get; set; } = 0;
        public int ManagementPropertyActivitiesCount { get; set; } = 0;
        public List<PropertyActivity> ManagementPropertyActivities { get; set; } = new List<PropertyActivity>();
    }

    public class PropertyContact
    {
        public string PropertyContactFullName { get; set; } = null!;
        public string PropertyContactType { get; set; } = null!;
        public string PropertyPrimaryContact { get; set; } = null!;
        public string PropertyContactPurposeDescription { get; set; } = null!;
    }

    public class PropertyActivity
    {
        public string PropertyActivityType { get; set; } = null!;
        public string PropertyActivitySubType { get; set; } = null!;
        public string PropertyActivityStatus { get; set; } = null!;
        public string PropertyActivityRequestedDate { get; set; } = null!;
        public string PropertyActivityCompletionDate { get; set; } = null!;
        public string PropertyActivityDescription { get; set; } = null!;
        public List<string> PropertyActivityMinistryContact { get; set; } = new List<string>();
        public string PropertyActivityRequestedSource { get; set; } = null!;
        public List<string> PropertyActivityInvolvedParties { get; set; } = new List<string>();
        public string PropertyActivityServiceProvider { get; set; } = null!;
        public int ManagementPropertyActivityInvoicesStartRow { get; set; } = 0;
        public int ManagementPropertyActivityInvoicesCount { get; set; } = 0;
        public List<ManagementPropertyActivityInvoice> ManagementPropertyActivityInvoices { get; set; } = new List<ManagementPropertyActivityInvoice>();
        public string ManagementPropertyActivityTotalPreTax { get; set; } = null!;
        public string ManagementPropertyActivityTotalGST { get; set; } = null!;
        public string ManagementPropertyActivityTotalPST { get; set; } = null!;
        public string ManagementPropertyActivityGrandTotal { get; set; } = null!;
    }

    public class ManagementPropertyActivityInvoice
    {
        public string PropertyActivityInvoiceNumber { get; set; } = null!;
        public string PropertyActivityInvoiceDate { get; set; } = null!;
        public string PropertyActivityInvoiceDescription { get; set; } = null!;
        public string PropertyActivityInvoicePretaxAmount { get; set; } = null!;
        public string PropertyActivityInvoiceGSTAmount { get; set; } = null!;
        public string PropertyActivityInvoicePSTApplicable { get; set; } = null!;
        public string PropertyActivityInvoicePSTAmount { get; set; } = null!;
        public string PropertyActivityInvoiceTotalAmount { get; set; } = null!;
    }

    public class PropertyHistory
    {
        public string PropertyHistoryIdentifier { get; set; } = null!;
        public string PropertyHistoryPlan { get; set; } = null!;
        public string PropertyHistoryStatus { get; set; } = null!;
        public string PropertyHistoryArea { get; set; } = null!;
    }

    public class PropertySubdivision
    {
        public PropertyHistory SubdivisionSource { get; set; } = new PropertyHistory();
        public List<PropertyHistory> SubdivisionDestination { get; set; } = new List<PropertyHistory>();
    }

    public class PropertyConsolidation
    {
        public List<PropertyHistory> ConsolidationSource { get; set; } = new List<PropertyHistory>();
        public PropertyHistory ConsolidationDestination { get; set; } = new PropertyHistory();
    }

    public class PropertyLatitudeLongitude
    {
        public string LatitudeDegree { get; set; } = null!;
        public string LatitudeMinutes { get; set; } = null!;
        public string LatitudeSeconds { get; set; } = null!;
        public string LatitudeDirection { get; set; } = null!;
        public string LongitudeDegree { get; set; } = null!;
        public string LongitudeMinutes { get; set; } = null!;
        public string LongitudeSeconds { get; set; } = null!;
        public string LongitudeDirection { get; set; } = null!;
    }
}
