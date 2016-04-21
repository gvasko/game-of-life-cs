using IGameOfLife;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    public sealed class DefaultDocFactory : IDocFactory
    {
        private static DefaultDocFactory factorySoleInstance;

        public static DefaultDocFactory GetFactory()
        {
            // TODO: not thread safe
            if (factorySoleInstance == null)
            {
                factorySoleInstance = new DefaultDocFactory();
            }

            return factorySoleInstance;
        }

        private DefaultDocFactory()
        {
            // make it private
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

        // TODO: untested code
        // TODO: I don't like the error handling
        public ILifeState LoadLifeStateFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            IList<Cell> cells = new List<Cell>();
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("#"))
                {
                    continue;
                }
                string[] components = trimmedLine.Split(' ');
                if (components.Length == 2)
                {
                    try
                    {
                        int x = Int32.Parse(components[0]);
                        int y = Int32.Parse(components[1]);
                        cells.Add(new Cell(x, y));
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }
            }

            return CreateLifeState(cells.ToArray());
        }
    }
}
