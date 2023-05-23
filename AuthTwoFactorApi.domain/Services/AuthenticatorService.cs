using AuthTwoFactorApi.Domain.Interfaces;
using AuthTwoFactorApi.Domain.Models;
using AuthTwoFactorApi.Domain.Models.Return;
using Google.Authenticator;
using Microsoft.Extensions.Logging;
using System.Text;

namespace AuthTwoFactorApi.Domain.Services
{
    public class AuthenticatorService : IAuthenticatorService
    {
        private readonly ILogger<AuthenticatorService> _logger;
        private readonly IMailSenderHandler _mailSenderHandler;
        private readonly IAuthenticatorRepository _authenticatorRepository;
        private readonly TwoFactorAuthenticator _twoFactorAuthenticator;

        public AuthenticatorService(ILogger<AuthenticatorService> logger, IMailSenderHandler mailSenderHandler, IAuthenticatorRepository authenticatorRepository)
        {
            _logger = logger;
            _mailSenderHandler = mailSenderHandler;
            _twoFactorAuthenticator = new TwoFactorAuthenticator();
            _authenticatorRepository = authenticatorRepository;
        }


        public TwoFactorGeneretorResponse GenerationTwoFactorValidation(string userMail)
        {
            string key = RandomKeyGenerator();

            SetupCode setupInfo = _twoFactorAuthenticator.GenerateSetupCode("Test Two Factor", userMail, key, true, 3);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;

            TwoFactorGeneretorResponse twoFactorGeneretorResponse = new() { QrCode = qrCodeImageUrl, Code = manualEntrySetupCode };

            _mailSenderHandler.SendMail(_twoFactorAuthenticator.GetCurrentPIN(key, true),userMail);

            UserAuthCode user = new() { Key = key, Mail = userMail };

            _authenticatorRepository.InsertUser(user);

            return twoFactorGeneretorResponse;
        }

        public bool ValidatorEntryCode(string manualEntrySetupCode, string userMail)
        {
            var user = _authenticatorRepository.GetKeyByEmail(userMail);

            bool result = _twoFactorAuthenticator.ValidateTwoFactorPIN(user.Key, manualEntrySetupCode, true);

            return result;
        }

        private string RandomKeyGenerator()
        {
            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            StringBuilder keyBuilder = new();

            Random random = new();
            for (int i = 0; i < 16; i++)
            {
                int index = random.Next(base32Chars.Length);
                keyBuilder.Append(base32Chars[index]);
            }

            return keyBuilder.ToString();
        }
    }
}
