using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowFaceExtTrackingInterface
{
    public class MeowFaceData
    {
        public long Timestamp;
        public int Hotkey;
        public bool FaceFound;
        public MeowVector Rotation;
        public MeowVector Position;
        public MeowVector EyeLeft;
        public MeowVector EyeRight;

        // the data is jumbled like this. we only need to use this to get the key/value pair from the json sent.
        public MeowShape[] BlendShapes =
        {
            new MeowShape( "jawOpen", 0.0f ),
            new MeowShape( "eyeLookOutRight", 0.0f ),
            new MeowShape( "eyeLookDownRight", 0.0f ),
            new MeowShape( "noseSneerLeft", 0.0f ),
            new MeowShape( "eyeLookOutLeft", 0.0f ),
            new MeowShape( "noseSneerRight", 0.0f ),
            new MeowShape( "mouthLeft", 0.0f ),
            new MeowShape( "headRight", 0.0f ),
            new MeowShape( "eyeLookUpLeft", 0.0f ),
            new MeowShape( "eyeLookUpRight", 0.0f ),
            new MeowShape( "headUp", 0.0f ),
            new MeowShape( "mouthRollLower", 0.0f ),
            new MeowShape( "cheekPuff", 0.0f ),
            new MeowShape( "browOuterUpRight", 0.0f ),
            new MeowShape( "eyeLookInRight", 0.0f ),
            new MeowShape( "mouthUpperUpLeft", 0.0f ),
            new MeowShape( "browInnerUpRight", 0.0f ),
            new MeowShape( "headRollRight", 0.0f ),
            new MeowShape( "eyeLookInLeft", 0.0f ),
            new MeowShape( "jawLeft", 0.0f ),
            new MeowShape( "browInnerUpLeft", 0.0f ),
            new MeowShape( "mouthUpperUpRight", 0.0f ),
            new MeowShape( "mouthRight", 0.0f ),
            new MeowShape( "browDownRight", 0.0f ),
            new MeowShape( "headDown", 0.0f ),
            new MeowShape( "eyeWideRight", 0.0f ),
            new MeowShape( "browDownLeft", 0.0f ),
            new MeowShape( "mouthShrugUpper", 0.0f ),
            new MeowShape( "mouthRollUpper", 0.0f ),
            new MeowShape( "eyeWideLeft", 0.0f ),
            new MeowShape( "browOuterUoLeft", 0.0f ),
            new MeowShape( "tongueOut", 0.0f ),
            new MeowShape( "eyeSquintLeft", 0.0f ),
            new MeowShape( "jawRight", 0.0f ),
            new MeowShape( "mouthLowerDownRight", 0.0f ),
            new MeowShape( "mouthLowerDownLeft", 0.0f ),
            new MeowShape( "eyeLookDownLeft", 0.0f ),
            new MeowShape( "eyeSquintRight", 0.0f ),
            new MeowShape( "mouthFrownLeft", 0.0f ),
            new MeowShape( "mouthFrownRight", 0.0f ),
            new MeowShape( "mouthSmileRight", 0.0f ),
            new MeowShape( "headRollLeft", 0.0f ),
            new MeowShape( "eyeBlinkLeft", 0.0f ),
            new MeowShape( "mouthPucker", 0.0f ),
            new MeowShape( "eyeBlinkRight", 0.0f ),
            new MeowShape( "mouthSmileLeft", 0.0f ),
            new MeowShape( "mouthFunnel", 0.0f ),
            new MeowShape( "headLeft", 0.0f ),
            new MeowShape( "browInnerUp", 0.0f )
        };
    }
}
