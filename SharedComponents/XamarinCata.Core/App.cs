using XamarinCata.Helpers;
using XamarinCata.Interfaces;
using XamarinCata.Services;
using XamarinCata.Model;

namespace XamarinCata
{
    public partial class App
    {
        public App()
        {
        }

        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
        }
    }
}