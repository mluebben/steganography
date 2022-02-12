// SPDX-License-Identifier: GPL-3.0-or-later
//
// Copyright 2022 Matthias Lübben <ml81@gmx.de>
//

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Steganography
{
    public class BitmapSteganographyEncoder
    {
        public int RedBits { get; set; }
        public int GreenBits { get; set; }
        public int BlueBits { get; set; }

        public Bitmap Encode(byte[] message, Bitmap inputBitmap)
        {
            var bitStream = new BitInputStream(message);

            var width = inputBitmap.Width;
            var height = inputBitmap.Height;

            Bitmap outputBitmap = new Bitmap(width, height);

            var bitmapArea = new Rectangle(0, 0, width, height);

            BitmapData inputData = inputBitmap.LockBits(bitmapArea, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb) ?? throw new InvalidOperationException("Unable to lock input bits.");
            if (inputData == null)
            {
                throw new InvalidOperationException("Unable to lock input bits.");
            }

            var outputData = outputBitmap.LockBits(bitmapArea, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            if (outputData == null)
            {
                throw new InvalidOperationException("Unable to lock output bits.");
            }
            //new Bitmap()
            unsafe
            {
                byte* inputScan0 = (byte*)inputData.Scan0.ToPointer();
                byte* outputScan0 = (byte*)outputData.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    byte* inputPtr = inputScan0 + y * inputData.Stride;
                    byte* outputPtr = outputScan0 + y * outputData.Stride;

                    for (int x = 0; x < width * 3; x++)
                    {
                        byte pixel = *inputPtr;
                        pixel = (byte)(pixel & 0xFC);

                        int b1 = bitStream.ReadBit();
                        int b2 = bitStream.ReadBit();
                        if (b1 == -1)
                        {
                            b1 = 0;
                        }
                        if (b2 == -1)
                        {
                            b2 = 0;
                        }

                        int bits = b1 << 1 | b2;

                        pixel = (byte)(pixel | bits);

                        *outputPtr = pixel;
                        inputPtr++;
                        outputPtr++;
                    }
                }
            }

            outputBitmap.UnlockBits(outputData);
            inputBitmap.UnlockBits(inputData);

            return outputBitmap;
        }
    }
}
