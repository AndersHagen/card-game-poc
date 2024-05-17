using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public static class TextureManager
    {
        public static Texture2D CardBack;
        public static Texture2D CardFrame;
        public static Texture2D CardDecor;
        public static Texture2D CardDecor2;
        public static Texture2D CardHeartSymbol;
        public static Texture2D CardShieldSymbol;
        public static Texture2D CardSwordSymbol;
        public static Texture2D CardInfoBar;
        public static Texture2D CardStar;
        public static Texture2D CardStarframe;
        public static Texture2D CardTestImage;
        public static Texture2D CardSkeleMage;
        public static Texture2D BackgroundDarkFrost;
        public static Texture2D CardBoneGolem;
        public static Texture2D CardSkeletalWarlord;
        public static Texture2D CardSwampZombie;


        public static void Init(ContentManager contentManager)
        {
            CardBack = contentManager.Load<Texture2D>("gfx/cards/cardback1");
            CardFrame = contentManager.Load<Texture2D>("gfx/cards/frame");
            CardDecor = contentManager.Load<Texture2D>("gfx/cards/decor");
            CardDecor2 = contentManager.Load<Texture2D>("gfx/cards/decor2");
            CardHeartSymbol = contentManager.Load<Texture2D>("gfx/cards/heart");
            CardShieldSymbol = contentManager.Load<Texture2D>("gfx/cards/shield");
            CardSwordSymbol = contentManager.Load<Texture2D>("gfx/cards/sword");
            CardInfoBar = contentManager.Load<Texture2D>("gfx/cards/info_bar");
            CardStar = contentManager.Load<Texture2D>("gfx/cards/star");
            CardStarframe = contentManager.Load<Texture2D>("gfx/cards/star_frame");
            CardTestImage = contentManager.Load<Texture2D>("gfx/cards/testimage");
            CardSkeleMage = contentManager.Load<Texture2D>("gfx/cards/skele_mage");
            CardBoneGolem = contentManager.Load<Texture2D>("gfx/cards/bone_golem");
            CardSkeletalWarlord = contentManager.Load<Texture2D>("gfx/cards/skeleton_warlord");
            CardSwampZombie = contentManager.Load<Texture2D>("gfx/cards/swamp_zombie");

            BackgroundDarkFrost = contentManager.Load<Texture2D>("gfx/scenery/background");
        }

    }
}