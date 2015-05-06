Public Interface ILookup
    Inherits IDisposable

    Function GetRecords(TableName As Entities.Enums.EnumTable, FieldName As Entities.Enums.EnumField) As Entities.Lookups.LookupVM
    Function GetRecord(TableName As Entities.Enums.EnumTable, FieldName As Entities.Enums.EnumField, Key As String) As String
End Interface