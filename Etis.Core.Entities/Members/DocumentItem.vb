Namespace Members
    Public Class DocumentItem
        Inherits Common

        ' ID
        Property MemberDocumentItemID As Integer
        Property MemberDocumentID As Integer
        Property TaxObjectID As Integer
        Property Guid As Guid

        ' Lookup Key
        Property TaxItemType As String
        Property TaxType As String

        ' Field Value
        Property Quantity As Integer
        Property UnitPrice As Decimal
        Property TaxPercent As Decimal
        Property SubGrandTotalAmount As Decimal
        Property Description As String

        ' Lookup Value

    End Class
End Namespace