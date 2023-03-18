using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using VRCFaceTracking;
using VRCFaceTracking.Params;

namespace MeowFaceExtTrackingInterface
{
    public class MeowFaceOSCExtTrackingInterface : ExtTrackingModule
    {
        private static int port = 12345;
        private static UdpClient client;
        private static IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        private static (bool, bool) sendData = (false, false);
        private static readonly float radianConst = 0.0174533f;

        public override (bool SupportsEye, bool SupportsExpression) Supported => (true, true);

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No connection found!");
        }

        public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
        {
            client = new UdpClient(port);
            client.Client.SendTimeout = 1000;
            client.Client.ReceiveTimeout = 1000;

            Logger.Warning("Seeking MeowFace connection for 60s. Accepting data on " + GetLocalIPAddress() + ":" + port);
            for (int i = 60; i > 0; i--)
            {
                try
                {
                    byte[] buffer = client.Receive(ref endPoint);
                    MeowFaceData data = new JavaScriptSerializer().Deserialize<MeowFaceData>(Encoding.ASCII.GetString(buffer));
                }
                catch (SocketException)
                {
                    continue;
                }
                catch (ArgumentNullException)
                {
                    Logger.Warning("JSON data sent does not match expected structure.");
                    return (false, false);
                }
                sendData = (eyeAvailable, expressionAvailable);
                return (eyeAvailable, expressionAvailable);
            }

            return (eyeAvailable, expressionAvailable);
        }

        private void UpdateEye(ref UnifiedEyeData eye, ref MeowFaceData data)
        {
            /*
            Logger.Msg("X" + data.EyeRight.x.ToString());
            Logger.Msg("Y" + data.EyeRight.y.ToString());
            Logger.Msg("z" + data.EyeRight.z.ToString());
            */

            // Data has wrong polarization
            eye.Right.Gaze.x = data.EyeRight.y * radianConst;
            eye.Right.Gaze.y = -data.EyeRight.x * radianConst;
            eye.Left.Gaze.x = data.EyeLeft.y * radianConst;
            eye.Left.Gaze.y = -data.EyeLeft.x * radianConst;

            // Both squint and eyeBlink contribute to openness. Using blink to gate squint from affecting openness partially to allow it to modify it's dedicated shape.
            eye.Right.Openness = 1.0f - (float)Math.Min(1, data.BlendShapes[(int)MeowShapeType.eyeBlinkRight].v +
                Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeBlinkRight].v, .33) * Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeSquintRight].v, 1.25));

            eye.Left.Openness = 1.0f - (float)Math.Min(1, data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v +
                (Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v, .33) * Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeSquintLeft].v, 1.25)));

            // ALVR simplified:
            //eye.Left.Openness = 1.0f - (float)Math.Min(1, data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v * (1.0 - data.BlendShapes[(int)MeowShapeType.eyeWideLeft].v) +
            //    (Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v, .33) * Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeSquintLeft].v, 1.25)));
            
        }
        private void UpdateEyeExpressions(ref UnifiedExpressionShape[] shapes, ref MeowFaceData data)
        {
            shapes[(int)UnifiedExpressions.BrowPinchLeft].Weight = data.BlendShapes[(int)MeowShapeType.browDownLeft].v;
            shapes[(int)UnifiedExpressions.BrowLowererLeft].Weight = data.BlendShapes[(int)MeowShapeType.browDownLeft].v;
            shapes[(int)UnifiedExpressions.BrowLowererRight].Weight = data.BlendShapes[(int)MeowShapeType.browDownRight].v;
            shapes[(int)UnifiedExpressions.BrowPinchRight].Weight = data.BlendShapes[(int)MeowShapeType.browDownRight].v;

            shapes[(int)UnifiedExpressions.BrowInnerUpRight].Weight = data.BlendShapes[(int)MeowShapeType.browOuterUpRight].v;
            shapes[(int)UnifiedExpressions.BrowOuterUpRight].Weight = data.BlendShapes[(int)MeowShapeType.browInnerUpRight].v;
            shapes[(int)UnifiedExpressions.BrowInnerUpLeft].Weight = data.BlendShapes[(int)MeowShapeType.browOuterUpLeft].v;
            shapes[(int)UnifiedExpressions.BrowOuterUpLeft].Weight = data.BlendShapes[(int)MeowShapeType.browInnerUpLeft].v;

            shapes[(int)UnifiedExpressions.EyeSquintLeft].Weight = data.BlendShapes[(int)MeowShapeType.eyeSquintLeft].v;
            shapes[(int)UnifiedExpressions.EyeSquintRight].Weight = data.BlendShapes[(int)MeowShapeType.eyeSquintRight].v;
            shapes[(int)UnifiedExpressions.EyeWideRight].Weight = data.BlendShapes[(int)MeowShapeType.eyeWideRight].v;
            shapes[(int)UnifiedExpressions.EyeWideLeft].Weight = data.BlendShapes[(int)MeowShapeType.eyeWideLeft].v;
        }
        private void UpdateExpressions(ref UnifiedExpressionShape[] shapes, ref MeowFaceData data)
        {
            shapes[(int)UnifiedExpressions.JawOpen].Weight = data.BlendShapes[(int)MeowShapeType.jawOpen].v;
            shapes[(int)UnifiedExpressions.JawLeft].Weight = data.BlendShapes[(int)MeowShapeType.jawLeft].v;
            shapes[(int)UnifiedExpressions.JawRight].Weight = data.BlendShapes[(int)MeowShapeType.jawRight].v;

            shapes[((int)UnifiedExpressions.MouthUpperRight)].Weight = data.BlendShapes[(int)MeowShapeType.mouthRight].v;
            shapes[((int)UnifiedExpressions.MouthLowerRight)].Weight = data.BlendShapes[(int)MeowShapeType.mouthRight].v;
            shapes[((int)UnifiedExpressions.MouthUpperLeft)].Weight = data.BlendShapes[(int)MeowShapeType.mouthLeft].v;
            shapes[((int)UnifiedExpressions.MouthLowerLeft)].Weight = data.BlendShapes[(int)MeowShapeType.mouthLeft].v;

            shapes[(int)UnifiedExpressions.MouthUpperUpRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthUpperUpRight].v;
            shapes[(int)UnifiedExpressions.MouthUpperDeepenRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthUpperUpRight].v;
            shapes[(int)UnifiedExpressions.MouthUpperUpLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthUpperUpLeft].v;
            shapes[(int)UnifiedExpressions.MouthUpperDeepenLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthUpperUpLeft].v;
            shapes[(int)UnifiedExpressions.MouthLowerDownRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthLowerDownRight].v;
            shapes[(int)UnifiedExpressions.MouthLowerDownLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthLowerDownLeft].v;

            shapes[(int)UnifiedExpressions.LipPuckerUpperRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthPucker].v;
            shapes[(int)UnifiedExpressions.LipPuckerLowerRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthPucker].v;
            shapes[(int)UnifiedExpressions.LipPuckerUpperLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthPucker].v;
            shapes[(int)UnifiedExpressions.LipPuckerLowerLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthPucker].v;

            shapes[(int)UnifiedExpressions.NoseSneerRight].Weight = data.BlendShapes[(int)MeowShapeType.noseSneerRight].v;
            shapes[(int)UnifiedExpressions.NoseSneerLeft].Weight = data.BlendShapes[(int)MeowShapeType.noseSneerLeft].v;

            shapes[(int)UnifiedExpressions.MouthCornerPullRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthSmileRight].v;
            shapes[(int)UnifiedExpressions.MouthCornerSlantRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthSmileRight].v;
            shapes[(int)UnifiedExpressions.MouthCornerPullLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthSmileLeft].v;
            shapes[(int)UnifiedExpressions.MouthCornerSlantLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthSmileLeft].v;
            shapes[(int)UnifiedExpressions.MouthFrownRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthFrownRight].v;
            shapes[(int)UnifiedExpressions.MouthFrownLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthFrownLeft].v;

            shapes[(int)UnifiedExpressions.LipFunnelLowerLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthFunnel].v;
            shapes[(int)UnifiedExpressions.LipFunnelLowerRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthFunnel].v;
            shapes[(int)UnifiedExpressions.LipFunnelUpperLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthFunnel].v;
            shapes[(int)UnifiedExpressions.LipFunnelUpperRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthFunnel].v;

            shapes[(int)UnifiedExpressions.LipSuckUpperRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthRollUpper].v;
            shapes[(int)UnifiedExpressions.LipSuckUpperLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthRollUpper].v;
            shapes[(int)UnifiedExpressions.LipSuckLowerRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthRollLower].v;
            shapes[(int)UnifiedExpressions.LipSuckLowerLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthRollLower].v;

            shapes[(int)UnifiedExpressions.MouthRaiserUpper].Weight = data.BlendShapes[(int)MeowShapeType.mouthShrugUpper].v;
            shapes[(int)UnifiedExpressions.MouthRaiserLower].Weight = data.BlendShapes[(int)MeowShapeType.mouthShrugUpper].v;

            shapes[(int)UnifiedExpressions.TongueOut].Weight = data.BlendShapes[(int)MeowShapeType.tongueOut].v;

            // Simulated shapes.
            shapes[(int)UnifiedExpressions.CheekSquintRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthSmileRight].v;
            shapes[(int)UnifiedExpressions.CheekSquintLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthSmileLeft].v;

            shapes[(int)UnifiedExpressions.MouthStretchRight].Weight = data.BlendShapes[(int)MeowShapeType.mouthFrownRight].v;
            shapes[(int)UnifiedExpressions.MouthStretchLeft].Weight = data.BlendShapes[(int)MeowShapeType.mouthFrownLeft].v;
        }

        public override Action GetUpdateThreadFunc()
        {
            return () =>
            {
                while (true)
                {
                    Update();
                    Thread.Sleep(10);
                }
            };
        }

        public void Update()
        {
            try
            {
                byte[] buffer = client.Receive(ref endPoint);
                MeowFaceData data = new JavaScriptSerializer().Deserialize<MeowFaceData>(Encoding.ASCII.GetString(buffer));

                if (sendData.Item1)
                {
                    UpdateEye(ref UnifiedTracking.Data.Eye, ref data);
                    UpdateEyeExpressions(ref UnifiedTracking.Data.Shapes, ref data);
                }
                if (sendData.Item2)
                    UpdateExpressions(ref UnifiedTracking.Data.Shapes, ref data);

                Logger.Msg("Eye Closed: " + data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v.ToString() + " Eye Squint: " + data.BlendShapes[(int)MeowShapeType.eyeSquintLeft].v.ToString() +
                    "\nSquint+Closed: " + (data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v + data.BlendShapes[(int)MeowShapeType.eyeSquintLeft].v).ToString() +
                    "\nSquint+Closed Comp: " + ((float)Math.Min(1, data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v +
                (Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeBlinkLeft].v, .33) * Math.Pow(data.BlendShapes[(int)MeowShapeType.eyeSquintLeft].v, 1.25)))).ToString());
            }
            catch (SocketException)
            {
                Thread.Sleep(15000);
                return;
            }
        }

        public override void Teardown()
        {
            Logger.Msg("Closing UDP client...");
            client.Dispose();
        }
    }
}