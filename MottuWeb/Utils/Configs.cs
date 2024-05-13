namespace MottuWeb.Utils
{
    public class Configs
    {
        public static string MotorcycleAPIBase {  get; set; }
        public static string LocationAPIBase {  get; set; }
        public static string OrderAPIBase {  get; set; }
        public static string AuthAPIBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleDeliveryman = "DELIVERYMAN";
        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
