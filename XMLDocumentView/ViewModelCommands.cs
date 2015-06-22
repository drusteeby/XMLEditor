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

namespace PrismSandbox.XMLDocumentView
{
    public partial class ViewModel
    {
        public DelegateCommand saveAs { get; set; }
        public DelegateCommand save { get; set; }
        public DelegateCommand<RoutedPropertyChangedEventArgs<Object>> SelectedItemChanged { get; set; }
        public DelegateCommand updateView { get; set; }
        public DelegateCommand AddXMLFile { get; set; }
        public DelegateCommand DeleteNode { get; set; }
        public DelegateCommand<SelectionChangedEventArgs> SelectionChanged { get; set; }
        public DelegateCommand RemoveXMLFile { get; set; }
        public DelegateCommand ClearSettings { get; set; }

        public void initCommands()
        {
            saveAs = new DelegateCommand(OnSaveAs);
            save = new DelegateCommand(OnSave);
            SelectedItemChanged = new DelegateCommand<RoutedPropertyChangedEventArgs<Object>>(OnSelectedItemChanged);            
            AddXMLFile = new DelegateCommand(OnAddXMLFile);
            DeleteNode = new DelegateCommand(OnDeleteNode, CanDeleteNode);
            SelectionChanged = new DelegateCommand<SelectionChangedEventArgs>(OnSelectionChanged);
            RemoveXMLFile = new DelegateCommand(OnRemoveXMLFile);
            ClearSettings = new DelegateCommand(OnClearSettings);
        }

        public void OnClearSettings()
        {
            _model.ClearSettings();
        }

        public void OnRemoveXMLFile()
        {
            _model.RemoveFile(SelectedDocument.FullFilePath);     
        }

        public void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            int index = (e.Source as TabControl).SelectedIndex;

            if (XMLDocumentViewContainers.Count >= index && index >= 0) 
                SelectedDocument = XMLDocumentViewContainers.ElementAt(index);

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
                _model.AddFile(dialog.FileName);
            }
        }


        public void OnSaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                SelectedDocument.XMLDataProvider.Document.Save(dialog.FileName);                           
            }
        }

        public void OnSave()
        {
            SelectedDocument.XMLDataProvider.Document.Save(SelectedDocument.FullFilePath);
            SelectedDocument.unsavedChanges = false;
        }


    }
}
