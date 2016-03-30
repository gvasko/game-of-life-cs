using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGameOfLife;
using System.IO;

namespace GameOfLifeApp.Logic
{
    public class DefaultAppFactory : IAppFactory
    {
        private IDocFactory docFactory;

        public DefaultAppFactory(IDocFactory docFactory)
        {
            this.docFactory = docFactory;
        }

        public Image CreateLogo()
        {
            throw new NotImplementedException();
        }

        public IDocument LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException("File not found: " + path);
            }

            return docFactory.CreateDocument(docFactory.LoadLifeStateFromFile(path));
        }

        public IImageBuilder CreateImageBuilder()
        {
            throw new NotImplementedException();
        }

        public IApplication CreateApplicationLogic()
        {
            throw new NotImplementedException();
        }
    }
}
