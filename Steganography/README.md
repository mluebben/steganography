# Definition for Streams of images.

    public interface BitmapInputStream
    {
        Bitmap ReadBitmap();
    }

    public interface BitmapOutputStream
    {
        void WriteBitmap(Bitmap bmp);
    }



# Definitions for Byte streams.

    public interface OutputStream : IDisposable
    {
        void Write(byte[] data, int offset, int length);

        void Write(byte[] data)
        {
            Write(data, 0, data.Length);
        }

        void Flush();
    }

    public interface InputStream : IDisposable
    {
        int Read(byte[] data, int offset, int length);

        int Read(byte[] data)
        {
            return Read(data, 0, data.Length);
        }
    }