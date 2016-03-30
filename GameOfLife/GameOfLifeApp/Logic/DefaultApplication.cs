using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeApp.Logic
{
    internal class DefaultApplication : IApplication
    {
        private IAppFactory factory;

        public DefaultApplication(IAppFactory factory)
        {
            this.factory = factory;
        }

        public int CellSize
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string File
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Image Image
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler CellSizeChanged;
        public event EventHandler FileChanged;
        public event EventHandler ImageChanged;

        public void NextImage()
        {
            throw new NotImplementedException();
        }

        public void ProcessCommandLineArguments(string[] args)
        {
            throw new NotImplementedException();
        }

        public void ResetImage()
        {
            throw new NotImplementedException();
        }

        public void StartAnimation()
        {
            throw new NotImplementedException();
        }

        public void StopAnimation()
        {
            throw new NotImplementedException();
        }
    }
}
