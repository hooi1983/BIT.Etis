Public Interface IMemberDocument
    Inherits IDisposable

    ' Sales
    Function GetSalesRecords(MemberID As Integer, Optional CurrentPage As Integer = 1, Optional Search As String = "") As Entities.Members.DocumentVM
    Function GetSalesList(MemberID As Integer) As IEnumerable(Of Entities.Members.Document)
    Function GetSalesRecord(MemberID As Integer, id As Integer) As Entities.Members.Document
    Function AddSalesRecord(p As Entities.Members.Document) As Entities.Status
    Function EditSalesRecord(p As Entities.Members.Document) As Entities.Status
    Function DeleteSalesRecord(MemberID As Integer, id As Integer) As Entities.Status

    ' Purchase
    Function GetPurchaseRecords(MemberID As Integer, Optional CurrentPage As Integer = 1, Optional Search As String = "") As Entities.Members.DocumentVM
    Function GetPurchaseList(MemberID As Integer) As IEnumerable(Of Entities.Members.Document)
    Function GetPurchaseRecord(MemberID As Integer, id As Integer) As Entities.Members.Document
    Function AddPurchaseRecord(p As Entities.Members.Document) As Entities.Status
    Function EditPurchaseRecord(p As Entities.Members.Document) As Entities.Status
    Function DeletePurchaseRecord(MemberID As Integer, id As Integer) As Entities.Status
End Interface