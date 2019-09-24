using System.ComponentModel;
using ConferenceBingo;
using ConferenceBingo.Droid;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Ads;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace ConferenceBingo.Droid
{
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        public AdMobViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control == null)
            {
                SetNativeControl(CreateAdView());
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(AdView.AdUnitId))
                Control.AdUnitId = Element.AdUnitId;
        }

        private AdView CreateAdView()
        {
            var adView = new AdView(Context)
            {
                AdSize = AdSize.SmartBanner,
                AdUnitId = Element.AdUnitId
            };

            adView.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

            adView.LoadAd(new AdRequest.Builder().Build());
            //adView.LoadAd(new AdRequest.Builder().AddTestDevice("27E3A324EEBFB7047EAFE09ADC0E59FA").Build());   //Added for the test device 09/23/2019--REMOVE before production.

            return adView;
        }
    }
}
