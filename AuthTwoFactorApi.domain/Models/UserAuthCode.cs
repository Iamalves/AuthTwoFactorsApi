using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthTwoFactorApi.Domain.Models
{
    public class UserAuthCode
    {
        [BsonElement("_id")]
        public ObjectId? Id { get; set; }
        [BsonElement("_mail")]
        public string Mail { get; set; } = string.Empty;
        [BsonElement("_key")]
        public string Key { get; set; } = string.Empty;
    }
}
