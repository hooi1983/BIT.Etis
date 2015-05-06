Public Class BindingHelper
    Public Shared Function BindDate(Value As Date)
        Try
            Return Value.ToString("yyyy-MM-dd")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function BindAmount(Value As Decimal)
        Try
            Return Value.ToString("n")
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class