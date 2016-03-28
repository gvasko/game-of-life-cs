using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    internal class DefaultDocument : IDocument
    {
        private ILife life;
        private ILifeState initialState;
        private ILifeState currentState;

        public DefaultDocument(IFactory factory, ILifeState initialState)
        {
            life = factory.CreateLife();
            life.AddRules(factory.GetDefaultRuleSet());
            this.initialState = initialState;
            currentState = this.initialState;
        }

        public ILifeState CurrentState
        {
            get
            {
                return currentState;
            }
            private set
            {
                if (currentState != value)
                {
                    currentState = value;
                    Notify();
                }
            }
        }

        private void Notify()
        {
            var localHandler = CurrentStateChanged;
            if (localHandler != null)
            {
                localHandler(this, EventArgs.Empty);
            }
        }

        public event EventHandler CurrentStateChanged;

        public void NextState()
        {
            CurrentState = life.CalculateNextState(CurrentState);
        }

        public void Reset()
        {
            CurrentState = initialState;
        }

    }
}
