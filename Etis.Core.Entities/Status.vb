Public Class Status
    Sub New()

    End Sub

    Sub New(Code As String)
        Me.Code = Code
    End Sub

    Sub New(Code As String, Message As String)
        Me.Code = Code
        Me.Message = Message
    End Sub

    Property Code As String
    Property Message As String
    Property Value As String
    Property CPoint As Decimal
    Property RPoint As Decimal
    Property SPoint As Decimal
    Property EPoint As Decimal
    Property MPoint As Decimal
End Class