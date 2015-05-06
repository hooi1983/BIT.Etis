Public Class MemberDocument
    Implements IMemberDocument

    ReadOnly SalesType As String = "ARIV"
    ReadOnly PurchaseType As String = "APIV"

    ' Sales
    Public Function GetSalesRecords(MemberID As Integer, Optional CurrentPage As Integer = 1, Optional Search As String = "") As Members.DocumentVM Implements IMemberDocument.GetSalesRecords
        Dim p = New Members.DocumentVM With {.CurrentPage = CurrentPage, .Search = Search}
        Dim sb = New System.Text.StringBuilder

        Try
            Using db As New DbContext
                sb.Append("select count(*) from tblMemberDocument where MemberID = @MemberID and DocumentType = @DocumentType ")

                If Not String.IsNullOrWhiteSpace(p.Search) Then
                    sb.Append("and (DocumentNo like concat('%', @Search, '%'))")
                End If

                p.RowCount = db.Database.SqlQuery(Of Integer)(
                    sb.ToString,
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@DocumentType", SalesType),
                    New SqlParameter("@Search", p.Search)
                ).SingleOrDefault

                sb.Clear()
                sb.Append(
                    "select row_number() over(order by MemberDocumentID) as RowNum, " +
                    "MemberDocumentID, MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "Ppn, GrandTotalAmount, VatTotalAmount, TotalAmount " +
                    "from tblMemberDocument " +
                    "where MemberID = @MemberID and DocumentType = @DocumentType "
                )

                If Not String.IsNullOrWhiteSpace(p.Search) Then
                    sb.Append("and (DocumentNo like concat('%', @Search, '%')) ")
                End If

                sb.Append(
                    "order by MemberDocumentID " +
                    "offset @Offset rows " +
                    "fetch next @Fetch rows only"
                )

                p.ItemList = db.Database.SqlQuery(Of Members.Document)(
                    sb.ToString,
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@DocumentType", SalesType),
                    New SqlParameter("@Search", p.Search),
                    New SqlParameter("@Offset", (p.CurrentPage - 1) * p.PageSize),
                    New SqlParameter("@Fetch", p.PageSize)
                ).ToList
            End Using

            p.PageCount = Math.Ceiling(p.RowCount / p.PageSize)

            Return p
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSalesList(MemberID As Integer) As IEnumerable(Of Members.Document) Implements IMemberDocument.GetSalesList
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of Members.Document)(
                    "select MemberDocumentID, MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "Ppn, GrandTotalAmount, VatTotalAmount, TotalAmount " +
                    "from tblMemberDocument " +
                    "where MemberID = @MemberID and DocumentType = @DocumentType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@DocumentType", SalesType)
                ).ToList
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSalesRecord(MemberID As Integer, id As Integer) As Members.Document Implements IMemberDocument.GetSalesRecord
        Dim Model = New Entities.Members.Document

        Try
            Using db As New DbContext
                Model = db.Database.SqlQuery(Of Members.Document)(
                    "select MemberDocumentID, MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "Ppn, GrandTotalAmount, VatTotalAmount, TotalAmount " +
                    "from tblMemberDocument " +
                    "where MemberID = @MemberID and MemberDocumentID = @MemberDocumentID and DocumentType = @DocumentType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberDocumentID", id),
                    New SqlParameter("@DocumentType", SalesType)
                ).SingleOrDefault

                Model.ItemList = db.Database.SqlQuery(Of Members.DocumentItem)(
                    "select MemberDocumentItemID, MemberDocumentID, TaxObjectID, TaxItemType, TaxType, Quantity, UnitPrice, TaxPercent, SubGrandTotalAmount, Description " +
                    "from tblMemberDocumentItem " +
                    "where MemberDocumentID = @MemberDocumentID",
                    New SqlParameter("@MemberDocumentID", id)
                ).ToList
            End Using

            Return Model
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' want use 1 connection to insert all or 1 by 1?
    Public Function AddSalesRecord(p As Members.Document) As Status Implements IMemberDocument.AddSalesRecord
        Try
            Using db As New DbContext
                p.MemberDocumentID = db.Database.SqlQuery(Of Integer)(
                    "begin transaction; " +
                    "insert into tblMemberDocument(MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "CreatedBy, CreatedAgent, CreatedIp) " +
                    "values(@MemberID, @MemberPartnerID, @CurrencyID, @TransactionTypeID, @TransactionTypeSubID, @DocumentType, @DocumentNo, @DocumentDate, @DueDate, @PaymentDate, " +
                    "@ByID, @UserAgent, @IpAddress); " +
                    "select cast(scope_identity() as int); " +
                    "commit;",
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@MemberPartnerID", p.MemberPartnerID),
                    New SqlParameter("@CurrencyID", p.CurrencyID),
                    New SqlParameter("@TransactionTypeID", p.TransactionTypeID),
                    New SqlParameter("@TransactionTypeSubID", p.TransactionTypeSubID),
                    New SqlParameter("@DocumentType", SalesType),
                    New SqlParameter("@DocumentNo", p.DocumentNo),
                    New SqlParameter("@DocumentDate", p.DocumentDate),
                    New SqlParameter("@DueDate", p.DueDate),
                    New SqlParameter("@PaymentDate", p.PaymentDate),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                ).SingleOrDefault

                If p.ItemList IsNot Nothing Then
                    For Each q In p.ItemList
                        db.Database.ExecuteSqlCommand(
                            "begin transaction; " +
                            "declare @TaxPercent decimal(5,2) = 0; " +
                            "select @TaxPercent = TaxPercent from tblTaxObject where TaxObjectID = @TaxObjectID; " +
                            "insert into tblMemberDocumentItem(MemberDocumentID, TaxObjectID, TaxItemType, TaxType, Quantity, UnitPrice, TaxPercent, Description) " +
                            "values(@MemberDocumentID, @TaxObjectID, @TaxItemType, @TaxType, @Quantity, @UnitPrice, @TaxPercent, @Description); " +
                            "commit;",
                            New SqlParameter("@MemberDocumentID", p.MemberDocumentID),
                            New SqlParameter("@TaxObjectID", q.TaxObjectID),
                            New SqlParameter("@TaxItemType", q.TaxItemType),
                            New SqlParameter("@TaxType", q.TaxType),
                            New SqlParameter("@Quantity", q.Quantity),
                            New SqlParameter("@UnitPrice", q.UnitPrice),
                            New SqlParameter("@Description", q.Description)
                        )
                    Next
                End If

                db.Database.ExecuteSqlCommand(
                    "begin transaction; " +
                    "declare @GrandTotalAmount decimal(12,2) = 0, @VatTotalAmount decimal(12,2) = 0, @TotalAmount decimal(12,2) = 0; " +
                    "select @GrandTotalAmount = isnull(sum(SubGrandTotalAmount), 0), @VatTotalAmount = isnull(sum(SubVatTotalAmount), 0), @TotalAmount = isnull(sum(SubTotalAmount), 0) " +
                    "from tblMemberDocumentItem " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "update tblMemberDocument set GrandTotalAmount = @GrandTotalAmount, VatTotalAmount = @VatTotalAmount, TotalAmount = @TotalAmount " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "commit;",
                    New SqlParameter("@MemberDocumentID", p.MemberDocumentID)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' TODO: fast way is delete all item just insert, after that just use Ajax to delete line when trigger delete line item
    Public Function EditSalesRecord(p As Members.Document) As Status Implements IMemberDocument.EditSalesRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "update tblMemberDocument " +
                    "set MemberPartnerID = @MemberPartnerID, CurrencyID = @CurrencyID, TransactionTypeID = @TransactionTypeID, TransactionTypeSubID = @TransactionTypeSubID, " +
                    "DocumentNo = @DocumentNo, DocumentDate = @DocumentDate, DueDate = @DueDate, PaymentDate = @PaymentDate, " +
                    "ModifiedBy = @ByID, ModifiedOn = dbo.getLocalTime(default), ModifiedOnUtc = getutcdate(), ModifiedAgent = @UserAgent, ModifiedIp = @IpAddress " +
                    "where MemberID = @MemberID and MemberDocumentID = @MemberDocumentID and DocumentType = @DocumentType; " +
                    "delete from tblMemberDocumentItem where MemberDocumentID = @MemberDocumentID; ",
                    New SqlParameter("@MemberDocumentID", p.MemberDocumentID),
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@MemberPartnerID", p.MemberPartnerID),
                    New SqlParameter("@CurrencyID", p.CurrencyID),
                    New SqlParameter("@TransactionTypeID", p.TransactionTypeID),
                    New SqlParameter("@TransactionTypeSubID", p.TransactionTypeSubID),
                    New SqlParameter("@DocumentType", SalesType),
                    New SqlParameter("@DocumentNo", p.DocumentNo),
                    New SqlParameter("@DocumentDate", p.DocumentDate),
                    New SqlParameter("@DueDate", p.DueDate),
                    New SqlParameter("@PaymentDate", p.PaymentDate),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                )

                If p.ItemList IsNot Nothing Then
                    For Each q In p.ItemList
                        db.Database.ExecuteSqlCommand(
                            "begin transaction; " +
                            "declare @TaxPercent decimal(5,2) = 0; " +
                            "select @TaxPercent = TaxPercent from tblTaxObject where TaxObjectID = @TaxObjectID; " +
                            "insert into tblMemberDocumentItem(MemberDocumentID, TaxObjectID, TaxItemType, TaxType, Quantity, UnitPrice, TaxPercent, Description) " +
                            "values(@MemberDocumentID, @TaxObjectID, @TaxItemType, @TaxType, @Quantity, @UnitPrice, @TaxPercent, @Description); " +
                            "commit;",
                            New SqlParameter("@MemberDocumentID", p.MemberDocumentID),
                            New SqlParameter("@TaxObjectID", q.TaxObjectID),
                            New SqlParameter("@TaxItemType", q.TaxItemType),
                            New SqlParameter("@TaxType", q.TaxType),
                            New SqlParameter("@Quantity", q.Quantity),
                            New SqlParameter("@UnitPrice", q.UnitPrice),
                            New SqlParameter("@Description", q.Description)
                        )
                    Next
                End If

                db.Database.ExecuteSqlCommand(
                    "begin transaction; " +
                    "declare @GrandTotalAmount decimal(12,2) = 0, @VatTotalAmount decimal(12,2) = 0, @TotalAmount decimal(12,2) = 0; " +
                    "select @GrandTotalAmount = isnull(sum(SubGrandTotalAmount), 0), @VatTotalAmount = isnull(sum(SubVatTotalAmount), 0), @TotalAmount = isnull(sum(SubTotalAmount), 0) " +
                    "from tblMemberDocumentItem " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "update tblMemberDocument set GrandTotalAmount = @GrandTotalAmount, VatTotalAmount = @VatTotalAmount, TotalAmount = @TotalAmount " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "commit;",
                    New SqlParameter("@MemberDocumentID", p.MemberDocumentID)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteSalesRecord(MemberID As Integer, id As Integer) As Status Implements IMemberDocument.DeleteSalesRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "begin transaction; " +
                    "delete from tblMemberDocument where MemberID = @MemberID and MemberDocumentID = @MemberDocumentID and DocumentType = @DocumentType; " +
                    "delete from tblMemberDocument where MemberDocumentID = @MemberDocumentID; " +
                    "commit;",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberDocumentID", id),
                    New SqlParameter("@DocumentType", SalesType)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Purchase
    Public Function GetPurchaseRecords(MemberID As Integer, Optional CurrentPage As Integer = 1, Optional Search As String = "") As Members.DocumentVM Implements IMemberDocument.GetPurchaseRecords
        Dim p = New Members.DocumentVM With {.CurrentPage = CurrentPage, .Search = Search}
        Dim sb = New System.Text.StringBuilder

        Try
            Using db As New DbContext
                sb.Append("select count(*) from tblMemberDocument where MemberID = @MemberID and DocumentType = @DocumentType ")

                If Not String.IsNullOrWhiteSpace(p.Search) Then
                    sb.Append("and (DocumentNo like concat('%', @Search, '%'))")
                End If

                p.RowCount = db.Database.SqlQuery(Of Integer)(
                    sb.ToString,
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@DocumentType", PurchaseType),
                    New SqlParameter("@Search", p.Search)
                ).SingleOrDefault

                sb.Clear()
                sb.Append(
                    "select row_number() over(order by MemberDocumentID) as RowNum, " +
                    "MemberDocumentID, MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "Ppn, GrandTotalAmount, VatTotalAmount, TotalAmount " +
                    "from tblMemberDocument " +
                    "where MemberID = @MemberID and DocumentType = @DocumentType "
                )

                If Not String.IsNullOrWhiteSpace(p.Search) Then
                    sb.Append("and (DocumentNo like concat('%', @Search, '%')) ")
                End If

                sb.Append(
                    "order by MemberDocumentID " +
                    "offset @Offset rows " +
                    "fetch next @Fetch rows only"
                )

                p.ItemList = db.Database.SqlQuery(Of Members.Document)(
                    sb.ToString,
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@DocumentType", PurchaseType),
                    New SqlParameter("@Search", p.Search),
                    New SqlParameter("@Offset", (p.CurrentPage - 1) * p.PageSize),
                    New SqlParameter("@Fetch", p.PageSize)
                ).ToList
            End Using

            p.PageCount = Math.Ceiling(p.RowCount / p.PageSize)

            Return p
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPurchaseList(MemberID As Integer) As IEnumerable(Of Members.Document) Implements IMemberDocument.GetPurchaseList
        Try
            Using db As New DbContext
                Return db.Database.SqlQuery(Of Members.Document)(
                    "select MemberDocumentID, MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "Ppn, GrandTotalAmount, VatTotalAmount, TotalAmount " +
                    "from tblMemberDocument " +
                    "where MemberID = @MemberID and DocumentType = @DocumentType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@DocumentType", PurchaseType)
                ).ToList
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPurchaseRecord(MemberID As Integer, id As Integer) As Members.Document Implements IMemberDocument.GetPurchaseRecord
        Dim Model = New Entities.Members.Document

        Try
            Using db As New DbContext
                Model = db.Database.SqlQuery(Of Members.Document)(
                    "select MemberDocumentID, MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "Ppn, GrandTotalAmount, VatTotalAmount, TotalAmount " +
                    "from tblMemberDocument " +
                    "where MemberID = @MemberID and MemberDocumentID = @MemberDocumentID and DocumentType = @DocumentType",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberDocumentID", id),
                    New SqlParameter("@DocumentType", PurchaseType)
                ).SingleOrDefault

                Model.ItemList = db.Database.SqlQuery(Of Members.DocumentItem)(
                    "select MemberDocumentItemID, MemberDocumentID, TaxObjectID, TaxItemType, TaxType, Quantity, UnitPrice, TaxPercent, SubGrandTotalAmount, Description " +
                    "from tblMemberDocumentItem " +
                    "where MemberDocumentID = @MemberDocumentID",
                    New SqlParameter("@MemberDocumentID", id)
                ).ToList
            End Using

            Return Model
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' want use 1 connection to insert all or 1 by 1?
    Public Function AddPurchaseRecord(p As Members.Document) As Status Implements IMemberDocument.AddPurchaseRecord
        Try
            Using db As New DbContext
                p.MemberDocumentID = db.Database.SqlQuery(Of Integer)(
                    "begin transaction; " +
                    "insert into tblMemberDocument(MemberID, MemberPartnerID, CurrencyID, TransactionTypeID, TransactionTypeSubID, DocumentType, DocumentNo, DocumentDate, DueDate, PaymentDate, " +
                    "CreatedBy, CreatedAgent, CreatedIp) " +
                    "values(@MemberID, @MemberPartnerID, @CurrencyID, @TransactionTypeID, @TransactionTypeSubID, @DocumentType, @DocumentNo, @DocumentDate, @DueDate, @PaymentDate, " +
                    "@ByID, @UserAgent, @IpAddress); " +
                    "select cast(scope_identity() as int); " +
                    "commit;",
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@MemberPartnerID", p.MemberPartnerID),
                    New SqlParameter("@CurrencyID", p.CurrencyID),
                    New SqlParameter("@TransactionTypeID", p.TransactionTypeID),
                    New SqlParameter("@TransactionTypeSubID", p.TransactionTypeSubID),
                    New SqlParameter("@DocumentType", PurchaseType),
                    New SqlParameter("@DocumentNo", p.DocumentNo),
                    New SqlParameter("@DocumentDate", p.DocumentDate),
                    New SqlParameter("@DueDate", p.DueDate),
                    New SqlParameter("@PaymentDate", p.PaymentDate),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                ).SingleOrDefault

                If p.ItemList IsNot Nothing Then
                    For Each q In p.ItemList
                        db.Database.ExecuteSqlCommand(
                            "begin transaction; " +
                            "declare @TaxPercent decimal(5,2) = 0; " +
                            "select @TaxPercent = TaxPercent from tblTaxObject where TaxObjectID = @TaxObjectID; " +
                            "insert into tblMemberDocumentItem(MemberDocumentID, TaxObjectID, TaxItemType, TaxType, Quantity, UnitPrice, TaxPercent, Description) " +
                            "values(@MemberDocumentID, @TaxObjectID, @TaxItemType, @TaxType, @Quantity, @UnitPrice, @TaxPercent, @Description); " +
                            "commit;",
                            New SqlParameter("@MemberDocumentID", p.MemberDocumentID),
                            New SqlParameter("@TaxObjectID", q.TaxObjectID),
                            New SqlParameter("@TaxItemType", q.TaxItemType),
                            New SqlParameter("@TaxType", q.TaxType),
                            New SqlParameter("@Quantity", q.Quantity),
                            New SqlParameter("@UnitPrice", q.UnitPrice),
                            New SqlParameter("@Description", q.Description)
                        )
                    Next
                End If

                db.Database.ExecuteSqlCommand(
                    "begin transaction; " +
                    "declare @GrandTotalAmount decimal(12,2) = 0, @VatTotalAmount decimal(12,2) = 0, @TotalAmount decimal(12,2) = 0; " +
                    "select @GrandTotalAmount = isnull(sum(SubGrandTotalAmount), 0), @VatTotalAmount = isnull(sum(SubVatTotalAmount), 0), @TotalAmount = isnull(sum(SubTotalAmount), 0) " +
                    "from tblMemberDocumentItem " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "update tblMemberDocument set GrandTotalAmount = @GrandTotalAmount, VatTotalAmount = @VatTotalAmount, TotalAmount = @TotalAmount " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "commit;",
                    New SqlParameter("@MemberDocumentID", p.MemberDocumentID)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' TODO: fast way is delete all item just insert, after that just use Ajax to delete line when trigger delete line item
    Public Function EditPurchaseRecord(p As Members.Document) As Status Implements IMemberDocument.EditPurchaseRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "update tblMemberDocument " +
                    "set MemberPartnerID = @MemberPartnerID, CurrencyID = @CurrencyID, TransactionTypeID = @TransactionTypeID, TransactionTypeSubID = @TransactionTypeSubID, " +
                    "DocumentNo = @DocumentNo, DocumentDate = @DocumentDate, DueDate = @DueDate, PaymentDate = @PaymentDate, " +
                    "ModifiedBy = @ByID, ModifiedOn = dbo.getLocalTime(default), ModifiedOnUtc = getutcdate(), ModifiedAgent = @UserAgent, ModifiedIp = @IpAddress " +
                    "where MemberID = @MemberID and MemberDocumentID = @MemberDocumentID and DocumentType = @DocumentType; " +
                    "delete from tblMemberDocumentItem where MemberDocumentID = @MemberDocumentID; ",
                    New SqlParameter("@MemberDocumentID", p.MemberDocumentID),
                    New SqlParameter("@MemberID", p.MemberID),
                    New SqlParameter("@MemberPartnerID", p.MemberPartnerID),
                    New SqlParameter("@CurrencyID", p.CurrencyID),
                    New SqlParameter("@TransactionTypeID", p.TransactionTypeID),
                    New SqlParameter("@TransactionTypeSubID", p.TransactionTypeSubID),
                    New SqlParameter("@DocumentType", PurchaseType),
                    New SqlParameter("@DocumentNo", p.DocumentNo),
                    New SqlParameter("@DocumentDate", p.DocumentDate),
                    New SqlParameter("@DueDate", p.DueDate),
                    New SqlParameter("@PaymentDate", p.PaymentDate),
                    New SqlParameter("@ByID", p.ByID),
                    New SqlParameter("@UserAgent", p.UserAgent),
                    New SqlParameter("@IpAddress", p.IpAddress)
                )

                If p.ItemList IsNot Nothing Then
                    For Each q In p.ItemList
                        db.Database.ExecuteSqlCommand(
                            "begin transaction; " +
                            "declare @TaxPercent decimal(5,2) = 0; " +
                            "select @TaxPercent = TaxPercent from tblTaxObject where TaxObjectID = @TaxObjectID; " +
                            "insert into tblMemberDocumentItem(MemberDocumentID, TaxObjectID, TaxItemType, TaxType, Quantity, UnitPrice, TaxPercent, Description) " +
                            "values(@MemberDocumentID, @TaxObjectID, @TaxItemType, @TaxType, @Quantity, @UnitPrice, @TaxPercent, @Description); " +
                            "commit;",
                            New SqlParameter("@MemberDocumentID", p.MemberDocumentID),
                            New SqlParameter("@TaxObjectID", q.TaxObjectID),
                            New SqlParameter("@TaxItemType", q.TaxItemType),
                            New SqlParameter("@TaxType", q.TaxType),
                            New SqlParameter("@Quantity", q.Quantity),
                            New SqlParameter("@UnitPrice", q.UnitPrice),
                            New SqlParameter("@Description", q.Description)
                        )
                    Next
                End If

                db.Database.ExecuteSqlCommand(
                    "begin transaction; " +
                    "declare @GrandTotalAmount decimal(12,2) = 0, @VatTotalAmount decimal(12,2) = 0, @TotalAmount decimal(12,2) = 0; " +
                    "select @GrandTotalAmount = isnull(sum(SubGrandTotalAmount), 0), @VatTotalAmount = isnull(sum(SubVatTotalAmount), 0), @TotalAmount = isnull(sum(SubTotalAmount), 0) " +
                    "from tblMemberDocumentItem " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "update tblMemberDocument set GrandTotalAmount = @GrandTotalAmount, VatTotalAmount = @VatTotalAmount, TotalAmount = @TotalAmount " +
                    "where MemberDocumentID = @MemberDocumentID; " +
                    "commit;",
                    New SqlParameter("@MemberDocumentID", p.MemberDocumentID)
                )
            End Using

            Return New Status("1")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeletePurchaseRecord(MemberID As Integer, id As Integer) As Status Implements IMemberDocument.DeletePurchaseRecord
        Try
            Using db As New DbContext
                db.Database.ExecuteSqlCommand(
                    "begin transaction; " +
                    "delete from tblMemberDocument where MemberID = @MemberID and MemberDocumentID = @MemberDocumentID and DocumentType = @DocumentType; " +
                    "delete from tblMemberDocument where MemberDocumentID = @MemberDocumentID; " +
                    "commit;",
                    New SqlParameter("@MemberID", MemberID),
                    New SqlParameter("@MemberDocumentID", id),
                    New SqlParameter("@DocumentType", PurchaseType)
                )
            End Using

            Return New Status("1")
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