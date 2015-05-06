Public Class Member
    Implements IMember

    Public Function GetRecords(pg As Integer) As Members.MemberVM Implements IMember.GetRecords
        Try
            Using dat As New Data.Member
                Return dat.GetRecords(pg)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetRecord(id As Integer) As Members.Member Implements IMember.GetRecord
        Try
            Using dat As New Data.Member
                Return dat.GetRecord(id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddRecord(p As Members.Member) As Status Implements IMember.AddRecord
        Try
            If String.IsNullOrWhiteSpace(p.LoginPassword) Then Return New Status With {.Code = "0", .Message = "Error. Password is required."}
            If String.IsNullOrWhiteSpace(p.FullName) Then Return New Status With {.Code = "0", .Message = "Error. Full Name is required."}
            If String.IsNullOrWhiteSpace(p.Email) Then Return New Status With {.Code = "0", .Message = "Error. Email is required."}

            If Not String.Compare(p.LoginPassword, p.ConfirmPassword) = 0 Then Return New Status("0", "Password not match.")

            Using dat As New Data.Member
                If dat.IsEmailExist(p.MemberID, p.Email) Then Return New Status With {.Code = "0", .Message = "Error. Email is duplicated."}

                Return dat.AddRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditRecord(p As Members.Member) As Status Implements IMember.EditRecord
        Try
            If String.IsNullOrWhiteSpace(p.FullName) Then Return New Status With {.Code = "0", .Message = "Error. Full Name is required."}
            If String.IsNullOrWhiteSpace(p.Email) Then Return New Status With {.Code = "0", .Message = "Error. Email is required."}

            Using dat As New Data.Member
                If dat.IsEmailExist(p.MemberID, p.Email) Then Return New Status With {.Code = "0", .Message = "Error. Email is duplicated."}

                Return dat.EditRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteRecord(id As Integer) As Status Implements IMember.DeleteRecord
        Try
            Using dat As New Data.Member
                Return dat.DeleteRecord(id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ResetPassword(p As Entities.Members.MemberPassword) As Status Implements IMember.ResetPassword
        Try
            If Not String.Compare(p.NewPassword, p.ConfirmPassword) = 0 Then Return New Status With {.Code = "0", .Message = "New Password not match."}
            If Not Me.IsValidPassword(p) Then Return New Status With {.Code = "0", .Message = "Old Password invalid."}

            Using dat As New Data.Member
                Return dat.ResetPassword(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ResetMemberPassword(p As Entities.Members.MemberPassword) As Status Implements IMember.ResetMemberPassword
        Try
            If Not String.Compare(p.NewPassword, p.ConfirmPassword) = 0 Then Return New Status With {.Code = "0", .Message = "New Password not match."}

            Using dat As New Data.Member
                Return dat.ResetPassword(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function Login(p As Members.MemberLogin) As Members.Member Implements IMember.Login
        Try
            Using dat As New Data.Member
                Return dat.Login(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsLoginNameExist(id As Integer, value As String) As Boolean Implements IMember.IsLoginNameExist
        Try
            Using dat As New Data.Member
                Return dat.IsLoginNameExist(id, value)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsEmailExist(id As Integer, value As String) As Boolean Implements IMember.IsEmailExist
        Try
            Using dat As New Data.Member
                Return dat.IsEmailExist(id, value)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsValidLogin(p As Members.MemberLogin) As Boolean Implements IMember.IsValidLogin
        Try
            If String.IsNullOrWhiteSpace(p.LoginName) Then Return False
            If String.IsNullOrWhiteSpace(p.LoginPassword) Then Return False

            Using dat As New Data.Member
                Return dat.IsValidLogin(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsValidPassword(p As Members.MemberPassword) As Boolean Implements IMember.IsValidPassword
        Try
            Using Data As New Data.Member
                Return Data.IsValidPassword(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class