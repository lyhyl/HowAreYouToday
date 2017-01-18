using Affdex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRUTWeb
{
    class Describer
    {
        private static string RawDataName(int idx)
        {
            string[] ns = {
                "Anger",
                "Contempt",
                "Disgust",
                "Fear",
                "Joy",
                "Sadness",
                "Surprise"
            };
            return ns[idx];
        }

        public static string DescribeFace(FaceBase face)
        {
            StringBuilder sb = new StringBuilder();

            switch (face.Gender)
            {
                case Gender.Female:
                    string[] d0 = {
                        "???",
                        "娇娥",
                        "淑女",
                        "婵娟",
                        "巾帼",
                        "裙钗",
                        "夫人",
                        "罗敷",
                    };
                    sb.Append(d0[(int)face.Age]);
                    break;
                case Gender.Male:
                    sb.Append($"{face.Age} 汉子!");
                    break;
                default:
                    sb.Append("此脸只应天上有~");
                    break;
            }
            return sb.ToString();
        }

        public static string DescribeIndex(FaceBase face)
        {
            string[] ns = {
                "好气啊!",
                "你看我理你?",
                "厌恶~",
                //"Engagement",
                "好方QAQ",
                "愉快~lalal~",
                "伤心T^T",
                "惊奇!",
                //"Valence"
            };
            int idx = face.DominantEmotionIndex;
            if (idx < 0)
                return "自然";
            return ns[idx];
        }

        public static string DescribEmoji(FaceBase face)
        {
            StringBuilder sb = new StringBuilder(@".\emojiData\");
            if (face.DominantEmoji != Emoji.Unknown)
            {
                sb.Append($@"emoji\{face.DominantEmoji}\");
            }
            else
            {
                sb.Append(@"emotion\");
                int idx = face.DominantEmotionIndex;
                if (idx < 0)
                    sb.Append(@"Null\");
                else
                    sb.Append($@"{RawDataName(idx)}\");
            }
            string path = Path.Combine(".\\", sb.ToString());
            sb.Append(DirectoryHelper.RandomFile(path));
            sb.Append(".png");
            return sb.ToString();
        }

        public static string DescribeIndexDetail(FaceBase face)
        {
            string[] ns = {
                "生气显老啊!",
                "傲娇~哼~",
                "今天遇到不可描述的事情?",
                //"Engagement",
                $"别{(face.Gender==Gender.Male?"怂":"怕")},我在!",
                "开心的你最可爱",
                "宝宝你别哭",
                "收到礼物了?",
                //"Valence"
            };
            int idx = face.DominantEmotionIndex;
            if (idx < 0)
                return "\"我的内心毫无波澜\"";
            return ns[idx];
        }

        public static Color EmotionColor(FaceBase face)
        {
            Color[] cs = {
                Color.Crimson,
                Color.LightSlateGray,
                Color.YellowGreen,
                //Color.CornflowerBlue,
                Color.Salmon,
                Color.Gold,
                Color.CornflowerBlue,
                Color.MediumOrchid,
                //Color.Purple
            };
            int idx = face.DominantEmotionIndex;
            if (idx < 0)
                return Color.Aquamarine;
            return cs[idx];
        }

        public static Color GenderColor(FaceBase face)
        {
            switch (face.Gender)
            {
                default:
                case Gender.Unknown:
                    return Color.Black;
                case Gender.Male:
                    return Color.DodgerBlue;
                case Gender.Female:
                    return Color.Plum;
            }
        }

        public static string Avatar(FaceBase face)
        {
            if (face.Gender == Gender.Unknown || face.Age == Age.Age_Unknown)
                return string.Empty;
            StringBuilder path = new StringBuilder($@".\emojiData\avatar\{face.Gender.ToString()}\");
            switch (face.Age)
            {
                default:
                case Age.Age_Under_18:
                case Age.Age_18_24:
                    path.Append(@"0-24\");
                    break;
                case Age.Age_25_34:
                case Age.Age_35_44:
                case Age.Age_45_54:
                    path.Append(@"25-54\");
                    break;
                case Age.Age_55_64:
                case Age.Age_65_Plus:
                    path.Append(@"55+\");
                    break;
            }
            path.Append(face.Ethnicity.ToString());
            path.Append(".png");
            return path.ToString();
        }
    }
}
