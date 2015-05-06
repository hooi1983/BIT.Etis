Public Class MemberPartner
    Implements IMemberPartner

    ' Seller
    Public Function GetSellerRecords(MemberID As Integer) As Members.PartnerVM Implements IMemberPartner.GetSellerRecords
        Try
            Using dat As New Data.MemberPartner
                Return dat.GetSellerRecords(MemberID)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSellerList(MemberID As Integer) As IEnumerable(Of Members.Partner) Implements IMemberPartner.GetSellerList
        Try
            Using dat As New Data.MemberPartner
                Return dat.GetSellerList(MemberID)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSellerRecord(MemberID As Integer, id As Integer) As Members.Partner Implements IMemberPartner.GetSellerRecord
        Try
            Using dat As New Data.MemberPartner
                Return dat.GetSellerRecord(MemberID, id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddSellerRecord(p As Members.Partner) As Status Implements IMemberPartner.AddSellerRecord
        Try
            If String.IsNullOrWhiteSpace(p.FullName) Then Return New Status("0", "Error. Name is required.")

            Using dat As New Data.MemberPartner
                Return dat.AddSellerRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditSellerRecord(p As Members.Partner) As Status Implements IMemberPartner.EditSellerRecord
        Try
            If String.IsNullOrWhiteSpace(p.FullName) Then Return New Status("0", "Error. Name is required.")

            Using dat As New Data.MemberPartner
                Return dat.EditSellerRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteSellerRecord(MemberID As Integer, id As Integer) As Status Implements IMemberPartner.DeleteSellerRecord
        Try
            Using dat As New Data.MemberPartner
                Return dat.DeleteSellerRecord(MemberID, id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsSellerCodeExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsSellerCodeExist
        Try
            Using dat As New Data.MemberPartner
                Return dat.IsSellerCodeExist(MemberID, id, value)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsSellerNameExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsSellerNameExist
        Try
            Using dat As New Data.MemberPartner
                Return dat.IsSellerNameExist(MemberID, id, value)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Buyer
    Public Function GetBuyerRecords(MemberID As Integer) As Members.PartnerVM Implements IMemberPartner.GetBuyerRecords
        Try
            Using dat As New Data.MemberPartner
                Return dat.GetBuyerRecords(MemberID)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBuyerList(MemberID As Integer) As IEnumerable(Of Members.Partner) Implements IMemberPartner.GetBuyerList
        Try
            Using dat As New Data.MemberPartner
                Return dat.GetBuyerList(MemberID)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBuyerRecord(MemberID As Integer, id As Integer) As Members.Partner Implements IMemberPartner.GetBuyerRecord
        Try
            Using dat As New Data.MemberPartner
                Return dat.GetBuyerRecord(MemberID, id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddBuyerRecord(p As Members.Partner) As Status Implements IMemberPartner.AddBuyerRecord
        Try
            If String.IsNullOrWhiteSpace(p.FullName) Then Return New Status("0", "Error. Name is required.")

            Using dat As New Data.MemberPartner
                Return dat.AddBuyerRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditBuyerRecord(p As Members.Partner) As Status Implements IMemberPartner.EditBuyerRecord
        Try
            If String.IsNullOrWhiteSpace(p.FullName) Then Return New Status("0", "Error. Name is required.")

            Using dat As New Data.MemberPartner
                Return dat.EditBuyerRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteBuyerRecord(MemberID As Integer, id As Integer) As Status Implements IMemberPartner.DeleteBuyerRecord
        Try
            Using dat As New Data.MemberPartner
                Return dat.DeleteBuyerRecord(MemberID, id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsBuyerCodeExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsBuyerCodeExist
        Try
            Using dat As New Data.MemberPartner
                Return dat.IsBuyerCodeExist(MemberID, id, value)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsBuyerNameExist(MemberID As Integer, id As Integer, value As String) As Boolean Implements IMemberPartner.IsBuyerNameExist
        Try
            Using dat As New Data.MemberPartner
                Return dat.IsBuyerNameExist(MemberID, id, value)
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