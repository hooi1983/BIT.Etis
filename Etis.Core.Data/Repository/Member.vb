Public Class Member
    Implements IMember

    Public Function GetRecords(pg As Integer) As Members.MemberVM Implements IMember.GetRecords
        Dim p = New Members.MemberVM

        Try
            Using db As New DbContext
                p.ItemList = db.Database.SqlQuery(Of Members.Member)(
                    "select row_number() over (order by MemberID) as RowNum, " +
                    "MemberID, LoginName, FullName, Email, LastLoginOn, LastLoginOnUtc " +
                    "from tblMember"
                ).ToList
            End Using

            Return p
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetRecord(id As Integer) As Members.Member Implements IMember.GetRecord
        Dim Model = New Members.Member

        Try
            Using db As New DbContext
                Model = db.Database.SqlQuery(Of Members.Member)(
                    "select MemberID, LoginName, FullName, Email, LastLoginOn, LastLoginOnUtc " +
                    "from tblMember " +
                    "where MemberID = @MemberID",
                    New SqlParameter("@MemberID", id)
                ).SingleOrDefault
            End Using

            Return Model
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddRecord(p As Members.Member) As Status Implements IMember.AddRecord
        Try
            Return New Status With {.Code = "1"}
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditRecord(p As Members.Member) As Status Implements IMember.EditRecord
        Try
            Return New Status With {.Code = "1"}
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteRecord(id As Integer) As Status Implements IMember.DeleteRecord
        Try
            Return New Status With {.Code = "1"}
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ResetPassword(p As Members.MemberPassword) As Status Implements IMember.ResetPassword
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "update tblMember set LoginPassword = @NewPassword where MemberID = @MemberID",
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@NewPassword", p.NewPassword)
                )
            End Using

            Return New Status With {.Code = "1"}
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ResetMemberPassword(p As Members.MemberPassword) As Status Implements IMember.ResetMemberPassword
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "update tblMember set LoginPassword = @NewPassword where MemberID = @MemberID",
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@NewPassword", p.NewPassword)
                )
            End Using

            Return New Status With {.Code = "1"}
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function Login(p As Members.MemberLogin) As Members.Member Implements IMember.Login
        Dim Model = New Members.Member

        Try
            Using db As New DbContext
                Model = db.Database.SqlQuery(Of Members.Member)(
                    "declare @MemberID int;" +
                    "select @MemberID = MemberID from tblMember where LoginName = @LoginName and LoginPassword = @LoginPassword; " +
                    "update tblMember " +
                    "set LastLoginOn = dbo.getLocalTime(default), LastLoginOnUtc = getutcdate(), LastLoginAgent = @UserAgent, LastLoginIp = @IpAddress " +
                    "where MemberID = @MemberID; " +
                    "select MemberID, LoginName, FullName " +
                    "from tblMember " +
                    "where MemberID = @MemberID;",
                    New SqlParameter("@LoginName", p.LoginName),
                    New SqlParameter("@LoginPassword", p.LoginPassword),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                ).SingleOrDefault

                Return Model
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsLoginNameExist(id As Integer, value As String) As Boolean Implements IMember.IsLoginNameExist
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMember where LoginName = @LoginName and not MemberID = @MemberID",
                    New SqlParameter("@MemberID", id),
                    New SqlParameter("@LoginName", value)
                ).SingleOrDefault
            End Using

            Return If(Not i = 0, True, False)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsEmailExist(id As Integer, value As String) As Boolean Implements IMember.IsEmailExist
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMember where Email = @Email and not MemberID = @MemberID",
                    New SqlParameter("@MemberID", id),
                    New SqlParameter("@Email", value)
                ).SingleOrDefault
            End Using

            Return If(Not i = 0, True, False)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsValidLogin(p As Members.MemberLogin) As Boolean Implements IMember.IsValidLogin
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMember where LoginName = @LoginName and LoginPassword = @LoginPassword and Status = 1",
                    New SqlParameter("@LoginName", p.LoginName),
                    New SqlParameter("@LoginPassword", p.LoginPassword)
                ).SingleOrDefault
            End Using

            Return If(Not i = 0, True, False)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsValidPassword(p As Members.MemberPassword) As Boolean Implements IMember.IsValidPassword
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMember where MemberID = @MemberID and LoginPassword = @LoginPassword",
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@LoginPassword", p.LoginPassword)
                ).SingleOrDefault
            End Using

            Return If(Not i = 0, True, False)
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