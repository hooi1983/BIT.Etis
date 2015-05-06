Public Class MemberDocument
    Implements IMemberDocument

    ' Sales
    Public Function GetSalesRecords(MemberID As Integer, Optional CurrentPage As Integer = 1, Optional Search As String = "") As Members.DocumentVM Implements IMemberDocument.GetSalesRecords
        Try
            Using dat As New Data.MemberDocument
                Return dat.GetSalesRecords(MemberID, CurrentPage, Search)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSalesList(MemberID As Integer) As IEnumerable(Of Members.Document) Implements IMemberDocument.GetSalesList
        Try
            Using dat As New Data.MemberDocument
                Return dat.GetSalesList(MemberID)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSalesRecord(MemberID As Integer, id As Integer) As Members.Document Implements IMemberDocument.GetSalesRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.GetSalesRecord(MemberID, id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddSalesRecord(p As Members.Document) As Status Implements IMemberDocument.AddSalesRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.AddSalesRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditSalesRecord(p As Members.Document) As Status Implements IMemberDocument.EditSalesRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.EditSalesRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteSalesRecord(MemberID As Integer, id As Integer) As Status Implements IMemberDocument.DeleteSalesRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.DeleteSalesRecord(MemberID, id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Purchase
    Public Function GetPurchaseRecords(MemberID As Integer, Optional CurrentPage As Integer = 1, Optional Search As String = "") As Members.DocumentVM Implements IMemberDocument.GetPurchaseRecords
        Try
            Using dat As New Data.MemberDocument
                Return dat.GetPurchaseRecords(MemberID, CurrentPage, Search)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPurchaseList(MemberID As Integer) As IEnumerable(Of Members.Document) Implements IMemberDocument.GetPurchaseList
        Try
            Using dat As New Data.MemberDocument
                Return dat.GetPurchaseList(MemberID)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPurchaseRecord(MemberID As Integer, id As Integer) As Members.Document Implements IMemberDocument.GetPurchaseRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.GetPurchaseRecord(MemberID, id)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddPurchaseRecord(p As Members.Document) As Status Implements IMemberDocument.AddPurchaseRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.AddPurchaseRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function EditPurchaseRecord(p As Members.Document) As Status Implements IMemberDocument.EditPurchaseRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.EditPurchaseRecord(p)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeletePurchaseRecord(MemberID As Integer, id As Integer) As Status Implements IMemberDocument.DeletePurchaseRecord
        Try
            Using dat As New Data.MemberDocument
                Return dat.DeletePurchaseRecord(MemberID, id)
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