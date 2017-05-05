﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RPChecker.Util
{
    public class NativeMethods
    {
        [DllImport("shell32.dll", EntryPoint = "#680", CharSet = CharSet.Unicode)]
        public static extern bool IsUserAnAdmin();

        [DllImport("Kernel32.dll", EntryPoint = "CreateHardLinkW", CharSet = CharSet.Unicode)]
        private static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        public static bool CreateHardLink(string lpFileName, string lpExistingFileName)
        {
            return CreateHardLink(lpFileName, lpExistingFileName, IntPtr.Zero);
        }

        public static void CreateHardLinkCMD(string lpFileName, string lpExistingFileName)
        {
            new Process {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "fsutil",
                    Arguments = $"hardlink create \"{lpFileName}\" \"{lpExistingFileName}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            }.Start();
        }
    }
}