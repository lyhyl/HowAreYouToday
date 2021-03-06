﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRUTWeb
{
    public class DetectAdapterEventAgrs : EventArgs
    {
        public FaceBase Face { private set; get; }
        public Exception Exception { private set; get; }

        public DetectAdapterEventAgrs(FaceBase face)
        {
            Face = face;
        }

        public DetectAdapterEventAgrs(Exception ex)
        {
            Exception = ex;
        }
    }

    interface IDetectAdapter
    {
        event EventHandler<DetectAdapterEventAgrs> OnFailure;
        event EventHandler<DetectAdapterEventAgrs> OnSuccess;
        void Process(Bitmap image);
    }
}
