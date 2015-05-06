Public Interface ITaxObject
    Inherits IDisposable

    Function GetRecords(TaxItemType As String, TaxType As String) As Entities.TaxObjects.TaxObjectVM
    Function GetList(TaxItemType As String, TaxType As String) As IEnumerable(Of Entities.TaxObjects.TaxObject)
End Interface