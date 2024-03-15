
## 📹 HLS Download Wrapper

![version](https://img.shields.io/badge/version-1.1.0.0-blue.svg)
![dotnetframework](https://img.shields.io/badge/.net%20framework-4.7.2-green.svg)
[![ffmpeg](https://img.shields.io/badge/ffmpeg-4.4-brightgreen.svg)](https://github.com/FFmpeg/FFmpeg/commit/f68ab9de4e)
[![icon](https://img.shields.io/badge/icon-iconfinder-pink.svg)](https://www.iconfinder.com)
![portable](https://img.shields.io/badge/portable-windows%20x64-yellow.svg)
![license](https://img.shields.io/badge/license-GPL%20%28inherited%29-blueviolet.svg)

WPF interface bridging the m3u8 stream download utility of [FFmpeg](https://github.com/FFmpeg/FFmpeg).

<img src="/imgs/Demo.gif?raw=true">


### 💡 How This Works

Visit [m3u8-dlwrapper.zip](https://github.com/der3318/m3u8-dlwrapper/releases/download/v1.1.0.0/m3u8-dlwrapper.zip) for the built binaries, or build the WPF project using Visual Studio on your own. Unzip and run `m3u8-dlwrapper.exe` on a Windows x64 machine. Enter the clip info to start downloading a .m3u8 stream.

These are what the tool actually does in the backend:

| Option | FFmpeg Command |
| :- | :- |
| 2x checked | ffmpeg.exe -user_agent "{UserAgent}" -headers "{Cookie}" -i {M3U8Link} -filter_complex "[0:v]setpts=PTS/2[v];[0:a]atempo=2[a]" -map "[v]" -map "[a]" -bsf:a aac_adtstoasc {SaveAs.mp4}
| 2x unchecked | ffmpeg.exe -user_agent "{UserAgent}" -headers "{Cookie}" -i {M3U8Link} -c copy -bsf:a aac_adtstoasc {SaveAs.mp4} |

All the STDOUT and STDERR (100 latest lines) of `ffmpeg.exe` wil be redirect and kept in the text area.


### 🖇️ How to Use - HLS (.m3u8) Link Retrieval

Chromium's DevTools panel (F12) is very useful. HLS links can be easily captured under "Network" tab when using ".m3u8" as the filter. Noted that this no longer works for Youtube and Bilibili, but it's still worth trying on some other video sites.

<img src="/imgs/LinkRetrieval.png">


### 🌐 How to Use - User Agent

Sometimes your request to a HLS link might be rejected due to incorrect user agent. Override the browser agent to your own with help of the online query: https://www.whatismybrowser.com/detect/what-is-my-user-agent

<img src="/imgs/UserAgent.png">


### 🍪 How to Use - Cookie

For those sites requiring sign-in and memberships to view videos, cookies play an important role. You might need to duplicate some cookies in order to make the tool works. Take a look at what are being used by clicking at the begining of the URL field:

<img src="/imgs/Cookie.png">

