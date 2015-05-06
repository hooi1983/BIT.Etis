Public Class GetValue
    Public Shared Function IdentityID(Identity As System.Security.Principal.IIdentity) As Integer
        Try
            If Identity IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(Identity.Name) Then
                If IsNumeric(Identity.Name.Split(CChar("|"))(0)) Then
                    Return CInt(Identity.Name.Split(CChar("|"))(0))
                End If
            End If

            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class