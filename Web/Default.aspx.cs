using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRUTWeb
{
    public partial class _Default : Page
    {
        private string filename;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
#if DEBUG
                System.Threading.Thread.Sleep(3 * 1000);
#endif

                UploadPanel.Visible = false;
                ShowPanel.Visible = true;

                filename =
                        Server.MapPath("~/App_Data/imgUpload/") +
                        Guid.NewGuid() +
                        Path.GetExtension(ImageUploader.FileName);
                ImageUploader.SaveAs(filename);
                ImageUploader.Dispose();

                Global.PhotoHandler.Detect(filename, PHandler_OnFinished);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(nameof(UploadButton_Click), ex);
            }
        }

        private void PHandler_OnFinished(object sender, PhotoHandlerEventArgs e)
        {
            if (e.Exception == null)
            {
                MemoryStream ms = new MemoryStream();
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters encoderParams = new EncoderParameters(1);
                EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, 100L);
                encoderParams.Param[0] = encoderParam;
                e.Image.Save(ms, jpgEncoder, encoderParams);
                ms.Position = 0;
                BinaryReader br = new BinaryReader(ms);
                byte[] bytes = br.ReadBytes((int)ms.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                HandledImage.ImageUrl = "data:image/jpeg;base64," + base64String;
            }
            else
            {
                Logger.WriteLog(nameof(PHandler_OnFinished), e.Exception);
                HandledImage.ImageUrl = "Content/ui/faceNotFound.jpg";
                HandledMessage.Text = e.Exception.Message;
            }
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;
            return null;
        }
    }
}