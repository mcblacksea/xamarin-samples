using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using PaperBoy.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(Image), typeof(CircleImageRender))]
namespace PaperBoy.Droid.Renders
{
    public class CircleImageRender:ImageRenderer
    {
        public CircleImageRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if((int)Build.VERSION.SdkInt<18)
                    SetLayerType(LayerType.Software,null);
            }
        }

        protected override bool DrawChild(Canvas canvas, View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;

                //path to clip
                var path = new Path();
                path.AddCircle(Width/2,Height/2,radius,Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                //path for circle border
                path=new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                
                var paint=new Paint();
                paint.AntiAlias = true;
                paint.StrokeWidth = 5;
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color = global::Android.Graphics.Color.White;

                canvas.DrawPath(path,paint);

                //properly dispose
                paint.Dispose();
                path.Dispose();
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}