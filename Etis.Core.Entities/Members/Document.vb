Namespace Members
    Public Class Document
        Inherits Common

        Property MemberDocumentID As Integer
        Property MemberID As Integer
        Property MemberPartnerID As Integer
        Property CurrencyID As Integer
        Property TransactionTypeID As Integer
        Property TransactionTypeSubID As Integer
        Property DocumentType As String
        Property DocumentNo As String
        Property DocumentDate As Date = Now
        Property DueDate As Date = Now
        Property PaymentDate As Date = Now
        Property Ppn As Decimal
        Property GrandTotalAmount As Decimal
        Property VatTotalAmount As Decimal
        Property TotalAmount As Decimal

        Property ItemList As IEnumerable(Of DocumentItem)
    End Class
End Namespace