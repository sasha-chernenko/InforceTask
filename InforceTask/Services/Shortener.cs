using System.Text;

namespace InforceTask.Services
{
    public class Shortener
    {
        private static string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static int BASE = ALPHABET.Length;

        public static string Shorten(int num)
        {
            var shortened = "";
            while (num > 0)
            {
                shortened += ALPHABET[num % BASE];
                num /= BASE;
            }
            char[] charArray = shortened.ToCharArray();
            Array.Reverse(charArray);
            shortened = new string(charArray);
            return shortened;
        }
    }
}
