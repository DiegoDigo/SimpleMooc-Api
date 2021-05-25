namespace SimpleMooc.Shared.Util
{
    public  static class PasswordUtil
    {
        public static string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool Verify(string password, string hashPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}