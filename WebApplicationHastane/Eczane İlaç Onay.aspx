<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Eczane İlaç Onay.aspx.cs" Inherits="WebApplicationHastane.Eczane_İlaç_Onay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <title></title>

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:400,600" />
    <link rel="stylesheet" href="login/css/all.min.css" />
    <link rel="stylesheet" href="login/css/bootstrap.min.css" />
    <link rel="stylesheet" href="login/css/templatemo-style.css" />
    <link href="login/css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form2" runat="server">
        <div class="parallax-window" data-parallax="scroll" data-image-src="login/images/SOA-Hospital-Car-Park.jpg">
            <div class="container-fluid">
                <div class="row tm-brand-row">
                    <div class="col-lg-4 col-11">
                        <div class="tm-brand-container tm-bg-white-transparent">
                            <a class="navbar-brand" href="AnaSayfa.aspx">
                                <img src="login/images/İsimsiz-1.png" width="200" height="200" />
                            </a>
                            <button class="navbar-toggler js-fh5co-nav-toggle fh5co-nav-toggle" type="button" data-toggle="collapse" data-target="#ftco-nav" aria-controls="ftco-nav" aria-expanded="false" aria-label="Toggle navigation">
                            </button>
                            <div class="tm-brand-texts">
                                <h1 class="text-uppercase tm-brand-name">Örnek Hastane</h1>
                                <p class="small">Geleceğiniz İçin Sağlığınız İçin...</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-8 col-1">
                        <div class="tm-nav">
                            <nav
                                class="navbar navbar-expand-lg navbar-light tm-bg-white-transparent tm-navbar">
                                <button
                                    class="navbar-toggler"
                                    type="button"
                                    data-toggle="collapse"
                                    data-target="#navbarNav"
                                    aria-controls="navbarNav"
                                    aria-expanded="false"
                                    aria-label="Toggle navigation">
                                    <span class="navbar-toggler-icon"></span>
                                </button>
                                <div class="collapse navbar-collapse" id="navbarNav">
                                    <ul class="navbar-nav">
                                        <li class="nav-item">                                            
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Underline="True"></asp:Label>
                                        </li>                                        
                                        <li class="nav-item">
                                            <div class="tm-nav-link-highlight"></div>
                                            <a class="nav-link" href="Login.aspx">Çıkış Yap</a>
                                        </li>
                                    </ul>
                                </div>
                            </nav>
                        </div>
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <!-- Services header -->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <section class="row">
                            <div class="col-12">
                                <div class="parallax-window tm-services-parallax-header"
                                    data-parallax="scroll"
                                    data-z-index="101"
                                    data-image-src="login/images/In-c-unıt.jpg">
                                    <div class="tm-bg-black-transparent text-center tm-services-header">
                                        <h2 class="text-uppercase tm-services-page-title">ECZANE</h2>
                                        <p class="tm-services-description mb-0">
                                            Hasta tedavi onay işlemi yapılmaktadır.
                                        </p>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="tm-bg-black-transparent tm-services-detail-box">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Hasta Seçiniz</span>
                                                </div>
                                                <asp:DropDownList ID="ddlHasta" runat="server" CssClass="btn btn-primary form-control" DataValueField="Id" DataTextField="AdSoyad" AutoPostBack="True" OnSelectedIndexChanged="ddlHasta_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btnonayla" runat="server" Text="Onayla" class="btn btn-success form-control" OnClick="btnonayla_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="tm-bg-black-transparent tm-services-detail-box">
                                    <div class="panel-group">
                                        <div class="panel panel-default">
                                            <div class="panel-body">İlaç Listesi</div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <asp:ListBox ID="ListBoxTedavi" runat="server" CssClass="form-control" DataTextField="ilac"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12" style="text-align: center">
                                <footer class="row">
                                    <p class="col-12 text-white text-center tm-copyright-text">
                                        Copyright &copy; 2019 Designed by Önder
                                    </p>
                                </footer>
                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlHasta" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnonayla" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <!-- Page footer -->

            </div>
            <!-- .container-fluid -->
        </div>

        <script src="login/js/jquery.min.js"></script>
        <script src="login/js/parallax.min.js"></script>
        <script src="login/js/bootstrap.min.js"></script>
        <script>
            $(function () {
                $('.tabgroup > div').hide();
                $('.tabgroup > div:first-of-type').show();
                $('.tabs a').click(function (e) {
                    e.preventDefault();
                    var $this = $(this),
                        tabgroup = '#' + $this.parents('.tabs').data('tabgroup'),
                        others = $this.closest('li').siblings().children('a'),
                        target = $this.attr('href');
                    others.removeClass('active');
                    $this.addClass('active');
                    $(tabgroup).children('div').hide();
                    $(target).show();

                    // Scroll to tab content (for mobile)
                    if ($(window).width() < 992) {
                        $('html, body').animate({
                            scrollTop: $("#first-tab-group").offset().top
                        }, 200);
                    }
                })
            });

        </script>
    </form>
</body>
</html>
