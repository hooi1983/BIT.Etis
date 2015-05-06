Public Class Lookup
    Implements ILookup

    Public Function GetRecords(TableName As Enums.EnumTable, FieldName As Enums.EnumField) As Lookups.LookupVM Implements ILookup.GetRecords
        Try
            Using dat As New Data.Lookup
                Return dat.GetRecords(TableName, FieldName)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetRecord(TableName As Enums.EnumTable, FieldName As Enums.EnumField, Key As String) As String Implements ILookup.GetRecord
        Try
            Using dat As New Data.Lookup
                Return dat.GetRecord(TableName, FieldName, Key)
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