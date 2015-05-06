Public Class Pagination
    Property CurrentPage As Integer = 1
    Property PageSize As Integer = 20
    Property RowCount As Int64
    Property PageCount As Integer
    Property Search As String = String.Empty
End Class