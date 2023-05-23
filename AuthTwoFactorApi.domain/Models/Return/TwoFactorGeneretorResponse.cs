

namespace AuthTwoFactorApi.Domain.Models.Return
{
    public class TwoFactorGeneretorResponse
    {
        public TwoFactorGeneretorResponse() { }

        public string Code { get; set; }
        public string QrCode { get; set; }

    }
}
