<nav class="text-center">
    <ul class="pagination">
        <li class="@(If(Model.CurrentPage = 1, "disabled", ""))">
            <a href="#" aria-label="First" data-page="1" data-search="@Model.Search">
                <span aria-hidden="true">First</span>
            </a>
        </li>
        <li class="@(If(Model.CurrentPage = 1, "disabled", ""))">
            <a href="#" aria-label="Previous" data-page="@(If(Model.CurrentPage = 1, 1, Model.CurrentPage - 1))" data-search="@Model.Search">
                <span aria-hidden="true">Previous</span>
            </a>
        </li>

        @If Model.PageCount > 11 Then
            @If Model.CurrentPage <= 6 Then
                For i = 1 To 11
                    @<li class="@(If(i = Model.CurrentPage, "active", ""))">
                         <a href="#" data-page="@i" data-search="@Model.Search">@i</a>
                    </li>
                Next
            ElseIf Model.PageCount - Model.CurrentPage <= 5 Then
                For i = Model.PageCount - 10 To Model.PageCount
                    @<li class="@(If(i = Model.CurrentPage, "active", ""))">
                         <a href="#" data-page="@i" data-search="@Model.Search">@i</a>
                    </li>
                Next
            Else
                For i = Model.CurrentPage - 5 To Model.CurrentPage + 5
                    @<li class="@(If(i = Model.CurrentPage, "active", ""))">
                         <a href="#" data-page="@i" data-search="@Model.Search">@i</a>
                    </li>
                Next
            End If
        Else
            For i = 1 To Model.PageCount
                @<li class="@(If(i = Model.CurrentPage, "active", ""))">
                     <a href="#" data-page="@i" data-search="@Model.Search">@i</a>
                </li>
            Next
        End If

        <li class="@(If(Model.CurrentPage = Model.PageCount, "disabled", ""))">
            <a href="#" aria-label="Next" data-page="@(If(Model.CurrentPage = Model.PageCount, Model.PageCount, Model.CurrentPage + 1))" data-search="@Model.Search">
                <span aria-hidden="true">Next</span>
            </a>
        </li>
        <li class="@(If(Model.CurrentPage = Model.PageCount, "disabled", ""))">
            <a href="#" aria-label="Last" data-page="@(Model.PageCount)" data-search="@Model.Search">
                <span aria-hidden="true">Last</span>
            </a>
        </li>
    </ul>
</nav>