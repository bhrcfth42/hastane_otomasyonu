<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hasta Randevu Paneli.aspx.cs" Inherits="WebApplicationHastane.Hasta_Randevu_Paneli" %>

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
            <h2>Randevu Alma</h2>
            <div class="form-group">
                <label for="Tc" class="col-sm-3 control-label">TC Kimlik No</label>
                <div class="col-sm-9">
                    <asp:Label ID="tc_no" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label for="firstName" class="col-sm-3 control-label">Adınız</label>
                <div class="col-sm-9">
                    <asp:Label ID="adi" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label for="lastName" class="col-sm-3 control-label">Soyadınız</label>
                <div class="col-sm-9">
                    <asp:Label ID="soyadi" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label for="birthDate" class="col-sm-3 control-label">Doğum Yılı</label>
                <div class="col-sm-9">
                    <asp:Label ID="dogumyili" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label for="phoneNumber" class="col-sm-3 control-label">Telefon Numarası</label>
                <div class="col-sm-9">
                    <asp:Label ID="telno" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label for="tarih" class="col-sm-3 control-label">Poliklinik</label>
                <div class="col-sm-9">
                    <asp:DropDownList ID="ddldoktorbölüm" runat="server" CssClass="form-control btn-primary" DataTextField="bölüm" AutoPostBack="True" OnSelectedIndexChanged="ddldoktorbölüm_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label for="tarih" class="col-sm-3 control-label">Doktor</label>
                <div class="col-sm-9">
                    <asp:DropDownList ID="ddldoktorad" runat="server" CssClass="form-control btn-primary" DataTextField="AdSoyad" DataValueField="Id" OnSelectedIndexChanged="ddldoktorad_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label for="tarih" class="col-sm-3 control-label">Randevu Tarihi</label>
                <div class="col-sm-9">
                    <asp:DropDownList ID="ddltarih" runat="server" CssClass="form-control btn btn-primary" AutoPostBack="True" OnSelectedIndexChanged="ddltarih_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label for="tarih" class="col-sm-3 control-label">Randevu Saati</label>
                <div class="col-sm-9">
                    <asp:DropDownList ID="ddlsaat" runat="server" CssClass="form-control btn-primary" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- /.form-group -->
                    <div style="text-align: center">
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
