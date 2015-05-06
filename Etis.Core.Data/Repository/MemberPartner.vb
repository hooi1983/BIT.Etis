Public Class MemberPartner
    Implements IMemberPartner

    ReadOnly SellerType As String = "SL"
    ReadOnly BuyerType As String = "BY"

    ' Seller
    Public Function GetSellerRecords(MemberID As Integer) As Members.PartnerVM Implements IMemberPartner.GetSellerRecords
        Dim p = New Members.PartnerVM

        Try
            Using db As New DbContext
                p.ItemList = db.Database.SqlQuery(Of Members.Partner)(
                    "select row_number() over(order by MemberPartnerID) as RowNum, " +
                    "MemberPartnerID, MemberID, PartnerType, FullName, t1.LookupValue as PartnerTypeValue " +
                    "from tblMemberPartner t0 " +
                    "left join tblLookup t1 on t1.LookupKey = t0.PartnerType and t1.TableName = 'tblMemberPartner' and t1.FieldName = 'PartnerType' " +
                    "where t0.MemberID = @MemberID and t0.PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@PartnerType", Me.SellerType)
                ).ToList
            End Using

            Return p
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSellerList(MemberID As Integer) As IEnumerable(Of Members.Partner) Implements IMemberPartner.GetSellerList
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of Members.Partner)(
                    "select MemberPartnerID, MemberID, PartnerType, FullName, t1.LookupValue as PartnerTypeValue " +
                    "from tblMemberPartner t0 " +
                    "left join tblLookup t1 on t1.LookupKey = t0.PartnerType and t1.TableName = 'tblMemberPartner' and t1.FieldName = 'PartnerType' " +
                    "where t0.MemberID = @MemberID and t0.PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@PartnerType", Me.SellerType)
                ).ToList
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSellerRecord(MemberID As Integer, id As Integer) As Members.Partner Implements IMemberPartner.GetSellerRecord
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of Members.Partner)(
                    "select MemberPartnerID, MemberID, PartnerType, FullName " +
                    "from tblMemberPartner " +
                    "where MemberID = @MemberID and MemberPartnerID = @MemberPartnerID and PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.SellerType)
                ).SingleOrDefault
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddSellerRecord(p As Members.Partner) As Status Implements IMemberPartner.AddSellerRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "insert into tblMemberPartner(MemberID, PartnerType, FullName, CreatedBy, CreatedAgent, CreatedIp) " +
                    "values(@MemberID, @PartnerType, @FullName, @ByID, @UserAgent, @IpAddress)",
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@PartnerType", Me.SellerType),
                    New SqlParameter("@FullName", p.FullName),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditSellerRecord(p As Members.Partner) As Status Implements IMemberPartner.EditSellerRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "update tblMemberPartner " +
                    "set FullName = @FullName, " +
                    "ModifiedBy = @ByID, ModifiedOn = dbo.getLocalTime(default), ModifiedOnUtc = getutcdate(), ModifiedAgent = @UserAgent, ModifiedIp = @IpAddress " +
                    "where MemberID = @MemberID and MemberPartnerID = @MemberPartnerID and PartnerType = @PartnerType",
                    New SqlParameter("@MemberPartnerID", p.MemberPartnerID),
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@PartnerType", Me.SellerType),
                    New SqlParameter("@FullName", p.FullName),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteSellerRecord(MemberID As Integer, id As Integer) As Status Implements IMemberPartner.DeleteSellerRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "delete from tblMemberPartner where MemberID = @MemberID and MemberPartnerID = @MemberPartnerID and PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.SellerType)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsSellerCodeExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsSellerCodeExist
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMemberPartner where MemberID = @MemberID and PartnerType = @PartnerType and MemberPartnerCode = @MemberPartnerCode and not MemberPartnerID = @MemberPartnerID",
                    New SqlParameter("@MemberID", id),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.SellerType),
                    New SqlParameter("@MemberPartnerCode", value)
                ).SingleOrDefault
            End Using

            Return If(Not i = 0, True, False)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsSellerNameExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsSellerNameExist
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMemberPartner where MemberID = @MemberID and PartnerType = @PartnerType and MemberPartnerName = @MemberPartnerName and not MemberPartnerID = @MemberPartnerID",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.SellerType),
                    New SqlParameter("@MemberPartnerName", value)
                ).SingleOrDefault
            End Using

            Return If(Not i = 0, True, False)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Buyer
    Public Function GetBuyerRecords(MemberID As Integer) As Members.PartnerVM Implements IMemberPartner.GetBuyerRecords
        Dim p = New Members.PartnerVM

        Try
            Using db As New DbContext
                p.ItemList = db.Database.SqlQuery(Of Members.Partner)(
                    "select row_number() over(order by MemberPartnerID) as RowNum, " +
                    "MemberPartnerID, MemberID, PartnerType, FullName, t1.LookupValue as PartnerTypeValue " +
                    "from tblMemberPartner t0 " +
                    "left join tblLookup t1 on t1.LookupKey = t0.PartnerType and t1.TableName = 'tblMemberPartner' and t1.FieldName = 'PartnerType' " +
                    "where t0.MemberID = @MemberID and t0.PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@PartnerType", Me.BuyerType)
                ).ToList
            End Using

            Return p
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBuyerList(MemberID As Integer) As IEnumerable(Of Members.Partner) Implements IMemberPartner.GetBuyerList
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of Members.Partner)(
                    "select MemberPartnerID, MemberID, PartnerType, FullName, t1.LookupValue as PartnerTypeValue " +
                    "from tblMemberPartner t0 " +
                    "left join tblLookup t1 on t1.LookupKey = t0.PartnerType and t1.TableName = 'tblMemberPartner' and t1.FieldName = 'PartnerType' " +
                    "where t0.MemberID = @MemberID and t0.PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@PartnerType", Me.BuyerType)
                ).ToList
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBuyerRecord(MemberID As Integer, id As Integer) As Members.Partner Implements IMemberPartner.GetBuyerRecord
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of Members.Partner)(
                    "select MemberPartnerID, MemberID, PartnerType, FullName " +
                    "from tblMemberPartner " +
                    "where MemberID = @MemberID and MemberPartnerID = @MemberPartnerID and PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.BuyerType)
                ).SingleOrDefault
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddBuyerRecord(p As Members.Partner) As Status Implements IMemberPartner.AddBuyerRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "insert into tblMemberPartner(MemberID, PartnerType, FullName, CreatedBy, CreatedAgent, CreatedIp) " +
                    "values(@MemberID, @PartnerType, @FullName, @ByID, @UserAgent, @IpAddress)",
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@PartnerType", Me.BuyerType),
                    New SqlParameter("@FullName", p.FullName),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditBuyerRecord(p As Members.Partner) As Status Implements IMemberPartner.EditBuyerRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "update tblMemberPartner " +
                    "set FullName = @FullName, " +
                    "ModifiedBy = @ByID, ModifiedOn = dbo.getLocalTime(default), ModifiedOnUtc = getutcdate(), ModifiedAgent = @UserAgent, ModifiedIp = @IpAddress " +
                    "where MemberID = @MemberID and MemberPartnerID = @MemberPartnerID and PartnerType = @PartnerType",
                    New SqlParameter("@MemberPartnerID", p.MemberPartnerID),
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@PartnerType", Me.BuyerType),
                    New SqlParameter("@FullName", p.FullName),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteBuyerRecord(MemberID As Integer, id As Integer) As Status Implements IMemberPartner.DeleteBuyerRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "delete from tblMemberPartner where MemberID = @MemberID and MemberPartnerID = @MemberPartnerID and PartnerType = @PartnerType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.BuyerType)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsBuyerCodeExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsBuyerCodeExist
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMemberPartner where MemberID = @MemberID and PartnerType = @PartnerType and MemberPartnerCode = @MemberPartnerCode and not MemberPartnerID = @MemberPartnerID",
                    New SqlParameter("@MemberID", id),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.BuyerType),
                    New SqlParameter("@MemberPartnerCode", value)
                ).SingleOrDefault
            End Using

            Return If(Not i = 0, True, False)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsBuyerNameExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsBuyerNameExist
        Dim i = 0

        Try
            Using db As New DbContext
                i = db.Database.SqlQuery(Of Integer)(
                    "select count(*) from tblMemberPartner where MemberID = @MemberID and PartnerType = @PartnerType and MemberPartnerName = @MemberPartnerName and not MemberPartnerID = @MemberPartnerID",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberPartnerID", id),
                    New SqlParameter("@PartnerType", Me.BuyerType),
                    New SqlParameter("@MemberPartnerName", value)
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