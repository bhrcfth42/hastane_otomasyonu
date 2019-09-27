<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hasta Randevu.aspx.cs" Inherits="WebApplicationHastane.Hasta_Randevu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!------ Include the above in your HEAD tag ---------->

    <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-2.1.3.min.js"></script>
    <!------ Include the above in your HEAD tag ---------->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <style type="text/css">
        body {
            background: url('https://static-communitytable.parade.com/wp-content/uploads/2014/03/rethink-target-heart-rate-number-ftr.jpg') fixed;
            background-size: cover;
        }

        *[role="form"] {
            max-width: 530px;
            padding: 15px;
            margin: 0 auto;
            border-radius: 0.3em;
            background-color: #f2f2f2;
        }

            *[role="form"] h2 {
                font-family: 'Open Sans', sans-serif;
                font-size: 40px;
                font-weight: 600;
                color: #000000;
                margin-top: 5%;
                text-align: center;
                text-transform: uppercase;
                letter-spacing: 4px;
            }
    </style>
    <title>Örnek Hastane</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
</head>
<body>
    <div class="container">
        <form class="form-horizontal" role="form" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <h2>Randevu SİSTEMİ GİRİŞ</h2>
            <div class="form-group">
                <label for="Tc" class="col-sm-3 control-label">TC Kimlik No</label>
                <div class="col-sm-9">
                    <input id="tckimlikno" runat="server" type="text" placeholder="TC Kimlik Numarası" class="form-control" maxlength="11" />
                </div>
            </div>
            <div class="form-group">
                <label for="firstName" class="col-sm-3 control-label">Adınız</label>
                <div class="col-sm-9">
                    <input type="text" id="Ad" runat="server" placeholder="Adınız" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label for="lastName" class="col-sm-3 control-label">Soyadınız</label>
                <div class="col-sm-9">
                    <input type="text" id="Soyad" runat="server" placeholder="Soyadınız" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label for="birthDate" class="col-sm-3 control-label">Doğum Yılı</label>
                <div class="col-sm-9">
                    <input type="number" id="birthDate" runat="server" class="form-control" maxlength="4" />
                </div>
            </div>
            <div class="form-group">
                <label for="phoneNumber" class="col-sm-3 control-label">Telefon Numarası</label>
                <div class="col-sm-9">
                    <input type="tel" id="phoneNumber" runat="server" placeholder="Telefon Numarası" class="form-control" />
                    <span class="help-block">Size ulaşabilmemiz için tüm bilgiler önemlidir.</span>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- /.form-group -->
                    <div style="text-align:center">
                        <asp:Button ID="Button1" runat="server" Text="Randevu Alma" class="btn btn-success" OnClick="Button1_Click" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </form>
        <!-- /form -->
    </div>
    <!-- ./container -->
    <script>

</script>
</body>
</html>
