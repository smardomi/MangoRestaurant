namespace Mango.Web
{
    public static class ApiUtil
    {
        public static string ProductAPIBase { get; set; }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
