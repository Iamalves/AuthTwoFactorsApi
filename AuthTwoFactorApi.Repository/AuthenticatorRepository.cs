using AuthTwoFactorApi.Domain.Interfaces;
using AuthTwoFactorApi.Domain.Models;
using MongoDB.Driver;

namespace AuthTwoFactorApi.Repository
{
    public class AuthenticatorRepository : IAuthenticatorRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<UserAuthCode> _collection;
        public AuthenticatorRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            var database = _mongoClient.GetDatabase("users");
            _collection = database.GetCollection<UserAuthCode>("auth");

        }

        public void InsertUser(UserAuthCode model)
        {
            var filter = Builders<UserAuthCode>.Filter.Eq("_mail", model.Mail);
            var update = Builders<UserAuthCode>.Update.Set("_key", model.Key);
            _collection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });

        }

        public UserAuthCode GetKeyByEmail(string mail)
        {
            var filter = Builders<UserAuthCode>.Filter.Eq("_mail", mail);
            var document = _collection.Find(filter).FirstOrDefault();
            return document;
        }
    }
}