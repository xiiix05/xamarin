using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Text;
using System;
using Android.Content;
using System.Collections.Generic;

namespace AndoridHellow
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        static readonly List<string> phoneNymbers = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            Button callHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            callHistoryButton.Click += (sender, e) =>
            {
                var intend = new Intent(this, typeof(CallHistoryActivity));
                intend.PutStringArrayListExtra("phone_numbers", phoneNymbers);
                StartActivity(intend);
            };

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);

            //전화걸기 버튼
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            callButton.Enabled = false;
            phoneNumberText.TextChanged +=
            (object sender, TextChangedEventArgs e) =>
            {
                if (!string.IsNullOrWhiteSpace(phoneNumberText.Text))
                    callButton.Enabled = true;
                else
                    callButton.Enabled = false;
            };
            callButton.Click += (object sender, EventArgs e) =>
            {
                //Make a Call 버튼 클릭시 전화를 건다.
                var callDialog = new Android.App.AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + phoneNumberText.Text + "?");
                //"Call"을 클릭하는 경우
                // 전화걸기 위한 인텐트 생성
                callDialog.SetNeutralButton("Call", delegate
                {
                    phoneNymbers.Add(phoneNumberText.Text);
                    callHistoryButton.Enabled = true;

                    // 인텐트는 액티비티의 전환이 일어날 때 호출하거나 메시지를 전달하는 매개체
                    // 암시적 인텐트 : 전환될 곳을 직접 지정하지 않고 액션을 적어서 사용한다.
                    // 명시적 인텐트 : 전환될 액티비티를 직접 적어서 표현하는 방법을 사용한다.
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("tel:" +
                   phoneNumberText.Text));
                    StartActivity(callIntent);
                });
                //Cancel을 클릭하는 경우
                callDialog.SetNegativeButton("Cancel", delegate { });
                callDialog.Show();
            };
        }
    }
}

