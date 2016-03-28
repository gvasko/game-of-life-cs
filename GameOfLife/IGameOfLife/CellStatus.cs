using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{
    public sealed class CellStatus
    {
        public static CellStatus Alive
    	{
	    	get { return alive; }
        }

        public static CellStatus Dead
    	{
	    	get { return dead; }
    	}
	
	    private static CellStatus alive = new CellStatus("Alive");
        private static CellStatus dead = new CellStatus("Dead");

        private string statusName;

        private CellStatus(string statusName)
        {
            this.statusName = statusName;
        }

        public override string ToString()
        {
            return statusName;
        }

    }
}
