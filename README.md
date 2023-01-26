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
=======
# VRCFaceTracking Modules
> All external tracking modules that I have developed and released for VRCFaceTracking

## Modules
The following VRCFaceTracking modules can be found in their respective [branches](https://github.com/regzo2/VRCFaceTracking-Modules/branches)

* [MeowFace Tracking Module](https://github.com/regzo2/VRCFaceTracking-Modules/tree/meowface)
  
## Licenses / Distribution

**All source and release files fall under the [Apache-2.0 License](https://github.com/regzo2/VRCFaceTracking-Modules/blob/master/LICENSE.txt)**.

## Credits
- [Ben](https://github.com/benaclejames/) for VRCFaceTracking!