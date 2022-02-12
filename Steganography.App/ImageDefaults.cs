// SPDX-License-Identifier: GPL-3.0-or-later
//
// Copyright 2022 Matthias Lübben <ml81@gmx.de>
//

using System;

namespace Steganography.App
{
    public static class ImageDefaults
    {
        public static string GetDefaultEncoderInputFile()
        {
            string workPath = GetWorkPath();

            return System.IO.Path.Combine(workPath, "katze.jpg");
        }

        public static string GetDefaultDecoderInputFile()
        {
            string workPath = GetWorkPath();

            return System.IO.Path.Combine(workPath, "katze.encoded.png");
        }

        public static string GetWorkPath()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string? strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            if (strWorkPath == null)
            {
                throw new InvalidOperationException("Unable to determine work path.");
            }

            return strWorkPath;
        }
       
    }
}
