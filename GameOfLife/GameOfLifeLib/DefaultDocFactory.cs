using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    public sealed class DefaultDocFactory : IDocFactory
    {
        private static DefaultDocFactory factorySoleInstance;

        public static IDocFactory GetFactory()
        {
            // TODO: not thread safe
            if (factorySoleInstance == null)
            {
                factorySoleInstance = new DefaultDocFactory();
            }

            return factorySoleInstance;
        }

        public IDocument CreateDocument(ILifeState initialState)
        {
            return new DefaultDocument(this, initialState);
        }

        public ILife CreateLife()
        {
            return new DefaultLife(this);
        }

        public ILifeState CreateLifeState(Cell[] cells)
        {
            return new DefaultLifeState(cells);
        }

        public LifeRule[] GetDefaultRuleSet()
        {
            return new LifeRule[] {
                DefaultRules.ApplyUnderPopulationRule,
                DefaultRules.ApplyNormalPopulationRule,
                DefaultRules.ApplyOverPopulationRule,
                DefaultRules.ApplyReproductionRule };
        }

        public ILifeState LoadLifeStateFromFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
