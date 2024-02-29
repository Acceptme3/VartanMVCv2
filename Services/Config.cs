namespace VartanMVCv2.Services
{
    public partial class Config
    {
        public static string ConnectionString { get; set; } = null!;
        public static PhotoMode PhotoMode { get; set; }
        public static string CompanyName { get; set; } = null!;
        public static string CompanyPhone { get; set; } = null!;
        public static string CompanySecondPhone { get; set; } = null!;
        public static string CompanyEmail { get; set; } = null!;
        public static string CompanyPhoneShort { get; set; } = null!;
        public static string CompanySecondPhoneShort { get; set; } = null!;
        public static string CompanyAddress { get; set; } = null!;
        public static string CompanyWorkTime { get; set; } = null!;
        public static string CompanyCooperationPhone { get; set; } = null!;
        public static string CompanyCooperationPhoneShort { get; set; } = null!;
    }
   public enum PhotoMode {group, single }

}
