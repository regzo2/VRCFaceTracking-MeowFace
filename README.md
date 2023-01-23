# MeowFace Tracking Module

> Let VRCFaceTracking take advantage of **MeowFace**'s tracking directly from your Android device.

## Installing **MeowFace Tracking Module** for **VRCFaceTracking**.

**VRCFaceTracking is required to use MeowFace Tracking Module.**

* Installer
  * Download and run the installer to install **MeowFace Tracking Module** into the **VRCFaceTracking** module directory directly.
  
* Manual
  * Include the supplied **.dll** release in a `CustomLibs` folder next to the **VRCFaceTracking** executable or in `%appdata%/VRCFaceTracking/CustomLibs` for a global install. 
  
## Using MeowFace with VRCFaceTracking

1. Download **MeowFace** from [Google Play](https://play.google.com/store/apps/details?id=com.suvidriel.meowface&hl=en_US&gl=US)
2. Initialize `MeowFaceExtTrackingInterface` in the **VRCFaceTracking** app.
3. Use the information provided from **VRCFaceTracking** to configure **MeowFace** to properly send the data.

## Troubleshooting

> MeowFace is not connecting to VRCFaceTracking.

Make sure that you have **MeowFace**'s *IP* and *port* configured as specified by **VRCFaceTracking** and that you are connected to the same network as the host. Optionally you can send the data to the port to your global IP from any MeowFace connection by port forwarding the specified port.

> MeowFace is not tracking as expected.

MeowFace is not all too perfect at tracking your face... however: some of the tracking may not track as expected when converted for use into VRCFaceTracking's parameter system. Feel free to make an issue if MeowFace doesn't track as expected!
