using MongoDB.Bson;
using Realms;

public partial class Snake2dRank : RealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    [MapTo("name")]
    public string? Name { get; set; }

    [MapTo("score")]
    public double? Score { get; set; }

    [MapTo("time")]
    public string? Time { get; set; }

    [MapTo("total")]
    public double? Total { get; set; }


    public Snake2dRank()
    {
        Id = ObjectId.GenerateNewId();
    }
}



