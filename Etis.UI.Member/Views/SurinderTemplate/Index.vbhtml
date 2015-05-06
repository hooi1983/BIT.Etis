@Code
    Layout = "~/Views/Shared/_LayoutSurinder.vbhtml"
End Code

<div id="content" class="container">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3">
            <div class="well no-padding">
                @Html.Partial("LoginPartial")
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-8 col-lg-9 hidden-xs hidden-sm">
            <h3 class="txt-color-red login-header-big">Welcome to the Directorate General of Taxes' Official On-line VAT Invoicing and Reporting Portal</h3>
            <div class="hero">
                <div class="pull-left login-desc-box-l">
                    <h4 class="paragraph-header">Please login to begin using the system or register as a new user today</h4>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-sm-12">
                    <p>
                        Our site was designed to simplify all of your VAT requirements:<br />
                        recording and reporting of all of your VAT transactions
                    </p>
                </div>
            </div>

            <div style="margin-top:20px;"></div>

            <div class="row">
                <div class="col-md-4 add-login">
                    <h1><span>Online Invoicing</span></h1>
                    <ul>
                        <li>Create and receive official VAT tax invoices</li>
                    </ul>
                </div>

                <div class="col-md-4 add-login">
                    <h1><span>VAT Filing</span></h1>
                    <ul>
                        <li>File your VAT online</li>
                        <li>Automatically generate your VAT filing based on all of your VAT invoice transactions</li>
                    </ul>
                </div>

                <div class="col-md-4 add-login">
                    <h1><span>VAT Payment</span></h1>
                    <ul>
                        <li>Pay your VAT online today</li>
                    </ul>
                </div>
            </div>

            <div style="margin-top:20px;"></div>

            <div class="well">
                <div class="row">
                    <div class="col-md-12"><h4><strong>Upcoming Key Dates</strong></h4></div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <strong>Current Filing Period</strong><br />
                        [May 2015]
                    </div>
                    <div class="col-md-4">
                        <strong>Filing Deadline</strong><br />
                        [7 June 2015]<br />
                        [X days remaining]
                    </div>
                    <div class="col-md-4">
                        <strong>Payment Deadline</strong><br />
                        [7 June 2015]<br />
                        [X days remaining]
                    </div>
                </div>
            </div>
            
            <div style="margin-top:20px;"></div>

            <div class="row">
                <div class="col-md-6 add-login">
                    <h1><span>Contact Us</span></h1>
                    <ul>
                        <li>
                            <span class="glyphicon glyphicon-map-marker login-address" aria-hidden="true"></span>
                            <address>
                                The Directorate of Counseling,<br />
                                Services and Public Relations<br />
                                Main Building, 16th floor<br />
                                Office of Directorate General of Taxes<br />
                                Jalan Gatot Subroto, Lot 40-42, Jakarta 12190<br />
                                PO Box 124
                            </address>
                        </li>
                        <li><span class="glyphicon glyphicon-envelope mail-login" aria-hidden="true"></span><address><a href="mailto:#">pengaduan@pajak.go.id</a></address></li>
                        <li><span class="glyphicon glyphicon-phone-alt login-phone" aria-hidden="true"></span> <address>+ (62) 21-5250208, 5251509</address></li>
                        <li><span class="glyphicon glyphicon-phone-alt login-phone" aria-hidden="true"></span> <address>+ (62) 21- 584 792</address></li>
                    </ul>
                </div>
                <div class="col-md-6 add-help">
                    <h1><span>Help</span></h1>
                    <ul>
                        <li><i class="glyphicon glyphicon-envelope mail-login" aria-hidden="true"></i><span>Email</span><address><a href="mailto:#">email@address.com</a></address></li>

                        <li><i class="glyphicon glyphicon-phone-alt login-phone" aria-hidden="true"></i><span>Hotline</span> <address>800 123 4567</address></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>