using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;

namespace PrismSandbox
{
    public partial class ShellViewModel
    {
        public DelegateCommand saveAs { get; set; }
        public DelegateCommand save { get; set; }
        public DelegateCommand<RoutedPropertyChangedEventArgs<Object>> SelectedItemChanged { get; set; }
        public DelegateCommand updateView { get; set; }
        public DelegateCommand AddXMLFile { get; set; }
        public DelegateCommand DeleteNode { get; set; }
        public DelegateCommand<SelectionChangedEventArgs> SelectionChanged { get; set; }
        public DelegateCommand RemoveXMLFile { get; set; }

        public void initCommands()
        {
            saveAs = new DelegateCommand(OnSaveAs);
            save = new DelegateCommand(OnSave);
            SelectedItemChanged = new DelegateCommand<RoutedPropertyChangedEventArgs<Object>>(OnSelectedItemChanged);
            updateView = new DelegateCommand(OnUpdateView);
            AddXMLFile = new DelegateCommand(OnAddXMLFile);
            DeleteNode = new DelegateCommand(OnDeleteNode, CanDeleteNode);
            SelectionChanged = new DelegateCommand<SelectionChangedEventArgs>(OnSelectionChanged);
            //RemoveXMLFile = new DelegateCommand(OnRemoveXMLFile);
        }

        public void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            SelectedDocument = XMLDataProviderList.ElementAt((e.Source as TabControl).SelectedIndex).Document;

        }

        public void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<Object> e)
        {
            SelectedNode = (XmlNode)(e.NewValue);
            DeleteNode.RaiseCanExecuteChanged();
        }

        public void OnDeleteNode()
        {
            SelectedNode.ParentNode.RemoveChild(SelectedNode);
            SelectedNode = null;
            DeleteNode.RaiseCanExecuteChanged();
        }

        public bool CanDeleteNode()
        {
            return (SelectedNode != null);
        }

        public void OnAddXMLFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML files (*.xml)|*.xml";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an XML file to load.";

            if (dialog.ShowDialog() == true)
            {
                AddDocument(dialog.FileName);
            }
        }

        public void OnUpdateView()
        {
            XmlDocument temp = SelectedDocument;
            SelectedDocument = new XmlDocument();
            SelectedDocument = temp;
        }

        public void OnSaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                SelectedDocument.Save(dialog.FileName);
                
            }
        }

        public void OnSave()
        {            
            SelectedDocument.Save(new Uri(SelectedDocument.BaseURI).LocalPath);
        }

        private void AddDocument(string fileName)
        {
            XmlDocument toAdd = new XmlDocument();
            toAdd.Load(fileName);

            XmlDataProvider provider = new XmlDataProvider();
            provider.Document = toAdd;
            XMLDataProviderList.Add(provider);

            Properties.Settings.Default.filePaths.Add(fileName);
            Properties.Settings.Default.Save();
        }
    }
}
