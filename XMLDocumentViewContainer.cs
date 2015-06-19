using MCM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrismSandbox
{
    public class XMLDocumentViewContainer
    {
        public XmlDataProvider XMLDataProvider { get; set; }
        public ObservableCollection<DataTag> DataTags { get; set; }        
        public string FullFilePath { get; set; }
        private bool _unsavedChanges;

        public string Header
        {
            get
            {
                return FullFilePath.Split('\\').Last() + (_unsavedChanges ? "*": " ");
            }

        }
        

        public XMLDocumentViewContainer(XmlDataProvider provider)
        {
            XMLDataProvider = provider;
            XMLDataProvider.DataChanged += XMLDataProvider_DataChanged;

            FullFilePath = provider.Source.LocalPath;

        }

        void XMLDataProvider_DataChanged(object sender, EventArgs e)
        {
            _unsavedChanges = true;
        }


        

    }
}
