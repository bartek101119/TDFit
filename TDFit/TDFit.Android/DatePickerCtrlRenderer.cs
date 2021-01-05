using System;
using Android.Graphics.Drawables;
using TDFit.CustomControls;
using TDFit.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]
namespace TDFit.Droid
{
    [Obsolete]
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

           DatePickerCtrl element = Element as DatePickerCtrl;
            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }
            this.Control.TextChanged += (sender, arg) => {
                var selectedDate = arg.Text.ToString();
                if (selectedDate == element.Placeholder)
                {
                    Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            };
        }
    }
}