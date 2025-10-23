# MeowFace Tracking Module

> Allows [VRCFaceTracking](https://docs.vrcft.io/docs/hardware/meowface) to take advantage of **MeowFace**'s tracking directly from your Android device.

## Installing **MeowFace Tracking Module** for **VRCFaceTracking**.

[Latest Release](https://github.com/regzo2/VRCFaceTracking-MeowFace/releases)

- VRCFaceTracking Module Registry
  - Install **MeowFace Tracking Module** by looking it up in the **Module Registry** and installing directly!
- Manual
  - Include the supplied **.dll** release in `%appdata%/VRCFaceTracking/CustomLibs`.

**VRCFaceTracking is required to use MeowFace Tracking Module.**

## Using MeowFace with VRCFaceTracking

1. Download **MeowFace** from [itch.io](https://suvidriel.itch.io/meowface/devlog/838034/meowface-moved-to-itch)
2. Get your PC's (the computer on which VRCFaceTracking is running) [local IP address](./docs/how-to-get-your-local-ip.md)
3. Open the **MeowFace** app and
4. Initialize `MeowFaceExtTrackingInterface` in the **VRCFaceTracking** app.
5. Configure the IP and port to match the IP and port of the PC running **VRCFaceTracking**.
   - The default port is `12345`.
   - The ip will be the local IP of the PC running **VRCFaceTracking**.

## Troubleshooting

> MeowFace is not connecting to VRCFaceTracking.

Make sure that you have **MeowFace**'s _IP_ and _port_ configured as specified by **VRCFaceTracking** and that you are connected to the same network as the host. Optionally you can send the data to the port to your global IP from any MeowFace connection by port forwarding the specified port.

> MeowFace is not tracking as expected.

MeowFace is not all too perfect at tracking your face... however: some of the tracking may not track as expected when converted for use into VRCFaceTracking's parameter system. Feel free to make an issue if MeowFace doesn't track as expected!

## Licenses / Distribution

**All source and release files fall under the [Apache-2.0 License](https://github.com/regzo2/VRCFaceTracking-Modules/blob/master/LICENSE.txt)**.

## Credits

- [Ben](https://github.com/benaclejames/) for VRCFaceTracking!
