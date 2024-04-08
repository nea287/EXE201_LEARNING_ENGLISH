using System.Drawing;

namespace EXE201_LEARNING_ENGLISH_Client
{
    public static class Utils
    {
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                return Image.FromStream(memoryStream);
            }
        }
    }
}
