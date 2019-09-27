<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnaSayfa.aspx.cs" Inherits="WebApplicationHastane.AnaSayfa" %>

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
                        <section class="row" id="a">
                            <div class="col-12">
                                <div class="parallax-window tm-services-parallax-header"
                                    data-parallax="scroll"
                                    data-z-index="101"
                                    data-image-src="login/images/In-c-unıt.jpg">
                                    <div class="tm-bg-black-transparent text-center tm-services-header">
                                        <h2 class="text-uppercase tm-services-page-title">Doktorun Yetkisi Bulunan Sayfalar</h2>
                                        <br />
                                        <div class="btn-group col-12">
                                            <asp:Button ID="hastayatisislemleri" type="button" class="btn btn-primary form-control" runat="server" Text="Hasta Yatış İşlemleri" OnClick="hastayatisislemleri_Click"></asp:Button>
                                            <asp:Button ID="kantahlilistek" type="button" class="btn btn-primary form-control" runat="server" Text="Hasta Kan Tahlil İsteği" OnClick="kantahlilistek_Click"></asp:Button>
                                            <asp:Button ID="radyolojiistek" type="button" class="btn btn-primary form-control" runat="server" Text="Radyoloji Tetkik İsteği" OnClick="radyolojiistek_Click"></asp:Button>
                                            <asp:Button ID="tedaviyazdir" type="button" class="btn btn-primary form-control" runat="server" Text="Tedavi Kaydet" OnClick="tedaviyazdir_Click"></asp:Button>
                                            <asp:Button ID="randevuhasta" type="button" class="btn btn-primary form-control" runat="server" Text="Poliklinik Randevulu Hasta" OnClick="randevuhasta_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <section class="row" id="b">
                            <div class="col-12">
                                <div class="parallax-window tm-services-parallax-header"
                                    data-parallax="scroll"
                                    data-z-index="101"
                                    data-image-src="login/images/ECZANE2.jpg">
                                    <div class="tm-bg-black-transparent text-center tm-services-header">
                                        <h2 class="text-uppercase tm-services-page-title">Hemşire Yetkisi Bulunan Sayfalar</h2>
                                        <br />
                                        <div class="btn-group col-12">
                                            <asp:Button ID="servishastalisteleme" type="button" class="btn btn-primary form-control" runat="server" Text="Servis Hasta Listesi" OnClick="servishastalisteleme_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <div class="col-12">
                            <div class="parallax-window tm-services-parallax-header"
                                data-parallax="scroll"
                                data-z-index="101"
                                data-image-src="login/images/teknolojimiz.jpg">
                                <div class="tm-bg-black-transparent text-center tm-services-header">
                                    <h2 class="text-uppercase tm-services-page-title">Eczacının Yetkisi Bulunan Sayfalar</h2>
                                    <br />
                                    <div class="btn-group col-12">
                                        <asp:Button ID="eczadepo" type="button" class="btn btn-primary form-control" runat="server" Text="Ecza Depo" OnClick="ilacdepo_Click"></asp:Button>
                                        <asp:Button ID="ilackaydet" type="button" class="btn btn-primary form-control" runat="server" Text="İlaç Kaydet" OnClick="ilackaydet_Click"></asp:Button>
                                        <asp:Button ID="eczaneilaconay" type="button" class="btn btn-primary form-control" runat="server" Text="Tedavi Onay" OnClick="eczaneilaconay_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="parallax-window tm-services-parallax-header"
                                data-parallax="scroll"
                                data-z-index="101"
                                data-image-src="login/images/teknolojimiz.jpg">
                                <div class="tm-bg-black-transparent text-center tm-services-header">
                                    <h2 class="text-uppercase tm-services-page-title">Laborant Yetkisi Bulunan Sayfalar</h2>
                                    <br />
                                    <div class="btn-group col-12">
                                        <asp:Button ID="kantahlilsonuc" type="button" class="btn btn-primary form-control" runat="server" Text="Kan Tahlil Sonuç Girişi" OnClick="kantahlilsonuc_Click"></asp:Button>
                                        <asp:Button ID="kantahliltetkikkayıt" type="button" class="btn btn-primary form-control" runat="server" Text="Kan Tahlil Tetkik Yeni Kayıt" OnClick="kantahliltetkikkayıt_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="parallax-window tm-services-parallax-header"
                                data-parallax="scroll"
                                data-z-index="101"
                                data-image-src="login/images/teknolojimiz.jpg">
                                <div class="tm-bg-black-transparent text-center tm-services-header">
                                    <h2 class="text-uppercase tm-services-page-title">Radyoloji Yetkisi Bulunan Sayfalar</h2>
                                    <br />
                                    <div class="btn-group col-12">
                                        <asp:Button ID="radyolojitetkikkayıt" type="button" class="btn btn-primary form-control" runat="server" Text="Radyoloji Tetkik Yeni Kayıt" OnClick="radyolojitetkikkayıt_Click"></asp:Button>
                                        <asp:Button ID="radyolojisonucgirisi" type="button" class="btn btn-primary form-control" runat="server" Text="Radyoloji Tetkik Sonuç Girişi" OnClick="radyolojisonucgirisi_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="parallax-window tm-services-parallax-header"
                                data-parallax="scroll"
                                data-z-index="101"
                                data-image-src="login/images/teknolojimiz.jpg">
                                <div class="tm-bg-black-transparent text-center tm-services-header">
                                    <h2 class="text-uppercase tm-services-page-title">Sekreter Yetkisi Bulunan Sayfalar</h2>
                                    <br />
                                    <div class="btn-group col-12">
                                        <asp:Button ID="Button1" type="button" class="btn btn-primary form-control" runat="server" Text="Hasta Yatış İşlemleri" OnClick="hastayatisislemleri_Click"></asp:Button>
                                        <asp:Button ID="hastakayit" type="button" class="btn btn-primary form-control" runat="server" Text="Hasta Kayıt" OnClick="hastakayit_Click"></asp:Button>
                                        <asp:Button ID="hastabilgigüncelle" type="button" class="btn btn-primary form-control" runat="server" Text="Hasta Bilgi Güncelle" OnClick="hastabilgigüncelle_Click"></asp:Button>
                                        <asp:Button ID="hastatransfer" type="button" class="btn btn-primary form-control" runat="server" Text="Hasta Transfer İşlemi" OnClick="hastatrasnfer_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="parallax-window tm-services-parallax-header"
                                data-parallax="scroll"
                                data-z-index="101"
                                data-image-src="login/images/teknolojimiz.jpg">
                                <div class="tm-bg-black-transparent text-center tm-services-header">
                                    <h2 class="text-uppercase tm-services-page-title">Diğer İşlemler</h2>
                                    <br />
                                    <div class="btn-group col-12">
                                        <asp:Button ID="doktorekle" type="button" class="btn btn-primary form-control" runat="server" Text="Doktor Ekle" OnClick="doktorekle_Click"></asp:Button>
                                        <asp:Button ID="servisekle" type="button" class="btn btn-primary form-control" runat="server" Text="Servis Ekle" OnClick="servisekle_Click"></asp:Button>
                                        <asp:Button ID="doktorbilgigüncelle" type="button" class="btn btn-primary form-control" runat="server" Text="Doktor Bilgi Güncelle" OnClick="doktorekle_Click"></asp:Button>
                                        <asp:Button ID="gönderilmismesajlar" type="button" class="btn btn-primary form-control" runat="server" Text="Gönderilen Mesajlar" OnClick="gönderilmismesajlar_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <footer class="row">
                            <p class="col-12 text-white text-center tm-copyright-text">
                                Copyright &copy; 2019 Designed by Önder
                            </p>
                        </footer>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tedaviyazdir" EventName="Click" />
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
