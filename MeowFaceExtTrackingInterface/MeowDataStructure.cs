using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowFaceExtTrackingInterface
{
    public struct MeowVector
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
    public struct MeowShape 
    { 
        public string k { get; set; }
        public float v { get; set; }

        public MeowShape(string key, float value)
        {
            k = key;
            v = value;
        }
    }

    public enum MeowShapeType
    {
        jawOpen,
        eyeLookOutRight,
        eyeLookDownRight,
        noseSneerLeft,
        eyeLookOutLeft,
        noseSneerRight,
        mouthLeft,
        headRight,
        eyeLookUpLeft,
        eyeLookUpRight,
        headUp,
        mouthRollLower,
        cheekPuff,
        browOuterUpRight,
        eyeLookInRight,
        mouthUpperUpLeft,
        browInnerUpRight,
        headRollRight,
        eyeLookInLeft,
        jawLeft,
        browInnerUpLeft,
        mouthUpperUpRight,
        mouthRight,
        browDownRight,
        headDown,
        eyeWideRight,
        browDownLeft,
        mouthShrugUpper,
        mouthRollUpper,
        eyeWideLeft,
        browOuterUpLeft,
        tongueOut,
        eyeSquintLeft,
        jawRight,
        mouthLowerDownRight,
        mouthLowerDownLeft,
        eyeLookDownLeft,
        eyeSquintRight,
        mouthFrownLeft,
        mouthFrownRight,
        mouthSmileRight,
        headRollLeft,
        eyeBlinkLeft,
        mouthPucker,
        eyeBlinkRight,
        mouthSmileLeft,
        mouthFunnel,
        headLeft,
        browInnerUp
    }
}
