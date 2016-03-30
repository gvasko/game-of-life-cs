using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{
    public interface IDocFactory
    {
        ILifeState LoadLifeStateFromFile(string path);
        ILifeState CreateLifeState(Cell[] cells);

        LifeRule[] GetDefaultRuleSet();

        ILife CreateLife();

        IDocument CreateDocument(ILifeState initialState);

    }
}
