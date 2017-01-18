using Affdex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRUTWeb
{
    public struct CircleF
    {
        public PointF Center;
        public float Radius;

        public CircleF(PointF c, float r)
        {
            Center = c;
            Radius = r;
        }
    }

    public abstract class FaceBase
    {
        //"neutral"
        //"Engagement"
        //"Valence"

        public abstract float Anger { get; }
        public abstract float Contempt { get; }
        public abstract float Disgust { get; }
        public abstract float Fear { get; }
        public abstract float Joy { get; }
        public abstract float Sadness { get; }
        public abstract float Surprise { get; }

        public abstract float DominantEmotion { get; }
        public abstract int DominantEmotionIndex { get; }

        public abstract Emoji DominantEmoji { get; }

        public abstract Gender Gender { get; }
        public abstract Age Age { get; }
        public abstract Ethnicity Ethnicity { get; }

        public abstract CircleF EnclosingCircle();
        public abstract RectangleF EnclosingRectangle();
    }
}
