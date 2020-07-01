using System.ComponentModel;
using Xamarin.Forms;

namespace ShadowExperiment
{
    using SkiaSharp;
    using SkiaSharp.Views.Forms;

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var skCanvas = e.Surface.Canvas;
            var skinfo = e.Info;
            skCanvas.Clear();


            var RectangleStyleFillShadowColor = new SKColor(24, 24, 24, 70);

            var RectangleStyleFillShadow = SKImageFilter.CreateDropShadow(
                (float)SliderShadowX.Value, (float)SliderShadowY.Value,
                (float)SliderSigmaX.Value, (float)SliderSigmaY.Value,
                RectangleStyleFillShadowColor,
                SKDropShadowImageFilterShadowMode.DrawShadowAndForeground,
                SKImageFilter.CreateBlur((float)SliderBlurX.Value, (float)SliderBlurY.Value), null);


            var RectangleStyleFillPaint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                BlendMode = SKBlendMode.SrcOver,
                IsAntialias = true,
                ImageFilter = RectangleStyleFillShadow
            };

            var shadowMargin = (float)SliderShadowMargin.Value;

            skCanvas.DrawRect(new SKRect(shadowMargin, shadowMargin, skinfo.Width - shadowMargin, skinfo.Height - shadowMargin), RectangleStyleFillPaint);
        }

        private void Slider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            CanvasView.InvalidateSurface();
        }
    }
}