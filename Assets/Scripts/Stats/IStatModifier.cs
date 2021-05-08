using System.Collections;

namespace RPG.Stats
{
  public interface IStatModifier
  {
    IEnumerable GetAdditiveModifiers(Stat stat);
    IEnumerable GetMultiplicativeModifiers(Stat stat);
  }
}
