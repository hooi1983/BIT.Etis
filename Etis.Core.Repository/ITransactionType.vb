Public Interface ITransactionType
    Inherits IDisposable

    Function GetParentRecords() As Entities.TransactionTypes.TransactionTypeVM
    Function GetRecords(ParentID As Integer) As Entities.TransactionTypes.TransactionTypeVM
    Function GetParentList() As IEnumerable(Of Entities.TransactionTypes.TransactionType)
    Function GetList(ParentID As Integer) As IEnumerable(Of Entities.TransactionTypes.TransactionType)
End Interface