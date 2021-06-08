using System;
using System.Text.RegularExpressions;
using SimpleMooc.Shared.Extensions;

namespace SimpleMooc.Shared.Util
{
    public static class StringUtil
    {
        public static string GenerateSlug(string phrase) 
        {
            var number = new Random().Next(9999);
            var str = phrase.RemoveAccent().ToLower(); 
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); 
            str = Regex.Replace(str, @"\s+", " ").Trim(); 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();   
            str = Regex.Replace(str, @"\s", "-");   
            return $"{str}_{number}"; 
        } 
    }
}