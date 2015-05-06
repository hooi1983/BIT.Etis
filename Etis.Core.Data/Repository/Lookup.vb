Public Class Lookup
    Implements ILookup

    Public Function GetRecords(TableName As Enums.EnumTable, FieldName As Enums.EnumField) As Lookups.LookupVM Implements ILookup.GetRecords
        Dim p = New Lookups.LookupVM

        Try
            Using db As New DbContext
                p.ItemList = db.Database.SqlQuery(Of Lookups.Lookup)(
                    "select LookupKey, LookupValue " +
                    "from tblLookup " +
                    "where TableName = @TableName and FieldName = @FieldName and Status = 1 " +
                    "order by ListOrder",
                    New SqlParameter("@TableName", [Enum].GetName(GetType(Enums.EnumTable), TableName)),
                    New SqlParameter("@FieldName", [Enum].GetName(GetType(Enums.EnumField), FieldName))
                ).ToList
            End Using

            Return p
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetRecord(TableName As Enums.EnumTable, FieldName As Enums.EnumField, Key As String) As String Implements ILookup.GetRecord
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of String)(
                    "select LookupValue " +
                    "from tblLookup " +
                    "where TableName = @TableName and FieldName = @FieldName and LookupKey = @Key and Status = 1",
                    New SqlParameter("@TableName", [Enum].GetName(GetType(Enums.EnumTable), TableName)),
                    New SqlParameter("@FieldName", [Enum].GetName(GetType(Enums.EnumField), FieldName)),
                    New SqlParameter("@Key", Key)
                ).SingleOrDefault
            End Using

            Return String.Empty
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