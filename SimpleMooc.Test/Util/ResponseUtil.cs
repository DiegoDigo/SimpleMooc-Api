using Microsoft.AspNetCore.Mvc;

namespace SimpleMooc.Test.Util
{
    public static class ResponseUtil
    {
        public static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T) ((ObjectResult) result.Result).Value;
        }

        public static int? GetObjectResultStatusCode<T>(ActionResult<T> result)
        {
            return ((ObjectResult) result.Result).StatusCode;
        }
    }
}