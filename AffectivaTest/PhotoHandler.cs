using Affdex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HRUTWeb
{
    public class PhotoHandlerEventArgs : EventArgs
    {
        public Image Image { private set; get; }
        public Exception Exception { private set; get; }

        public PhotoHandlerEventArgs(Image img)
        {
            Image = img;
        }

        public PhotoHandlerEventArgs(Exception e)
        {
            Exception = e;
        }
    }
    public class PhotoHandler
    {
        private FaceDrawer faceDrawer = new FaceDrawer();
        private Bitmap imageToProcess;
        private IDetectAdapter[] detecters;
        
        public event EventHandler<PhotoHandlerEventArgs> OnFinished;

        public PhotoHandler()
        {
            detecters = new IDetectAdapter[] {
                AffdexAdapter.CreateSingleton(FaceDetectorMode.LARGE_FACES),
                new MSAdapter()
            };

            for (int i = 0; i < detecters.Length; i++)
                detecters[i].OnSuccess += Det_OnSuccess;

            for (int i = 0; i < detecters.Length - 1; i++)
            {
                int next = i + 1;
                detecters[i].OnFailure += (s, e) => detecters[next].Process(imageToProcess);
            }

            detecters.Last().OnFailure += Det_OnFailure;
        }

        public void Detect(string filename)
        {
            imageToProcess = BitmapExtensions.LoadImageFitSize(filename);
            detecters.First().Process(imageToProcess);
        }

        private void Det_OnFailure(object sender, DetectAdapterEventAgrs e)
        {
            try
            {
                OnFinished?.Invoke(this, new PhotoHandlerEventArgs(e.Exception));
            }
            finally
            {
                imageToProcess.Dispose();
                imageToProcess = null;
            }
        }

        private void Det_OnSuccess(object sender, DetectAdapterEventAgrs e)
        {
            try
            {
                Image image = faceDrawer.Draw(e.Face, imageToProcess);
                OnFinished?.Invoke(this, new PhotoHandlerEventArgs(image));
            }
            finally
            {
                imageToProcess.Dispose();
                imageToProcess = null;
            }
        }
    }
}