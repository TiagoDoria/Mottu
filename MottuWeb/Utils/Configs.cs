namespace MottuWeb.Utils
{
    public class Configs
    {
        public static string MotorcycleAPIBase {  get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
