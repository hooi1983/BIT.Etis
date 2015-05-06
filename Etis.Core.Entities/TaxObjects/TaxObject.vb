Namespace TaxObjects
    Public Class TaxObject
        Inherits Common

        ' ID
        Property TaxObjectID As Integer

        ' Lookup Key
        Property TaxItemType As String
        Property TaxType As String

        ' Field Value
        Property TaxObjectCode As String
        Property TaxObjectName As String
        Property TaxPercent As Decimal
    End Class
End Namespace