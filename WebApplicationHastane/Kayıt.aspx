<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kayıt.aspx.cs" Inherits="WebApplicationHastane.Kayıt" %>

<!DOCTYPE html>

<html lang="en">
<head>
	<title>Örnek Hastane</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
<!--===============================================================================================-->	
	<link rel="icon" type="image/png" href="login/images/icons/favicon.ico"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="login/fonts/font-awesome-4.7.0/css/font-awesome.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="login/fonts/Linearicons-Free-v1.0.0/icon-font.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="login/vendor/animate/animate.css"/>
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="login/vendor/css-hamburgers/hamburgers.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="login/vendor/animsition/css/animsition.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="login/vendor/select2/select2.min.css"/>
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="login/vendor/daterangepicker/daterangepicker.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="login/css/util.css"/>
	<link rel="stylesheet" type="text/css" href="login/css/main.css"/>
<!--===============================================================================================-->
    <style type="text/css">
footer { position: absolute; bottom: 0; width: 100%; height: 60px; }
</style>
</head>
<body>
	
	<div class="limiter">
		<div class="container-login100" style="background-image: url('login/images/bg-01.jpg');">
			<div class="wrap-login100 p-t-30 p-b-50">
				<span class="login100-form-title p-b-41">
					Kullanıcı Kayıt
				</span>
				<form id="form1" runat="server" class="login100-form validate-form p-b-33 p-t-5" onsubmit="return isValid(this)">

                    <div class="wrap-input100 validate-input" data-validate = "Adı">
						<input id="textadi" runat="server" class="input100" type="text" name="name" placeholder="Adı">
						<span class="focus-input100" data-placeholder="&#xe82a;"></span>
					</div>

                    <div class="wrap-input100 validate-input" data-validate = "Soyadı">
						<input id="textsoyadi" runat="server" class="input100" type="text" name="surname" placeholder="Soyadı">
						<span class="focus-input100" data-placeholder="&#xe82a;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate = "Kullanıcı Adı">
						<input id="textuser" runat="server" class="input100" type="text" name="username" placeholder="Kullanıcı Adı">
						<span class="focus-input100" data-placeholder="&#xe82a;"></span>
					</div>

                    <div class="wrap-input100 validate-input" data-validate = "Yetki">						
                        <asp:DropDownList ID="ddlyetki" runat="server" CssClass="input100" Font-Names="Yetki">
                            <asp:ListItem>Doktor</asp:ListItem>
                            <asp:ListItem>Hemşire</asp:ListItem>
                            <asp:ListItem>Eczacı</asp:ListItem>
                            <asp:ListItem>Laborant</asp:ListItem>
                            <asp:ListItem>Radyoloji</asp:ListItem>
                            <asp:ListItem>Admin</asp:ListItem>
                        </asp:DropDownList>
						<span class="focus-input100" data-placeholder="&#xe82a;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Parola">
						<input id="textpassword" runat="server" class="input100" type="password" name="pass" placeholder="Parola">
						<span class="focus-input100" data-placeholder="&#xe80f;"></span>
					</div>

                    <div class="wrap-input100 validate-input" data-validate="Parola Doğrulama">
						<input id="textpassword2" runat="server" class="input100" type="password" name="confirm_password" placeholder="Parola Tekrar">
						<span class="focus-input100" data-placeholder="&#xe80f;"></span>
					</div>

					<div class="container-login100-form-btn m-t-32">
                        <asp:Button ID="loginbutton" class="login100-form-btn" runat="server" Text="Kayıt Yap" OnClick="loginbutton_Click"/>
					</div>

				</form>
			</div>
            <!-- Page footer -->
                <footer class="row">
                    <p class="col-12 text-white text-center tm-copyright-text">
                        Copyright &copy; 2019 Designed by Önder
                    </p>
                </footer>
		</div>
	</div>
	

	<div id="dropDownSelect1"></div>
	
<!--===============================================================================================-->
	<script src="login/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="login/vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script src="login/vendor/bootstrap/js/popper.js"></script>
	<script src="login/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="login/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="login/vendor/daterangepicker/moment.min.js"></script>
	<script src="login/vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script src="login/vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script src="login/js/main.js"></script>
    <script type="text/javascript">
function isValid(frm)
{
    var pass = frm.pass.value;
    var confirm_password = frm.confirm_password.value;
    
    if (!(pass == confirm_password) )
    {
        alert("Şifreler eşleşmiyor");
        return false;
    }
    return true;
}
</script>

</body>
</html>
