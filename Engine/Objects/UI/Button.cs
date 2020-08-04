using Android.Graphics;
using HopeEngine.Engine.Objects.Geometrics;
using HopeEngine.Engine.Textures;
using System;

namespace HopeEngine.Engine.Objects.UI
{
    public class Button : Square
    {

        private bool _isPressed = false;

        public Text Text;

        public Action OnClick;

        public Button(string text, Texture texture, Color? textColor = null) : base(texture)
        {
            Text = new Text(text, textColor ?? Color.White);
            // Text.Transform.Position.Y += 5; // TODO handle text alignment
            // Text.Transform.Position.X += 5;
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