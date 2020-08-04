using Android.Graphics;
using HopeEngine.Engine.Objects.Geometrics;
using System;

namespace HopeEngine.Engine.Objects.UI
{
    public class ColoredButton : ColoredSquare
    {

        private bool _isPressed = false;

        public Text Text;

        public Action OnClick;

        public ColoredButton(string text, Color? bgColor = null, Color? textColor = null) : base(bgColor ?? Color.Black)
        {
            Text = new Text(text, textColor ?? Color.White);
            Text.SetTextHorizontalAlign(TextHorizontalAlignment.CENTER);
            Text.SetTextVerticalAlign(TextVerticalAlignment.CENTER);
            AddComponents(Text);
        }

        public void SetText(string text)
        {
            Text.SetText(text);
        }

        public void SetTextColor(Color color)
        {
            Text.Color = color;
        }

        public override void OnFingerDown(float x, float y)
        {
            _isPressed = Transform.IsInside2D(x, y);
        }

        public override void OnFingerUp(float x, float y)
        {
            if (OnClick == null) return;
            if (_isPressed && Transform.IsInside2D(x, y))
            {
                OnClick.Invoke();
            }
            _isPressed = false;
        }

    }
}