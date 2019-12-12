using System.Drawing;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public sealed class Text : IElement
    {

        public bool Enabled { get; set; }
        public Color Color { get; set; }
        public Vector2 Position { get; set; }

        private long _captionId;
        private string _caption;

        public string Caption
        {
            get => _caption;
            set {
                _caption = value;
                _captionId = Function.Call<long>((Hash)0xFA925AC00EB830B9, 10, "LITERAL_STRING", _caption);
            }
        }

        public Alignment Alignment { get; set; }
        public bool Shadow { get; set; }
        public bool Outline { get; set; }
        public float Scale { get; set; }
        public Font Font { get; set; }
        public float WrapWidth { get; set; }

        public bool Centered
        {
            get => Alignment == Alignment.Center;
            set => Alignment = value ? Alignment.Center : Alignment.Left;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class used for drawing text on the screen.
        /// </summary>
        /// <param name="caption">The <see cref="Text"/> to draw.</param>
        /// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="Text"/>.</param>
        /// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="Text"/>, for no scaling use 1.0f.</param>	
        public Text(string caption, Vector2 position, float scale) : this(caption, position, scale, Color.FromArgb(255, 255, 255, 255), Font.Unk0, Alignment.Left, false, false, 0.0f)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class used for drawing text on the screen.
        /// </summary>
        /// <param name="caption">The <see cref="Text"/> to draw.</param>
        /// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="Text"/>.</param>
        /// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="Text"/>, for no scaling use 1.0f.</param>
        /// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="Text"/>.</param>							 
        /// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>	
        public Text(string caption, Vector2 position, float scale, Color color, Font font) : this(caption, position, scale, color, font, Alignment.Left, false, false, 0.0f)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class used for drawing text on the screen.
        /// </summary>
        /// <param name="caption">The <see cref="Text"/> to draw.</param>
        /// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="Text"/>.</param>
        /// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="Text"/>, for no scaling use 1.0f.</param>
        /// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="Text"/>.</param>							 
        /// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>
        /// <param name="alignment">Sets the <see cref="Alignment"/> used when drawing the text, <see cref="External.Alignment.Left"/>,<see cref="External.Alignment.Center"/> or <see cref="External.Alignment.Right"/>.</param>
        public Text(string caption, Vector2 position, float scale, Color color, Font font, Alignment alignment) : this(caption, position, scale, color, font, alignment, false, false, 0.0f)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class used for drawing text on the screen.
        /// </summary>
        /// <param name="caption">The <see cref="Text"/> to draw.</param>
        /// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="Text"/>.</param>
        /// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="Text"/>, for no scaling use 1.0f.</param>
        /// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="Text"/>.</param>							 
        /// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>
        /// <param name="alignment">Sets the <see cref="Alignment"/> used when drawing the text, <see cref="External.Alignment.Left"/>,<see cref="External.Alignment.Center"/> or <see cref="External.Alignment.Right"/>.</param>
        /// <param name="shadow">Sets whether or not to draw the <see cref="Text"/> with a <see cref="Shadow"/> effect.</param>
        /// <param name="outline">Sets whether or not to draw the <see cref="Text"/> with an <see cref="Outline"/> around the letters.</param>	
        public Text(string caption, Vector2 position, float scale, Color color, Font font, Alignment alignment, bool shadow, bool outline) : this(caption, position, scale, color, font, alignment, shadow, outline, 0.0f)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class used for drawing text on the screen.
        /// </summary>
        /// <param name="caption">The <see cref="Text"/> to draw.</param>
        /// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="Text"/>.</param>
        /// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="Text"/>, for no scaling use 1.0f.</param>
        /// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="Text"/>.</param>							 
        /// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>
        /// <param name="alignment">Sets the <see cref="Alignment"/> used when drawing the text, <see cref="External.Alignment.Left"/>,<see cref="External.Alignment.Center"/> or <see cref="External.Alignment.Right"/>.</param>
        /// <param name="shadow">Sets whether or not to draw the <see cref="Text"/> with a <see cref="Shadow"/> effect.</param>
        /// <param name="outline">Sets whether or not to draw the <see cref="Text"/> with an <see cref="Outline"/> around the letters.</param>
        /// <param name="wrapWidth">Sets how many horizontal pixel to draw before wrapping the <see cref="Text"/> on the next line down.</param>											 																	  
        public Text(string caption, Vector2 position, float scale, Color color, Font font = Font.Unk0, Alignment alignment = Alignment.Left, bool shadow = false, bool outline = false, float wrapWidth = 0.0f)
        {
            Enabled = true;
            Caption = caption;
            Position = position;
            Scale = scale;
            Color = color;
            Font = font;
            Alignment = alignment;
            Shadow = shadow;
            Outline = outline;
            WrapWidth = wrapWidth;
        }



        public void Draw(Vector2 offset = default)
        {
            InternalDraw(offset);
        }

        public void ScaledDraw(Vector2 offset = default)
        {
            InternalDraw(offset);
        }

        private void InternalDraw(Vector2 offset)
        {
            Function.Call(Hash.SET_TEXT_SCALE, Scale, Scale);

            if (Shadow)
                Function.Call((Hash)0x1BE39DBAA7263CA5, 0.001, 0, 0, 120);

            Function.Call((Hash)0xBE5261939FBECB8C, Centered);
            Function.Call((Hash)0x50a41ad966910f03, Color.R, Color.G, Color.B, Color.A);
            Function.Call((Hash)0xADA9255D, (int)Font);
            Function.Call((Hash)0xd79334a4bb99bad1, _captionId, Position.X + offset.X, Position.Y + offset.Y);
        }
    }

    public enum Font
    {
        Unk0 = 0,
        Unk1,
        Unk2,
        Unk3,
        Unk4,
        Unk5,
        Unk6,
        Unk7,
        Unk8,
        Unk9,
        Unk10
    }

    public enum Alignment
    {
        Center = 0,
        Left
    }

}
