<%@ Page Title="How are you today?" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HRUTWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="Scripts/HRUT/previewImage.js"></script>
    <script type="text/javascript">
        function getImagePreviewer() {
            return '#<%=ImagePreviewer.ClientID%>';
        }
        function MakeAlert(msg) {
            $('#AlertMessagePlaceholder').html('<p>' + msg + '</p>');
            document.getElementById("AlertPanel").style.height = "100%";
        }
        function DismissAlert() {
            document.getElementById("AlertPanel").style.height = "0%";
        }
        function SaveImage() {
            var dimg = document.createElement('a');
            dimg.href = $('#<%=HandledImage.ClientID%>').prop('src');
            dimg.download = 'HRUT' + '<%=DateTime.Now.ToString()%>' + '.jpg';
            dimg.click();
        }
        function ShowLoading() {
            if ($('#<%=ImageUploader.ClientID%>').prop('value') == "") {
                MakeAlert("咦自拍呢?");
                return;
            }
            document.getElementById("UploadingPanel").style.height = "100%";
        }
    </script>

    <style>
        .uploader {
            opacity: 0;
            position: absolute;
            z-index: -1;
        }

        .overlay {
            height: 0;
            width: 100%;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0, 0.8);
            overflow: hidden;
            transition: 0.5s;
        }

        .overlay-content-outer {
            display: table;
            position: absolute;
            height: 100%;
            width: 100%;
        }

        .overlay-content-middle {
            display: table-cell;
            vertical-align: middle;
        }

        .overlay-content-inner {
            margin-left: auto;
            margin-right: auto;
            width: 66%;
        }
    </style>

    <div id="AlertPanel" class="overlay">
        <div class="overlay-content-outer">
            <div class="overlay-content-middle">
                <div class="overlay-content-inner">
                    <div class="alert alert-dismissible alert-info">
                        <button type="button" class="close" onclick="DismissAlert();">&times;</button>
                        <h4>出问题辣!</h4>
                        <div id="AlertMessagePlaceholder"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="UploadingPanel" class="overlay">
        <div class="overlay-content-outer">
            <div class="overlay-content-middle">
                <div class="overlay-content-inner">
                    <div class="panel panel-default">
                        <div class="panel-heading">正在上传&处理</div>
                        <div class="panel-body">
                            <div style="text-align: center">
                                <img src="Content/ui/uploading.gif" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="UploadPanel" CssClass="jumbotron" runat="server">
        <h1>今天怎么样?</h1>
        <p class="lead">
            来张自拍
            <br />
            生成你的心情图片~
        </p>
        <asp:FileUpload ID="ImageUploader" CssClass="uploader" onchange="ShowImagePreview(this);" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="ImageUploader" runat="server" />
        <div>
            <div style="text-align: center">
                <div>
                    <label for="<%=ImageUploader.ClientID%>" class="btn btn-default">
                        <asp:Image ID="ImagePreviewer" CssClass="img-thumbnail" src="Content/ui/selectImage.png" Width="100%" runat="server" />
                    </label>
                </div>
                <br />
                <asp:Button ID="UploadButton" CssClass="btn btn-default" Text="生成心情图片" value="submit"
                    OnClick="UploadButton_Click" OnClientClick="ShowLoading();" runat="server" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="ShowPanel" CssClass="well" Visible="false" runat="server">
        <div style="text-align: center;">
            <asp:Image ID="HandledImage" ImageUrl="~/Content/ui/thumb.png" CssClass="img-thumbnail" runat="server" />
            <br />
            <br />
            <div>
                <asp:Label ID="HandledMessage" runat="server" />
                <%--<button class="btn btn-default" type="button" onclick="SaveImage();">保存图片</button>--%>
                <div>
                    <p class="text-muted">
                        喜欢的话请长按保存图片哦~<br />
                        保存放大更清晰<br />
                        返回点右上角汉堡菜单
                    </p>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
