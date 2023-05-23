using AuthTwoFactorApi.Domain.Models.Retorno;
using Microsoft.AspNetCore.Mvc;
using AuthTwoFactorApi.Domain.Models.Return;
using AuthTwoFactorApi.Domain.Interfaces;

namespace AuthTwoFactorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutheticationController : ControllerBase
    {

        private readonly ILogger<AutheticationController> _logger;
        private readonly IAuthenticatorService _authenticatorService;
        public AutheticationController(ILogger<AutheticationController> logger, IAuthenticatorService authenticatorService)
        {
            _logger = logger;
            _authenticatorService = authenticatorService;
        }

        [HttpPost("LoginTwoFactorValid", Name = "LoginTwoFactorValid")]
        public GenericResponse LoginTwoFactorValid(string userEnteredCode, string userMail)
        {
            var result = _authenticatorService.ValidatorEntryCode(userEnteredCode, userMail);
            if (result)
            {
                var response = new GenericResponse() { Message = "Welcome", Status = "Sucess", StatusCode = 200 };
                _logger.LogInformation(response.Message);
                return response;

            }
            return new GenericResponse();
        }


        [HttpPost("LoginTwoFactor", Name = "LoginTwoFactor")]
        public TwoFactorGeneretorResponse LoginTwoFactor(string userMail)
        {
            var result = _authenticatorService.GenerationTwoFactorValidation(userMail);

            return result;
        }

    }
}