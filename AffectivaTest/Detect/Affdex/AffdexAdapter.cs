using Affdex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRUTWeb
{
    public class AffdexAdapter :
        IDetectAdapter,
        ImageListener,
        ProcessStatusListener
    {
        private string classifierPath = @"C:\Program Files (x86)\Affectiva\Affdex SDK\data";
        private static AffdexAdapter affdex = null;
        private PhotoDetector detector;

        public event EventHandler<DetectAdapterEventAgrs> OnFailure;
        public event EventHandler<DetectAdapterEventAgrs> OnSuccess;
        
        private AffdexAdapter(FaceDetectorMode mode)
        {
            detector = new PhotoDetector(1, mode);

            detector.setClassifierPath(classifierPath);
            
            detector.setProcessStatusListener(this);
            detector.setImageListener(this);

            detector.setDetectAllAppearances(true);
            detector.setDetectAllEmojis(true);
            detector.setDetectAllEmotions(true);
            detector.setDetectAllExpressions(false);
        }

        public static AffdexAdapter CreateSingleton(FaceDetectorMode mode)
        {
            if (affdex == null)
                affdex = new AffdexAdapter(mode);
            return affdex;
        }

        public void Process(Bitmap image)
        {
            try
            {
                detector.start();
                detector.process(image.ConvertToFrame());
            }
            catch (AffdexException ae)
            {
                detector.stop();
                OnFailure?.Invoke(this, new DetectAdapterEventAgrs(ae));
            }
        }

        public void onProcessingException(AffdexException ex)
        {
            detector.stop();
            OnFailure?.Invoke(this, new DetectAdapterEventAgrs(ex));
        }

        public void onProcessingFinished()
        {
            detector.stop();
        }

        public void onImageCapture(Frame frame)
        {
            frame.Dispose();
        }

        public void onImageResults(Dictionary<int, Face> faces, Frame frame)
        {
            if (faces.Count == 0)
            {
                OnFailure?.Invoke(this, new DetectAdapterEventAgrs(new Exception("Face not found")));
            }
            else
            {
                OnSuccess?.Invoke(this, new DetectAdapterEventAgrs(new AffdexFace(faces.First().Value)));
            }
            frame.Dispose();
        }
    }
}
