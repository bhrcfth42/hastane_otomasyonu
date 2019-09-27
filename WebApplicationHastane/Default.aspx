<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplicationHastane.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Örnek Hastane</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <link href="https://fonts.googleapis.com/css?family=Poppins:100,200,300,400,500,600,700,800,900" rel="stylesheet" />

    <link rel="stylesheet" href="index/css/open-iconic-bootstrap.min.css" />
    <link rel="stylesheet" href="index/css/animate.css" />

    <link rel="stylesheet" href="index/css/owl.carousel.min.css" />
    <link rel="stylesheet" href="index/css/owl.theme.default.min.css" />
    <link rel="stylesheet" href="index/css/magnific-popup.css" />

    <link rel="stylesheet" href="index/css/aos.css" />

    <link rel="stylesheet" href="index/css/ionicons.min.css" />

    <link rel="stylesheet" href="index/css/flaticon.css" />
    <link rel="stylesheet" href="index/css/icomoon.css" />
    <link rel="stylesheet" href="index/css/style.css" />
</head>
<body>
    <div class="py-1 bg-black top">
        <div class="container">
            <div class="row no-gutters d-flex align-items-start align-items-center px-md-0">
                <div class="col-lg-12 d-block">
                    <div class="row d-flex">
                        <div class="col-md pr-4 d-flex topper align-items-center">
                            <div class="icon mr-2 d-flex justify-content-center align-items-center"><span class="icon-phone2"></span></div>
                            <span class="text">+90 543 451 7298</span>
                        </div>
                        <div class="col-md pr-4 d-flex topper align-items-center">
                            <div class="icon mr-2 d-flex justify-content-center align-items-center"><span class="icon-paper-plane"></span></div>
                            <span class="text">fatihbuhurcu539@gmail.com</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <nav class="navbar navbar-expand-lg navbar-dark ftco_navbar bg-dark ftco-navbar-light site-navbar-target" id="ftco-navbar">
        <div class="container">
            <a class="navbar-brand" href="Default.aspx">
                <img src="login/images/İsimsiz-1.png" width="50" height="50" /><span>   Örnek Hastane</span></a>
            <button class="navbar-toggler js-fh5co-nav-toggle fh5co-nav-toggle" type="button" data-toggle="collapse" data-target="#ftco-nav" aria-controls="ftco-nav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="oi oi-menu"></span>Menü
            </button>

            <div class="collapse navbar-collapse" id="ftco-nav">
                <ul class="navbar-nav nav ml-auto">
                    <li class="nav-item"><a href="#home-section" class="nav-link"><span>Home</span></a></li>
                    <li class="nav-item"><a href="#about-section" class="nav-link"><span>Hakkında</span></a></li>
                    <li class="nav-item"><a href="#department-section" class="nav-link"><span>Bölüm</span></a></li>
                    <li class="nav-item"><a href="#doctor-section" class="nav-link"><span>Doktorlar</span></a></li>
                    <li class="nav-item"><a href="#contact-section" class="nav-link"><span>İletişim</span></a></li>
                    <li class="nav-item cta mr-md-2"><a href="Login.aspx" class="nav-link">Giriş</a></li>
                </ul>
            </div>
        </div>
    </nav>

    <section class="hero-wrap js-fullheight" style="background-image: url('index/images/bg_3.jpg');" data-section="home" data-stellar-background-ratio="0.5">
        <div class="overlay"></div>
        <div class="container">
            <div class="row no-gutters slider-text js-fullheight align-items-center justify-content-start" data-scrollax-parent="true">
                <div class="col-md-6 pt-5 ftco-animate">
                    <div class="mt-5">
                        <span class="subheading">Örnek Hastaneye Hoş Geldiniz</span>
                        <h1 class="mb-4">Geleceğiniz İçin
                                <br />
                            Sağlığınız İçin...</h1>
                        <p class="mb-4">Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove.</p>
                        <p><a href="Hasta Randevu.aspx" class="btn btn-primary py-3 px-4">Randevu Alma</a></p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="ftco-counter img ftco-section ftco-no-pt ftco-no-pb" id="about-section">
        <div class="container">
            <div class="row d-flex">
                <div class="col-md-6 col-lg-5 d-flex">
                    <div class="img d-flex align-self-stretch align-items-center" style="background-image: url(index/images/about.jpg);">
                    </div>
                </div>
                <div class="col-md-6 col-lg-7 pl-lg-5 py-md-5">
                    <div class="py-md-5">
                        <div class="row justify-content-start pb-3">
                            <div class="col-md-12 heading-section ftco-animate p-4 p-lg-5">
                                <h2 class="mb-4">Biz <span>Örnek Hastane </span>Sizin Özel Hastaneniz </h2>
                                <p>Yıllardır size hizmet için burada sizin şehrinizde bulunmaktayız.</p>
                                <p><a href="Hasta Randevu.aspx" class="btn btn-primary py-3 px-4">Randevu Alma</a> <a href="#contact-section" class="btn btn-secondary py-3 px-4">İletişim</a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <section class="ftco-section ftco-no-pt ftco-no-pb ftco-services-2 bg-light">
        <div class="container">
            <div class="row d-flex">
                <div class="col-md-12 py-5">
                    <div class="py-lg-5">
                        <div class="row justify-content-center pb-5">
                            <div class="col-md-12 heading-section ftco-animate">
                                <h2 class="mb-3">Our Services</h2>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 d-flex align-self-stretch ftco-animate">
                                <div class="media block-6 services d-flex">
                                    <div class="icon justify-content-center align-items-center d-flex"><span class="flaticon-ambulance"></span></div>
                                    <div class="media-body pl-md-4">
                                        <h3 class="heading mb-3">Acil Servisler</h3>
                                        <p>Acil müdahaleleri en hızlı şekilde yapılmaktadır.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 d-flex align-self-stretch ftco-animate">
                                <div class="media block-6 services d-flex">
                                    <div class="icon justify-content-center align-items-center d-flex"><span class="flaticon-doctor"></span></div>
                                    <div class="media-body pl-md-4">
                                        <h3 class="heading mb-3">Nitelikli Doktorlar</h3>
                                        <p>Branşında uzman doktorlarımız bulunmaktadır.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 d-flex align-self-stretch ftco-animate">
                                <div class="media block-6 services d-flex">
                                    <div class="icon justify-content-center align-items-center d-flex"><span class="flaticon-stethoscope"></span></div>
                                    <div class="media-body pl-md-4">
                                        <h3 class="heading mb-3">Checkup</h3>
                                        <p>Genel Sağlık kontrolleri geniş kapsamlı olarak yapılabilmektedir.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 d-flex align-self-stretch ftco-animate">
                                <div class="media block-6 services d-flex">
                                    <div class="icon justify-content-center align-items-center d-flex"><span class="flaticon-24-hours"></span></div>
                                    <div class="media-body pl-md-4">
                                        <h3 class="heading mb-3">24 Saat Hizmet</h3>
                                        <p>Kesintisiz hizmet kalitesi sunulmaktadır.</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="ftco-intro img" style="background-image: url(index/images/bg_2.jpg);">
        <div class="overlay"></div>
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-9 text-center">
                    <h2>Sağlığınız Önceliğimizdir</h2>
                    <p>Geleceğiniz için sağlığınız için...</p>
                </div>
            </div>
        </div>
    </section>

    <section class="ftco-section ftco-no-pt ftco-no-pb" id="department-section">
        <div class="container-fluid px-0">
            <div class="row no-gutters">
                <div class="col-md-4 d-flex">
                    <div class="img img-dept align-self-stretch" style="background-image: url(index/images/dept-1.jpg);"></div>
                </div>

                <div class="col-md-8">
                    <div class="row no-gutters">
                        <div class="col-md-4">
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Neroloji</a></h3>
                                </div>
                            </div>
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Genel Cerrahi</a></h3>
                                </div>
                            </div>
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Diş</a></h3>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">İntaniye</a></h3>
                                </div>
                            </div>
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Kardiyoloji</a></h3>
                                </div>
                            </div>
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Psikiyatri</a></h3>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Üroloji</a></h3>
                                </div>
                            </div>
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Pediatri</a></h3>
                                </div>
                            </div>
                            <div class="department-wrap p-4 ftco-animate">
                                <div class="text p-2 text-center">
                                    <div class="icon">
                                        <span class="flaticon-stethoscope"></span>
                                    </div>
                                    <h3><a href="#">Dahiliye</a></h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="ftco-section" id="doctor-section">
        <div class="container-fluid px-5">
            <div class="row justify-content-center mb-5 pb-2">
                <div class="col-md-8 text-center heading-section ftco-animate">
                    <h2 class="mb-4">Nitelikli Doktorlarımız</h2>
                    <p>Uzmanlık alanlarında başarılar elde etmiş doktorlarımız</p>
                </div>
            </div>
            <div class="row" style="text-align: center">
                <div class="col-md-6 col-lg-3 ftco-animate">
                    <div class="staff">
                        <div class="img-wrap d-flex align-items-stretch">
                            <div class="img align-self-stretch" style="background-image: url(index/images/doc-1.jpg);"></div>
                        </div>
                        <div class="text pt-3 text-center">
                            <h3 class="mb-2">Dr. Önder Fatih Buhurcu</h3>
                            <span class="position mb-2">Kardiyolog</span>
                            <div class="faded">
                                <ul class="ftco-social text-center">
                                    <li class="ftco-animate"><a href="https://www.instagram.com/onderfatihbuhurcu"><span class="icon-instagram"></span></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3 ftco-animate">
                    <div class="staff">
                        <div class="img-wrap d-flex align-items-stretch">
                            <div class="img align-self-stretch" style="background-image: url(index/images/doc-2.jpg);"></div>
                        </div>
                        <div class="text pt-3 text-center">
                            <h3 class="mb-2">Dr. Selahattin Altuntaş</h3>
                            <span class="position mb-2">Dişçi</span>
                            <div class="faded">
                                <ul class="ftco-social text-center">
                                    <li class="ftco-animate"><a href="https://www.instagram.com/selahattinaltuntass"><span class="icon-instagram"></span></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="ftco-facts img ftco-counter" style="background-image: url(index/images/bg_3.jpg);">
        <div class="overlay"></div>
        <div class="container">
            <div class="row d-flex align-items-center">
                <div class="col-md-5 heading-section heading-section-white">
                    <h2 class="mb-4">1.200'den fazla hasta bize güveniyor</h2>
                    <p class="mb-0"><a href="Hasta Randevu.aspx" class="btn btn-secondary px-4 py-3">Randevu Al</a></p>
                </div>
                <div class="col-md-7">
                    <div class="row pt-4">
                        <div class="col-md-6 d-flex justify-content-center counter-wrap ftco-animate">
                            <div class="block-18">
                                <div class="text">
                                    <strong class="number" data-number="3">0</strong>
                                    <span>Deneyimli Yıllar</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 d-flex justify-content-center counter-wrap ftco-animate">
                            <div class="block-18">
                                <div class="text">
                                    <strong class="number" data-number="3000">0</strong>
                                    <span>Hastalar</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 d-flex justify-content-center counter-wrap ftco-animate">
                            <div class="block-18">
                                <div class="text">
                                    <strong class="number" data-number="24">0</strong>
                                    <span>Doktorlar</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 d-flex justify-content-center counter-wrap ftco-animate">
                            <div class="block-18">
                                <div class="text">
                                    <strong class="number" data-number="128">0</strong>
                                    <span>Çalışan</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="ftco-section contact-section" id="contact-section">
        <div class="container">
            <div class="row justify-content-center mb-5 pb-3">
                <div class="col-md-7 heading-section text-center ftco-animate">
                    <h2 class="mb-4">İletişim</h2>
                    <p>Görüş, öneri ve şikayetlerinizi bize iletebilirsiniz</p>
                </div>
            </div>
            <div class="row d-flex contact-info mb-5">
                <div class="col-md-6 col-lg-4 d-flex ftco-animate">
                    <div class="align-self-stretch box p-4 text-center bg-light">
                        <div class="icon d-flex align-items-center justify-content-center">
                            <span class="icon-map-signs"></span>
                        </div>
                        <h3 class="mb-4">Adres</h3>
                        <p>Bosna Hersek Mahallesi Sümeyye Caddesi Hizmet Sitesi 41/3 Selçuklu/KONYA</p>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4 d-flex ftco-animate">
                    <div class="align-self-stretch box p-4 text-center bg-light">
                        <div class="icon d-flex align-items-center justify-content-center">
                            <span class="icon-phone2"></span>
                        </div>
                        <h3 class="mb-4">İletişim Numarası</h3>
                        <p><a href="tel://+905434517298">+90 543 451 7298</a></p>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4 d-flex ftco-animate">
                    <div class="align-self-stretch box p-4 text-center bg-light">
                        <div class="icon d-flex align-items-center justify-content-center">
                            <span class="icon-paper-plane"></span>
                        </div>
                        <h3 class="mb-4">Mail Adresi</h3>
                        <p><a href="mailto:fatihbuhurcu539@gmail.com">fatihbuhurcu539@gmail.com</a></p>
                    </div>
                </div>
            </div>
            
            <div class="row no-gutters block-9">
                <div class="col-md-12 order-md-last d-flex">
                    <form id="form1" runat="server" class="bg-light p-5 contact-form">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="form-group">
                            <input id="isim" runat="server" type="text" class="form-control" placeholder="Adınız ve Soyadınız" />
                        </div>
                        <div class="form-group">
                            <input id="email" runat="server" type="text" class="form-control" placeholder="Email Adresi" />
                        </div>
                        <div class="form-group">
                            <input type="tel" id="phoneNumber" runat="server" placeholder="Telefon Numarası" class="form-control" />
                        </div>
                        <div class="form-group">
                            <input id="konu" runat="server" type="text" class="form-control" placeholder="Konu" />
                        </div>
                        <div class="form-group">
                            <textarea name="" id="mesaj" runat="server" cols="30" rows="7" class="form-control" placeholder="Mesajınız"></textarea>
                        </div>
                        <div class="form-group" style="text-align: center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="gönder" runat="server" Text="Mesaj Gönder" class="btn btn-secondary py-3 px-5" OnClick="gönder_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gönder" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </section>

    <footer class="ftco-footer ftco-section img" style="background-image: url(index/images/footer-bg.jpg);">
        <div class="overlay"></div>
        <div class="container-fluid px-md-5">
            <div class="row">
                <div class="col-md-12 text-center">
                    <p>
                        Copyright &copy; 2019 Designed by Önder
                    </p>
                </div>
            </div>
        </div>
    </footer>



    <!-- loader -->
    <div id="ftco-loader" class="show fullscreen">
        <svg class="circular" width="48px" height="48px">
            <circle class="path-bg" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke="#eeeeee" />
            <circle class="path" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke-miterlimit="10" stroke="#F96D00" />
        </svg>
    </div>


    <script src="index/js/jquery.min.js"></script>
    <script src="index/js/jquery-migrate-3.0.1.min.js"></script>
    <script src="index/js/popper.min.js"></script>
    <script src="index/js/bootstrap.min.js"></script>
    <script src="index/js/jquery.easing.1.3.js"></script>
    <script src="index/js/jquery.waypoints.min.js"></script>
    <script src="index/js/jquery.stellar.min.js"></script>
    <script src="index/js/owl.carousel.min.js"></script>
    <script src="index/js/jquery.magnific-popup.min.js"></script>
    <script src="index/js/aos.js"></script>
    <script src="index/js/jquery.animateNumber.min.js"></script>
    <script src="index/js/scrollax.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBVWaKrjvy3MaE7SQ74_uJiULgl1JY0H2s&sensor=false"></script>
    <script src="index/js/google-map.js"></script>

    <script src="index/js/main.js"></script>
</body>
</html>
