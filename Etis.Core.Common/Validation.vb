Public Class Validation
    Public Shared Function ValDecimal(value As String) As Decimal
        Try
            If Not String.IsNullOrWhiteSpace(value) Then
                If Not IsNumeric(value) Then
                    Return 0
                End If
            Else
                Return 0
            End If

            Return CDec(value)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class