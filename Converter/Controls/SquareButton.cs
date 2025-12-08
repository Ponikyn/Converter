using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Converter.Controls
{
    public class SquareButton : Button
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            // Make button a square based on the minimum of available width/height
            var size = Math.Min(widthConstraint <= 0 ? double.PositiveInfinity : widthConstraint,
                                heightConstraint <= 0 ? double.PositiveInfinity : heightConstraint);

            if (double.IsInfinity(size) || double.IsNaN(size) || size <= 0)
            {
                return base.OnMeasure(widthConstraint, heightConstraint);
            }

            return new SizeRequest(new Size(size, size));
        }
    }
}
