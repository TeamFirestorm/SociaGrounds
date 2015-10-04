using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.Controllers
{
    public class Fonts
    {
        public static SpriteFont SmallFont;
        public static SpriteFont Medium;
        public static SpriteFont LargeFont;

        public static void CreateFonts(ContentManager content)
        {
            
            SmallFont = content.Load<SpriteFont>("SociaGrounds/SociaGroundsFont");
            Medium = content.Load<SpriteFont>("SociaGrounds/SociaGrounds_Font_Small");
            LargeFont = content.Load<SpriteFont>("SociaGrounds/SociaGrounds_Font_Large");
        }
    }
}
