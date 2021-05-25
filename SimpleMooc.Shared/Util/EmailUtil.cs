namespace SimpleMooc.Shared.Util
{
    public static class EmailUtil
    {
        public static string EmailNormalize(string email) => email.Trim().ToLower();
    }
}