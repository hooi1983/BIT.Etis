Public Module HtmlHelperExtension
    ' Drop Down List from Business

    <Extension>
    Public Function BuyerPartnerDdl(helper As HtmlHelper, Optional ObjectID As String = "MemberPartnerID", Optional ObjectName As String = "MemberPartnerID",
            Optional MemberID As Integer = 0, Optional Value As Integer = 0, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.MemberPartner
                Dim lst = biz.GetBuyerList(MemberID)

                If lst IsNot Nothing Then
                    For Each p In lst
                        Dim OptionItem = New TagBuilder("option")

                        If p.MemberPartnerID = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.MemberPartnerID)
                        OptionItem.SetInnerText(p.FullName)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Extension>
    Public Function SellerPartnerDdl(helper As HtmlHelper, Optional ObjectID As String = "MemberPartnerID", Optional ObjectName As String = "MemberPartnerID",
            Optional MemberID As Integer = 0, Optional Value As Integer = 0, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.MemberPartner
                Dim lst = biz.GetSellerList(MemberID)

                If lst IsNot Nothing Then
                    For Each p In lst
                        Dim OptionItem = New TagBuilder("option")

                        If p.MemberPartnerID = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.MemberPartnerID)
                        OptionItem.SetInnerText(p.FullName)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Extension>
    Public Function TaxObjectDdl(helper As HtmlHelper, TaxItemType As String, TaxType As String,
            Optional ObjectID As String = "TaxObjectID", Optional ObjectName As String = "TaxObjectID",
            Optional Value As Integer = 0, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.TaxObject
                Dim lst = biz.GetList(TaxItemType, TaxType)

                If lst IsNot Nothing Then
                    For Each p In lst
                        Dim OptionItem = New TagBuilder("option")

                        If p.TaxObjectID = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.TaxObjectID)
                        OptionItem.Attributes.Add("data-taxpercent", p.TaxPercent)
                        OptionItem.SetInnerText(p.TaxObjectName)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Extension>
    Public Function TransactionTypeDdl(helper As HtmlHelper, Optional ObjectID As String = "TransactionTypeID", Optional ObjectName As String = "TransactionTypeID",
            Optional Value As Integer = 0, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.TransactionType
                Dim lst = biz.GetParentList

                If lst IsNot Nothing Then
                    For Each p In lst
                        Dim OptionItem = New TagBuilder("option")

                        If p.TransactionTypeID = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.TransactionTypeID)
                        OptionItem.SetInnerText(p.TransactionTypeName)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Extension>
    Public Function TransactionTypeSubDdl(helper As HtmlHelper, Optional ObjectID As String = "TransactionTypeSubID", Optional ObjectName As String = "TransactionTypeSubID",
            Optional ParentID As Integer = 0, Optional Value As Integer = 0, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.TransactionType
                Dim lst = biz.GetList(ParentID)

                If lst IsNot Nothing Then
                    For Each p In lst
                        Dim OptionItem = New TagBuilder("option")

                        If p.TransactionTypeID = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.TransactionTypeID)
                        OptionItem.SetInnerText(p.TransactionTypeName)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Drop Down List from Lookup

    <Extension>
    Public Function DocumentTypeDdl(helper As HtmlHelper, Optional ObjectID As String = "DocumentType", Optional ObjectName As String = "DocumentType",
            Optional Value As String = Nothing, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.Lookup
                Dim Model = biz.GetRecords(Enums.EnumTable.tblMemberDocument, Enums.EnumField.DocumentType)

                If Model IsNot Nothing Then
                    For Each p In Model.ItemList
                        Dim OptionItem = New TagBuilder("option")

                        If p.LookupKey = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.LookupKey)
                        OptionItem.SetInnerText(p.LookupValue)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Extension>
    Public Function PartnerTypeDdl(helper As HtmlHelper, Optional ObjectID As String = "PartnerType", Optional ObjectName As String = "PartnerType",
            Optional Value As String = Nothing, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.Lookup
                Dim Model = biz.GetRecords(Enums.EnumTable.tblMemberPartner, Enums.EnumField.PartnerType)

                If Model IsNot Nothing Then
                    For Each p In Model.ItemList
                        Dim OptionItem = New TagBuilder("option")

                        If p.LookupKey = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.LookupKey)
                        OptionItem.SetInnerText(p.LookupValue)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Extension>
    Public Function TaxItemTypeDdl(helper As HtmlHelper, Optional ObjectID As String = "TaxItemType", Optional ObjectName As String = "TaxItemType",
            Optional Value As String = Nothing, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.Lookup
                Dim Model = biz.GetRecords(Enums.EnumTable.tblMemberDocumentItem, Enums.EnumField.TaxItemType)

                If Model IsNot Nothing Then
                    For Each p In Model.ItemList
                        Dim OptionItem = New TagBuilder("option")

                        If p.LookupKey = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.LookupKey)
                        OptionItem.SetInnerText(p.LookupValue)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Extension>
    Public Function TaxTypeDdl(helper As HtmlHelper, Optional ObjectID As String = "TaxType", Optional ObjectName As String = "TaxType",
            Optional Value As String = Nothing, Optional ShowDefault As Boolean = False,
            Optional ObjectClass As String = Nothing, Optional ObjectStyle As String = Nothing) As IHtmlString

        Dim Builder = New TagBuilder("select")
        Dim Options = New StringBuilder

        Try
            Builder.GenerateId(ObjectID)
            Builder.Attributes.Add("name", ObjectName)

            If Not String.IsNullOrWhiteSpace(ObjectClass) Then Builder.Attributes.Add("class", ObjectClass)
            If Not String.IsNullOrWhiteSpace(ObjectStyle) Then Builder.Attributes.Add("style", ObjectStyle)

            If ShowDefault Then
                Dim OptionItem = New TagBuilder("option")

                OptionItem.Attributes.Add("value", 0)
                OptionItem.SetInnerText("Please Select")

                Options.Append(OptionItem.ToString(TagRenderMode.Normal))
            End If

            Using biz As New Business.Lookup
                Dim Model = biz.GetRecords(Enums.EnumTable.tblMemberDocumentItem, Enums.EnumField.TaxType)

                If Model IsNot Nothing Then
                    For Each p In Model.ItemList
                        Dim OptionItem = New TagBuilder("option")

                        If p.LookupKey = Value Then OptionItem.Attributes.Add("selected", "selected")

                        OptionItem.Attributes.Add("value", p.LookupKey)
                        OptionItem.SetInnerText(p.LookupValue)

                        Options.Append(OptionItem.ToString(TagRenderMode.Normal))
                    Next
                End If
            End Using

            Builder.InnerHtml = Options.ToString

            Return helper.Raw(Builder.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Radio Button

    <Extension>
    Public Function YesNoRadio(helper As HtmlHelper, Optional ObjectName As String = "TrueFalse", Optional Value As Boolean = False) As IHtmlString
        Dim sb = New StringBuilder

        Try
            Dim ObjectID = String.Format("{0}_Yes", ObjectName)
            Dim Checked = IIf(Value, " checked='checked'", String.Empty)

            sb.AppendFormat(
                "<label class='radio-inline'>" +
                "<input type='radio' id='{0}' name='{1}' value='true' {2} /> Yes" +
                "</label>",
                ObjectID, ObjectName, Checked
            )

            ObjectID = String.Format("{0}_No", ObjectName)
            Checked = IIf(Not Value, " checked='checked'", String.Empty)

            sb.AppendFormat(
                "<label class='radio-inline'>" +
                "<input type='radio' id='{0}' name='{1}' value='false' {2} /> No" +
                "</label>",
                ObjectID, ObjectName, Checked
            )

            Return helper.Raw(sb.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function

End Module