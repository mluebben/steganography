// SPDX-License-Identifier: GPL-3.0-or-later
//
// Copyright 2022 Matthias Lübben <ml81@gmx.de>
//

namespace Steganography
{
    public class BitOutputStream
    {
        private byte[] _data = new byte[32];
        int _byteIndex;
        int _bitIndex;

        public void WriteBit(int b)
        {
            if (_byteIndex >= _data.Length)
            {
                return;
            }

            _data[_byteIndex] = (byte)((_data[_byteIndex] >> 1) | (b == 1 ? (1 << 7) : 0));
            _bitIndex++;
            if (_bitIndex >= 8)
            {
                _bitIndex = 0;
                _byteIndex++;
            }
        }

        public byte[] Data
        {
            get { return _data; }
        }
    }
}
