// SPDX-License-Identifier: GPL-3.0-or-later
//
// Copyright 2022 Matthias Lübben <ml81@gmx.de>
//

namespace Steganography
{
    public class BitInputStream
    {
        private readonly byte[] _data;
        private byte _currentByte;
        private int _byteIndex;
        private int _bitIndex;

        public BitInputStream(byte[] data)
        {
            _data = data;
            _currentByte = _data[0];
            _byteIndex = 0;
            _bitIndex = 0;
        }

        public int ReadBit()
        {
            if (_byteIndex >= _data.Length)
            {
                return -1;
            }

            int bit = _currentByte & 1;
            _currentByte = (byte)(_currentByte >> 1);
            _bitIndex++;

            if (_bitIndex >= 8)
            {
                _bitIndex = 0;
                _byteIndex++;
                if (_byteIndex < _data.Length)
                {
                    _currentByte = _data[_byteIndex];
                }
            }

            return bit;
        }
    }
}
