using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeowFaceExtTrackingInterface
{
    public class MeowFaceData
    {
        public long Timestamp { get; set; }
        public int Hotkey { get; set; }
        public bool FaceFound { get; set; }
        public MeowVector Rotation { get; set; }
        public MeowVector Position { get; set; }
        public MeowVector EyeLeft { get; set; }
        public MeowVector EyeRight { get; set; }

        [JsonConverter(typeof(MeowShapeListConverter))]
        public MeowShape[] BlendShapes { get; set; } = Array.Empty<MeowShape>();
    }
}
