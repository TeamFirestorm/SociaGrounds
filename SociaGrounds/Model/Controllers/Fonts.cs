using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.Controllers
{
    public class Fonts
    {
        public static SpriteFont SmallFont;
        public static SpriteFont NormalFont;
        public static SpriteFont LargeFont;

        public static void CreateFonts(ContentManager content)
        {
            SmallFont = content.Load<SpriteFont>("SociaGrounds/SociaGrounds_Font_Small");
            NormalFont = content.Load<SpriteFont>("SociaGrounds/SociaGroundsFont");
            LargeFont = content.Load<SpriteFont>("SociaGrounds/SociaGrounds_Font_Large");
        }
    }
}
