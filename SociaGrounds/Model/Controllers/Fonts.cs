using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.Controllers
{
    public class Fonts
    {
        public static SpriteFont SmallFont { get; private set; }
        public static SpriteFont Medium { get; private set; }
        public static SpriteFont LargeFont { get; private set; }

        public static void CreateFonts(ContentManager content)
        {
            
            SmallFont = content.Load<SpriteFont>("SociaGrounds/SociaGroundsFont");
            Medium = content.Load<SpriteFont>("SociaGrounds/SociaGrounds_Font_Small");
            LargeFont = content.Load<SpriteFont>("SociaGrounds/SociaGrounds_Font_Large");
        }
    }
}
