using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGameOfLife;
using System.IO;
using GameOfLifeLib;

namespace GameOfLifeApp.Logic
{
    public sealed class DefaultAppFactory : IAppFactory
    {
        private static DefaultAppFactory factorySoleInstance;

        public static DefaultAppFactory GetFactory()
        {
            // TODO: not thread safe
            if (factorySoleInstance == null)
            {
                // TODO: tight coupling
                factorySoleInstance = new DefaultAppFactory(DefaultDocFactory.GetFactory());
            }

            return factorySoleInstance;
        }

        private IDocFactory docFactory;

        public DefaultAppFactory(IDocFactory docFactory)
        {
            this.docFactory = docFactory;
        }

        public Image CreateLogo()
        {
            return Properties.Resources.Logo;
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
            return new DefaultImageBuilder();
        }

        public IApplication CreateApplicationLogic()
        {
            return new DefaultApplication(this);
        }
    }
}
