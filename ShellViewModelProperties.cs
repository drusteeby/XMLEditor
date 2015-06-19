using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace PrismSandbox
{
    public partial class ShellViewModel
    {
        private ShellModel _model;

        protected void initProperties()
        {
            _model = new ShellModel();
            XMLDocumentViewContainers = new ObservableCollection<XMLDocumentViewContainer>();

            _model.LoadXmlDataProviders();
            XMLDataProviderList = _model.XMLDataProviderList;
            


            foreach(XmlDataProvider provider in XMLDataProviderList)
            {
                XMLDocumentViewContainers.Add(new XMLDocumentViewContainer(provider));                
            }
        }

        public ObservableCollection<XmlDataProvider> XMLDataProviderList
        {
            get { return (ObservableCollection<XmlDataProvider>)GetValue(XMLFileListProperty); }
            set { SetValue(XMLFileListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XMLFileList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XMLFileListProperty =
            DependencyProperty.Register("XMLFileList", typeof(ObservableCollection<XmlDataProvider>), typeof(ShellViewModel), new UIPropertyMetadata(null));

   


        public XmlNode SelectedNode
        {
            get { return (XmlNode)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register("SelectedNode", typeof(XmlNode), typeof(ShellViewModel), new UIPropertyMetadata(null));




        public XmlDocument SelectedDocument
        {
            get { return (XmlDocument)GetValue(SelectedDocumentProperty); }
            set { SetValue(SelectedDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDocumentProperty =
            DependencyProperty.Register("SelectedDocument", typeof(XmlDocument), typeof(ShellViewModel), new UIPropertyMetadata(null));



        public ObservableCollection<XMLDocumentViewContainer> XMLDocumentViewContainers
        {
            get { return (ObservableCollection<XMLDocumentViewContainer>)GetValue(XMLDocumentViewContainersProperty); }
            set { SetValue(XMLDocumentViewContainersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XMLDocumentViewContainers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XMLDocumentViewContainersProperty =
            DependencyProperty.Register("XMLDocumentViewContainers", typeof(ObservableCollection<XMLDocumentViewContainer>), typeof(ShellViewModel), new UIPropertyMetadata(null));

        

        
    }
}
