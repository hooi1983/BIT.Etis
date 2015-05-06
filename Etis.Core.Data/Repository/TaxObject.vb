Public Class TaxObject
    Implements ITaxObject

    Public Function GetRecords(TaxItemType As String, TaxType As String) As TaxObjects.TaxObjectVM Implements ITaxObject.GetRecords
        Dim p = New TaxObjects.TaxObjectVM

        Try
            Using db As New DbContext
                p.ItemList = db.Database.SqlQuery(Of TaxObjects.TaxObject)(
                    "select row_number() over(order by ListOrder) as RowNum, TaxObjectID, TaxObjectCode, TaxObjectName, TaxPercent " +
                    "from tblTaxObject " +
                    "where TaxItemType = @TaxItemType and TaxType = @TaxType",
                    New SqlParameter("@TaxItemType", TaxItemType),
                    New SqlParameter("@TaxType", TaxType)
                ).ToList
            End Using

            Return p
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetList(TaxItemType As String, TaxType As String) As IEnumerable(Of TaxObjects.TaxObject) Implements ITaxObject.GetList
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of TaxObjects.TaxObject)(
                    "select TaxObjectID, TaxObjectCode, TaxObjectName, TaxPercent " +
                    "from tblTaxObject " +
                    "where TaxItemType = @TaxItemType and TaxType = @TaxType and Status = 1 " +
                    "order by ListOrder, TaxObjectName",
                    New SqlParameter("@TaxItemType", TaxItemType),
                    New SqlParameter("@TaxType", TaxType)
                ).ToList
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