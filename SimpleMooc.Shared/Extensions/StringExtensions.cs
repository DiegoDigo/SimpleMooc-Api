namespace SimpleMooc.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAccent(this string txt)
        {
            var bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt); 
            return System.Text.Encoding.ASCII.GetString(bytes); 
        }
    }
}