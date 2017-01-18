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
        private string fname;
        private Bitmap imageToProcess;
        private IDetectAdapter[] detectors;
        private object detectorLock = new object();
        private EventHandler<PhotoHandlerEventArgs> OnFinished;

        public PhotoHandler()
        {
            detectors = new IDetectAdapter[] {
                AffdexAdapter.CreateSingleton(FaceDetectorMode.LARGE_FACES),
                new MSAdapter()
            };

            for (int i = 0; i < detectors.Length; i++)
                detectors[i].OnSuccess += Det_OnSuccess;

            for (int i = 0; i < detectors.Length - 1; i++)
            {
                int next = i + 1;
                detectors[i].OnFailure += (s, e) => detectors[next].Process(imageToProcess);
            }

            detectors.Last().OnFailure += Det_OnFailure;
        }

        public void Detect(string filename, EventHandler<PhotoHandlerEventArgs> callback)
        {
            lock (detectorLock)
            {
                fname = System.IO.Path.GetFileNameWithoutExtension(filename);
                Logger.WriteLog($"Detect {fname}");
                OnFinished = callback;
                imageToProcess = BitmapExtensions.LoadImageFitSize(filename);
                detectors.First().Process(imageToProcess);
            }
        }

        private void Det_OnFailure(object sender, DetectAdapterEventAgrs e)
        {
            try
            {
                Logger.WriteLog($"Failed {fname}");
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