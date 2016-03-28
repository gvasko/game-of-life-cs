using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{

    /// <summary>
    /// Defines a rule in the game.
    ///
    /// </summary>
    /// <param name="state">The state of the game where to apply the rule </param>
    /// <param name="cell">The current cell that status needs to be updated</param>
    /// <returns>The new CellStatus, null if the rule is not applicable</returns>
    public delegate CellStatus LifeRule(ILifeState state, Cell cell);

    public interface ILife
    {
        void AddRule(LifeRule rule);
        void AddRules(LifeRule[] rules);

        ILifeState CalculateNextState(ILifeState currentState);
    }
}
