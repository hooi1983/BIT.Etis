module db_scheme
{
    export interface HasLog
    {
        CreatedBy:          number
        CreatedOn:          Date
        CreatedOnUtc:       Date
        CreatedAgent:       string
        CreatedIp:          string
        ModifiedBy:         number
        ModifiedOn:         Date
        ModifiedOnUtc:      Date
        ModifiedAgent:      string
        ModifiedIp:         string
    }

    export interface tblCompany extends HasLog
    {
        CompanyID: number
        OfficeID: number
        CompanyCode: string
        CompanyName: string
        Address1: string
        Address2: string
        Address3: string
        Postcode: string
        City: string
        State: string
        Country: string
        ContactNunber1: string
        ContactNumber2: string
        FaxNumber1: string
        FaxNumber2: string
        Email: string
        Industry: string
        Status: number
    }

    export interface tblCompanyUser extends HasLog
    {
        CompanyUserID: number
        CompanyID: number
        UserGroupID: number
        LoginName: string
        LoginPassword: string
        FullName: string
        IcNo: string
        ContactNumber1: string
        ContactNumber2: string
        Email: string
        Status: number
    }

    export interface tblCurrency extends HasLog
    {
        CurrencyID: number
        IsoCode: string
        CurrencyCode: string
        CurrencyName: string
        Symbol: string
        FractionUnit: string
        NumberToBasic: number
        ListOrder: number
        IsDefault: boolean
        Status: number
    }

    export interface tblDocument extends HasLog
    {
        DocumentID: number
        CompanyID: number
        BillToCompanyID: number
        ShipToCompanyID: number
        CurrencyID: number
        TransactionTypeID: number
        TransactionTypeSubID: number
        DocumentType: string
        DocumentDate: Date
        DueDate: Date
        PaymentDate: Date
        Ppn: number
        GrandTotalAmount: number
        VatTotalAmount: number
        TotalAmount: number
    }

    export interface tblDocumentItem extends HasLog
    {
        DocumentItemID: number
        DocumentID: number
        TaxObjectID: number
        TaxType: string
        Quantity: number
        UnitPrice: number
        TaxPercent: number
        TaxFixedAmount: number
        IsFixedAmount: boolean
        SubGrandTotalAmount: number
        SubVatTotalAmount: number
        SubTotalAmount: number
        Description: string
    }

    export interface tblLookup
    {
        LookupID: number
        TableName: string
        FieldName: string
        LookupKey: string
        LookupValue: string
        Description: string
        ListOrder: number
        Status: number
    }

    export interface tblModule
    {
        ModuleID: number
        ModuleName: string
        AreaName: string
        ControllerName: string
        ActionName: string
        CustomUrl: string
        Status: number
    }

    export interface tblOfficer extends HasLog
    {
        OfficerID: number
        SupervisorID: number
        LoginName: string
        LoginPassword: string
        FullName: string
        Title: string
        District: string
        IcNo: string
        ContactNumber1: string
        ContactNumber2: string
        Email: string
        Status: number
    }

    export interface tblTaxObject extends HasLog
    {
        TaxObjectID: number
        TaxItemType: string
        TaxType: string
        TaxObjectCode: string
        TaxObjectName: string
        TaxPercent: number
        TaxFixedAmount: number
        IsFixedAmount: boolean
        ListOrder: number
        Status: number
    }

    export interface tblTransaction extends HasLog
    {
        TransactionID: number
        DebitCompanyID: number
        CreditCompanyID: number
        DocumentID: number
        DocumentItemID: number
        TransactionTypeID: number
        DocumentType: string
        TransactionDate: Date
        PostingDate: Date
        DebitAmount: number
        CreditAmount: number
    }

    export interface tblTransactionType extends HasLog
    {
        TransactionTypeID: number
        ParentID: number
        TransactionTypeCode: string
        TransactionTypeName: string
        ListOrder: number
        IsDefault: boolean
        Status: number
    }

    export interface tblUserGroup extends HasLog
    {
        UserGroupID: number
        UserGroupType: string
        UserGroupCode: string
        UserGroupName: string
        Status: number
    }

    export interface tblUserGroupModule extends HasLog
    {
        UserGroupModuleID: number
        UserGroupID: number
        ModuleID: number
        CanView: boolean
        CanCreate: boolean
        CanUpdate: boolean
        CanDelete: boolean
    }
}