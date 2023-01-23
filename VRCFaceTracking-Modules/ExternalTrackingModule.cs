using System;
using System.Threading;
using VRCFaceTracking;
using VRCFaceTracking.Params;

namespace VRCFaceTracking_Modules
{
    public class VRCFaceTracking_Modules : ExtTrackingModule
    {
        // Lets Unified Library Manager know what type of data is supported.
        public override (bool SupportsEye, bool SupportsExpressions) Supported => (true, true);

        // Synchronous module initialization. Take as much time as you need to initialize any external modules. This runs in the init-thread
        public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
        {
            // Use the incoming parameters to determine if the tracking interface can initialize into any available module slots.
            // Then let the library manager know if your tracking interface has initialized properly
            var moduleState = (eyeAvailable, expressionAvailable);

            return moduleState;
        }

        // Update eye data
        private void UpdateEye(ref UnifiedEyeData eye)
        {
            // Example of updating the Eye container:
            // eye.Right.Openness = 1.0f;
            // eye.Left.Openness = 1.0f;
        }

        // Update expression data
        private void UpdateExpressions(ref UnifiedExpressionShape[] shapes)
        {
            // Example of updating the Shape container:
            // shapes[(int)UnifiedExpressions.JawOpen].Weight = 1.0f;
        }

        // This will be run in the tracking thread. This is exposed so you can control when and if the tracking data is updated down to the lowest level.
        public override Action GetUpdateThreadFunc()
        {
            return () =>
            {
                while (true)
                {
                    Update();
                }
            };
        }

        // The update function needs to be defined separately in case the user is running with the --vrcft-nothread launch parameter
        public void Update()
        {
            Logger.Msg("Updating inside external module.");

            if (Status.EyeState == ModuleState.Active)
            {
                UpdateEye(ref UnifiedTracking.Data.Eye);
            }
            if (Status.ExpressionState == ModuleState.Active)
            {
                UpdateExpressions(ref UnifiedTracking.Data.Shapes);
            }
        }

        public override void Teardown()
        {
            Logger.Msg("Teardown");
        }
    }
}