Public Interface IMember
    Inherits IDisposable

    Function GetRecords(pg As Integer) As Entities.Members.MemberVM
    Function GetRecord(id As Integer) As Entities.Members.Member
    Function AddRecord(p As Entities.Members.Member) As Entities.Status
    Function EditRecord(p As Entities.Members.Member) As Entities.Status
    Function DeleteRecord(id As Integer) As Entities.Status
    Function ResetPassword(p As Entities.Members.MemberPassword) As Entities.Status
    Function ResetMemberPassword(p As Entities.Members.MemberPassword) As Entities.Status
    Function Login(p As Entities.Members.MemberLogin) As Entities.Members.Member
    Function IsLoginNameExist(id As Integer, value As String) As Boolean
    Function IsEmailExist(id As Integer, value As String) As Boolean
    Function IsValidLogin(p As Entities.Members.MemberLogin) As Boolean
    Function IsValidPassword(p As Entities.Members.MemberPassword) As Boolean
End Interface