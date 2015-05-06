Public Class DbContext
    Inherits Entity.DbContext

    Sub New()
        MyBase.New("Server=(local)\MSSQL;Database=Etis;User Id=sa;Password=123456;")
        'MyBase.New("Server=b2zr90qzs4.database.windows.net,1433;Database=etis2;User Id=etis;Password=xF3h_fG!4haP;")
    End Sub
End Class