﻿using System;
using System.Diagnostics;
using System.IO;

namespace RPChecker.Util.FilterProcess
{
    public static class Utils
    {
        public static string GetVsPipePath(this IProcess process, out Exception exception)
        {
            exception = null;
            string vspipePath;
            try
            {
                vspipePath = RegistryStorage.Load();
                if (vspipePath == null || !File.Exists(Path.Combine(vspipePath, "vspipe.exe")))
                {
                    vspipePath = Path.GetDirectoryName(ToolKits.GetFullPathFromWindows("vspipe.exe")) ??
                                 ToolKits.GetVapourSynthPathViaRegistry();
                    if (vspipePath != null)
                    {
                        RegistryStorage.Save(vspipePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                vspipePath = string.Empty;
                exception = ex;
            }
            return vspipePath;
        }


        public static string GetFFmpegPath(this IProcess process, out Exception exception)
        {
            exception = null;
            var ffmpegInReg = RegistryStorage.Load(name: "FFmpegPath");
            try
            {
                var ffmpegInPath = Path.GetDirectoryName(ToolKits.GetFullPathFromWindows("ffmpeg.exe"));
                if (!string.IsNullOrEmpty(ffmpegInPath))
                {
                    if (ffmpegInReg != ffmpegInPath)
                        RegistryStorage.Save(ffmpegInPath, name: "FFmpegPath");
                    return ffmpegInPath;
                }

                if (!File.Exists(Path.Combine(ffmpegInReg, "ffmpeg.exe")))
                {
                    ffmpegInReg = Notification.InputBox("请输入FFmpeg的地址", "注意不要带上多余的引号", "C:\\FFmpeg\\ffmpeg.exe");
                    RegistryStorage.Save(Path.GetDirectoryName(ffmpegInReg), name: "FFmpegPath");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return ffmpegInReg;
        }
    }
}