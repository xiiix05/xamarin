using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinFormPhoneWorld.iOS;

[assembly: Dependency(typeof(PhoneDialer))]
namespace XamarinFormPhoneWorld.iOS
{        
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + number));
        }
    }

}