using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueRuby
{
    public abstract class Card
    {
        public static Dictionary<string, Type> Cards => new Dictionary<string, Type>()
        {
            { "FiveGoldCard", typeof(FiveGoldCard) },
            { "TenGoldCard", typeof(TenGoldCard) },
            { "SpellMagicFire", typeof(SpellMagicFire) },
            { "ShieldTaygarol", typeof(ShieldTaygarol) },
            { "DragonKhiragir", typeof(DragonKhiragir) },
            { "WizardCard", typeof(WizardCard) }
        };

        public static Image GetTexture(Point location) =>
            Images.GetFragment(Properties.Resources.Cards, new Size(128, 224), new Rectangle(location, new Size(200, 350)));
        public abstract Image Texture { get; }
        public static Image CloseTexture => GetTexture(new Point(0, 0));

        public static Size CardSize => new Size(64, 112);

        public override string ToString() => $"{GetType().Name}";
    }

    public class WizardCard : Card
    {
        public override Image Texture => GetTexture(new Point(200, 350));
        public WizardCard()
        { }
    }

    public class DragonKhiragir : Card
    {
        public override Image Texture => GetTexture(new Point(0, 350));
        public DragonKhiragir()
        { }
    }

    public class SpellMagicFire : Card
    {
        public override Image Texture => GetTexture(new Point(600, 0));
        public SpellMagicFire()
        { }
    }

    public class ShieldTaygarol : Card
    {
        public override Image Texture => GetTexture(new Point(800, 0));
        public ShieldTaygarol()
        { }
    }

    public abstract class GoldCard : Card
    {
        public int Value { get; protected set; }
    }
    public class FiveGoldCard : GoldCard
    {
        public override Image Texture => GetTexture(new Point(200, 0));
        public FiveGoldCard()
        { Value = 5; }
    }
    public class TenGoldCard : GoldCard
    {
        public override Image Texture => GetTexture(new Point(400, 0));
        public TenGoldCard()
        { Value = 10; }
    }

    public class BlueRubyCard : Card
    {
        public override Image Texture => !IsOpen ? GetTexture(new Point(0, 700)) :
                                          IsReal ? GetTexture(new Point(400, 700)) :
                                                   GetTexture(new Point(200, 700));
        public static new Image CloseTexture => new BlueRubyCard(false).Texture;
        public bool IsReal { get; }
        public bool IsOpen { get; private set; } = false;
        public void Open() => IsOpen = true;

        public BlueRubyCard(bool isReal)
        {
            IsReal = isReal;
        }
    }

    public static class LiveCard
    {
        public static Image CloseTexture => Card.GetTexture(new Point(400, 350));
        public static Image OpenTexture => Card.GetTexture(new Point(600, 350));
    }
}
