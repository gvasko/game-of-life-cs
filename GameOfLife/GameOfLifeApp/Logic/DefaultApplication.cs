using IGameOfLife;
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
        private IDocument document;
        private IImageBuilder imageBuilder;
        private Image image;
        private Image logo;
        private int cellSize;
        private string file;

        public DefaultApplication(IAppFactory factory)
        {
            this.factory = factory;
            document = null;
            imageBuilder = factory.CreateImageBuilder();
            logo = factory.CreateLogo();
            image = logo;
            cellSize = 1;
            file = string.Empty;
        }

        public int CellSize
        {
            get
            {
                return cellSize;
            }

            set
            {
                if (cellSize != value)
                {
                    cellSize = value;
                    imageBuilder.CellSize = value;
                    NotifyObservers(CellSizeChanged);
                    UpdateImage();
                }
            }
        }

        public string File
        {
            get
            {
                return file;
            }

            set
            {
                if (file != value)
                {
                    IDocument newDoc = LoadNewDoc(value);
                    DetachFromOldDocument();
                    AttachToNewDocument(value, newDoc);
                }
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
            private set
            {
                if (image != value)
                {
                    image = value;
                    NotifyObservers(ImageChanged);
                }
            }
        }

        public event EventHandler CellSizeChanged;
        public event EventHandler FileChanged;
        public event EventHandler ImageChanged;

        public void NextImage()
        {
            if (document != null)
            {
                document.NextState();
            }
        }

        public void ProcessCommandLineArguments(string[] args)
        {
            throw new NotImplementedException();
        }

        public void ResetImage()
        {
            if (document != null)
            {
                document.Reset();
            }
        }

        public void StartAnimation()
        {
            throw new NotImplementedException();
        }

        public void StopAnimation()
        {
            throw new NotImplementedException();
        }

        private IDocument LoadNewDoc(string newFile)
        {
            if (string.IsNullOrEmpty(newFile))
            {
                return null;
            }

            return factory.LoadFile(newFile);
        }

        private void DetachFromOldDocument()
        {
            if (document != null)
            {
                document.CurrentStateChanged -= OnLifeStateChanged;
            }
        }

        private void AttachToNewDocument(string newFile, IDocument newDoc)
        {
            file = newFile;
            document = newDoc;
            if (document != null)
            {
                document.CurrentStateChanged += OnLifeStateChanged;
            }
            NotifyObservers(FileChanged);
            UpdateImage();
        }

        private void OnLifeStateChanged(object sender, EventArgs args)
        {
            if (sender != document)
            {
                throw new InvalidOperationException("Unknown sender.");
            }
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (document != null)
            {
                Image = imageBuilder.AsImage(document.CurrentState);
            }
            else
            {
                Image = logo;
            }
        }

        private void NotifyObservers(EventHandler localHandler)
        {
            if (localHandler != null)
            {
                localHandler(this, EventArgs.Empty);
            }
        }

    }
}
