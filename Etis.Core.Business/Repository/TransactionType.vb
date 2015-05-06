Public Class TransactionType
    Implements ITransactionType

    Public Function GetParentRecords() As TransactionTypes.TransactionTypeVM Implements ITransactionType.GetParentRecords
        Try
            Using dat As New Data.TransactionType
                Return dat.GetParentRecords()
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetRecords(ParentID As Integer) As TransactionTypes.TransactionTypeVM Implements ITransactionType.GetRecords
        Try
            Using dat As New Data.TransactionType
                Return dat.GetRecords(ParentID)
            End Using
        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function GetParentList() As IEnumerable(Of TransactionTypes.TransactionType) Implements ITransactionType.GetParentList
        Try
            Using dat As New Data.TransactionType
                Return dat.GetParentList
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetList(ParentID As Integer) As IEnumerable(Of TransactionTypes.TransactionType) Implements ITransactionType.GetList
        Try
            Using dat As New Data.TransactionType
                Return dat.GetList(ParentID)
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