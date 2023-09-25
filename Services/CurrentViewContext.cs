namespace VartanMVCv2.Services
{
    public class CurrentViewContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public string? CurrentViewName
        {
            get { return _contextAccessor.HttpContext!.Session.GetString("MyCustomData"); }
            set { _contextAccessor.HttpContext!.Session.SetString("MyCustomData", value!); }
        }

        public CurrentViewContext(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
    }
}
