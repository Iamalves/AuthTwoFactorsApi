

namespace AuthTwoFactorApi.Domain.Models.Retorno
{
    public class GenericResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public long StatusCode { get; set; }

    }
}
