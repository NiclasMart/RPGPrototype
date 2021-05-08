using System.Collections;

namespace RPG.Stats
{
  public interface IStatModifier
  {
    IEnumerable GetAdditiveModifier(Stat stat);
  }
}
