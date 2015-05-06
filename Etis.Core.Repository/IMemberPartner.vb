Public Interface IMemberPartner
    Inherits IDisposable

    ' Seller
    Function GetSellerRecords(MemberID As Integer) As Entities.Members.PartnerVM
    Function GetSellerList(MemberID As Integer) As IEnumerable(Of Entities.Members.Partner)
    Function GetSellerRecord(MemberID As Integer, id As Integer) As Entities.Members.Partner
    Function AddSellerRecord(p As Entities.Members.Partner) As Entities.Status
    Function EditSellerRecord(p As Entities.Members.Partner) As Entities.Status
    Function DeleteSellerRecord(MemberID As Integer, id As Integer) As Entities.Status
    Function IsSellerCodeExist(MemberID As Integer, id As Integer, value As String) As Boolean
    Function IsSellerNameExist(MemberID As Integer, id As Integer, value As String) As Boolean

    ' Buyer
    Function GetBuyerRecords(MemberID As Integer) As Entities.Members.PartnerVM
    Function GetBuyerList(MemberID As Integer) As IEnumerable(Of Entities.Members.Partner)
    Function GetBuyerRecord(MemberID As Integer, id As Integer) As Entities.Members.Partner
    Function AddBuyerRecord(p As Entities.Members.Partner) As Entities.Status
    Function EditBuyerRecord(p As Entities.Members.Partner) As Entities.Status
    Function DeleteBuyerRecord(MemberID As Integer, id As Integer) As Entities.Status
    Function IsBuyerCodeExist(MemberID As Integer, id As Integer, value As String) As Boolean
    Function IsBuyerNameExist(MemberID As Integer, id As Integer, value As String) As Boolean
End Interface