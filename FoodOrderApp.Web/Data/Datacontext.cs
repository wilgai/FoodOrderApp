using FireSharp.Config;
using FireSharp.Interfaces;

namespace FoodOrderApp.Web.Data
{
    public class Datacontext
    {
        public IFirebaseConfig GetFirebaseConnection()
        {

            IFirebaseConfig config = new FirebaseConfig()
            {

                AuthSecret = "WGrhhQphcYKSODUzqJaK8Dor4HeMclTDMDBqDVyR",
                BasePath = "https://delevery-cb7c7.firebaseio.com/"
            };

            return config;
        }
    }
}
