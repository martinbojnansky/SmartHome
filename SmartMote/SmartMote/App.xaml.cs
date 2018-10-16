using SmartMote.Views.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;
using XamarinToolkit.IoC;

namespace SmartMote
{
    public partial class App : Application
    {
        public IoCResolver IoCResolver = new IoCResolver();

        public App()
        {
            InitializeComponent();

            IoCResolver.BuildContainer(new Assembly[] {
                typeof(App).GetTypeInfo().Assembly,             
            });

            MainPage = new NavigationPage(new MainView())
            {
                BarBackgroundColor = Color.FromHex("#3f51b5"),
                BarTextColor = Color.White
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}