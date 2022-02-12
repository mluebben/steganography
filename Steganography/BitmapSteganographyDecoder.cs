// SPDX-License-Identifier: GPL-3.0-or-later
//
// Copyright 2022 Matthias Lübben <ml81@gmx.de>
//

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Steganography
{
    public class BitmapSteganographyDecoder
    {
        public byte[] Decode(Bitmap inputBitmap)
        {
            var bitStream = new BitOutputStream();

            var width = inputBitmap.Width;
            var height = inputBitmap.Height;

            var bitmapArea = new Rectangle(0, 0, width, height);

            BitmapData inputData = inputBitmap.LockBits(bitmapArea, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb) ?? throw new InvalidOperationException("Unable to lock input bits.");
            if (inputData == null)
            {
                throw new InvalidOperationException("Unable to lock input bits.");
            }

            unsafe
            {
                byte* inputScan0 = (byte*)inputData.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    byte* inputPtr = inputScan0 + y * inputData.Stride;

                    for (int x = 0; x < width * 3; x++)
                    {
                        byte pixel = *inputPtr;

                        pixel = (byte)(pixel & 0x03);

                        int b1 = (pixel & 0x02) == 0x02 ? 1 : 0;
                        int b2 = (pixel & 0x01) == 0x01 ? 1 : 0;

                        bitStream.WriteBit(b1);
                        bitStream.WriteBit(b2);

                        inputPtr++;
                    }
                }
            }

            inputBitmap.UnlockBits(inputData);

            return bitStream.Data;
        }
    }
}
