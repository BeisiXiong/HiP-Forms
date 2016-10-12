using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Text;
using Android.Views;
using Android.Widget;
using de.upb.hip.mobile.droid.fragments;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace de.upb.hip.mobile.droid.Activities {
    [Activity (MainLauncher = true, Theme = "@style/AppTheme")]
    public class AutoCarouselPageActivity : FragmentActivity, ViewPager.IOnPageChangeListener {

        // number of pages

        private const int NUM = 3;
        private Button btnSkip, btnNext;
        private TextView[] dots;

        //things for autocarausel
        private LinearLayout dotsLayout;

        //page adapater; provide pages to the ViewPager
        private PagerAdapter pagerAdapter;

        private PreferenceManager prefManager;

        //
        private ISharedPreferences sharedPreferences;

        // pager wigdet, handles animatin and aloow swiping horizontally
        private ViewPager viewPager;

        public void OnPageScrollStateChanged (int state)
        {
        }

        public void OnPageScrolled (int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageSelected (int position)
        {
            AddButtomDots (position);

            if (position == NUM - 1)
            {
                btnSkip.Visibility = ViewStates.Gone;
                btnNext.Text = GetString (Resource.String.start);
            }
            else
            {
                btnSkip.Visibility = ViewStates.Visible;
                btnNext.Text = GetString (Resource.String.next);
            }
        }

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences (this);

            if (!sharedPreferences.GetBoolean (Resources.GetString (Resource.String.pref_first_time_launch_key), true))
            {
                launchSplashScreen ();
                Finish ();
            }

            // Create your fragment here
            SetContentView (Resource.Layout.activity_auto_carousel);
            viewPager = FindViewById<ViewPager> (Resource.Id.view_pager);
            pagerAdapter = new ScreenSlidePageAdapter (SupportFragmentManager);
            viewPager.Adapter = pagerAdapter;

            viewPager.AddOnPageChangeListener (this);

            dotsLayout = FindViewById<LinearLayout> (Resource.Id.layoutDots);
            btnSkip = FindViewById<Button> (Resource.Id.btn_skip);
            btnNext = FindViewById<Button> (Resource.Id.btn_next);

            MakeNotificationBarTransparent ();

            AddButtomDots (0);

            btnNext.Click += delegate {
                var current = viewPager.CurrentItem + 1;
                if (current < NUM)
                {
                    viewPager.CurrentItem = current;
                }
                else
                {
                    launchSplashScreen ();
                }
            };

            btnSkip.Click += delegate { launchSplashScreen (); };
        }

        private void launchSplashScreen ()
        {
            var edit = sharedPreferences.Edit ();
            edit.PutBoolean (Resources.GetString (Resource.String.pref_first_time_launch_key), false);
            edit.Commit ();
            StartActivity (typeof (SplashScreenActivity));
            Finish ();
        }

        private void MakeNotificationBarTransparent ()
        {
            /*
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                View decor = Window.DecorView;
                decor.SystemUiVisibility = StatusBarVisibility.Hidden;
                
                Window.SetStatusBarColor(Resources.GetColor(Resource.Color.colorPrimaryDark));
            }
            */
        }

        public void AddButtomDots (int currentPage)
        {
            dots = new TextView[NUM];

            dotsLayout.RemoveAllViews (); //proveri zasto je ovo potrebno

            for (var i = 0; i < NUM; i++)
            {
                dots [i] = new TextView (this);
                dots [i].Text = Html.FromHtml ("&#8226;").ToString ();
                dots [i].TextSize = 35;
                dots [i].SetTextColor (Color.Black);
                dotsLayout.AddView (dots [i]);
            }

            dots [currentPage].SetTextColor (Color.White);
        }

        public override void OnBackPressed ()
        {
            if (viewPager.CurrentItem == 0)
            {
                base.OnBackPressed ();
            }
            else
            {
                viewPager.CurrentItem = viewPager.CurrentItem - 1;
            }
        }

        private class ScreenSlidePageAdapter : FragmentStatePagerAdapter {

            public ScreenSlidePageAdapter (FragmentManager fm) : base (fm)
            {
            }

            public override int Count {
                get { return NUM; }
            }

            public override Fragment GetItem (int position)
            {
                return AutoCarouselPageFragment.Create (position);
                //return new ScreenSlidePageFragment();
            }

        }

    }
}