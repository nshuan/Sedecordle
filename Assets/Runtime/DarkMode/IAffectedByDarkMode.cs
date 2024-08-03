using DG.Tweening;
using Runtime.Const;

namespace Runtime.DarkMode
{
    public interface IAffectedByDarkMode
    {
        Tween DoChangeColorMode(ColorConst colorPalette);
    }
}