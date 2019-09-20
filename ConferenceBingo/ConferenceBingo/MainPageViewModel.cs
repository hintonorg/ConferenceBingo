using Xamarin.Forms;
using ConferenceBingo;

namespace ConferenceBingo
{
	public class MainPageViewModel: BindableObject
    {

        //public string AdUnitId { get; set; } = "ca-app-pub-3940256099942544/6300978111";  //Android
        //public string AdUnitId { get; set; } = "ca-app-pub-3940256099942544/2934735716";    //IOS
        public string AdUnitId { get; set; }

        public MainPageViewModel()
        {
            if (Device.RuntimePlatform == Device.iOS)
                AdUnitId = "ca-app-pub-3940256099942544/2934735716";
            else if (Device.RuntimePlatform == Device.Android)
                AdUnitId = "ca-app-pub-3940256099942544/6300978111";
        }
	}
}
