using Realms;
using Realms.Sync;
using System.Threading.Tasks;

public class RealmController
{
    private Realm realm;
    private readonly string myRealmAppId = "snake2d-xvdjuei";
    private readonly string apiKey = "cgCtzmdls07V7tdJwdMTBek18ZieDQE698q7xfse7RWnFzUYigU4eJjzNboxGimS";

    public RealmController()
    {
        InitAsync();
    }
    private async void InitAsync()
    {
        var app = App.Create(myRealmAppId);
        User user = await Get_userAsync(app);
        //PartitionSyncConfiguration config = GetConfig(user);
        var config = new FlexibleSyncConfiguration(app.CurrentUser);
        realm = await Realm.GetInstanceAsync(config);



        // Add the subscription
        realm.Subscriptions.Update(() =>
        {
            var query = realm.All<Snake2dRank>();
            realm.Subscriptions.Add(query);
        });
        await realm.Subscriptions.WaitForSynchronizationAsync();
    }

    private async Task<User> Get_userAsync(App app)
    {
        User user = app.CurrentUser;
        if (user == null)
        {
            user = await app.LogInAsync(Credentials.ApiKey(apiKey));
        }
        return user;
    }

    /*
    private PartitionSyncConfiguration GetConfig(User user)
    {
        var config = new FlexibleSyncConfiguration("Snake2dGame", user);

        config.ClientResetHandler = new DiscardLocalResetHandler()
        {
            ManualResetFallback = (ClientResetException ClientResetException) => ClientResetException.InitiateClientReset()
        };
        return config;
    }
    */

    public void AddRecord(string playerName, int score, string time, int totalScore)
    {
        if (realm == null)
        {
            UnityEngine.Debug.Log("Realm not ready");
            return;
        }
        realm.Write(() =>
        {
            //Add record
            realm.Add(new Snake2dRank()
            {
                Name = playerName,
                Score = score,
                Time = time,
                Total = totalScore
            });
            UnityEngine.Debug.Log("Added");

        });

    }

    public void Terminate()
    {
        realm?.Dispose();
    }
}
