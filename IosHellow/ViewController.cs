using Foundation;
using System;
using UIKit;

namespace IosHellow
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            PhoneNumberText.ResignFirstResponder();

            CallButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                var url = new NSUrl("tel:" + PhoneNumberText.Text);
                
                // Use URL handler를 호출하는데 "tel:" 키워드는 애플의 Phone app을 호출하고 그렇지 않으면 경고창을 띄운다. 
                //본 예제는 시뮬레이터에서 동작하므로 경 고창이 로드된다.         
                if (!UIApplication.SharedApplication.OpenUrl(url))
                {
                    //경고창을 띄운다.    
                    var alert =
                    UIAlertController.Create("Not Supported", "Schema 'tel:' is not supported on this device", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, true, null);
                }
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}