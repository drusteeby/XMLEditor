using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using MCM.Core.Objects;

namespace PrismSandbox
{
    public class ShellModel
    {
        public ObservableCollection<XmlDataProvider> XMLDataProviderList { get; private set; }

        public ShellModel()
        {

        }

        public void RemoveFileFromList(string filePath)
        {
            //remove it from the application properties then reload the data providers
            if (Properties.Settings.Default.filePaths != null && Properties.Settings.Default.filePaths.Contains(filePath))
                Properties.Settings.Default.filePaths.RemoveAt(Properties.Settings.Default.filePaths.IndexOf(filePath));

            LoadXmlDataProviders();
        }

        public void LoadXmlDataProviders()
        {
            if (Properties.Settings.Default.filePaths == null) Properties.Settings.Default.filePaths = new StringCollection();

            XMLDataProviderList = new ObservableCollection<XmlDataProvider>();

            //required to flag the removal of the file from the list after the foreach block has finished enumerating.
            List<int> indexFlags = new List<int>();

            foreach (string fn in Properties.Settings.Default.filePaths)
            {
                try
                {
                    XmlDataProvider provider = new XmlDataProvider();
                    provider.Source = new Uri(fn);
                    provider.XPath = "tags";

                    if(!XMLDataProviderList.Contains(provider))
                        XMLDataProviderList.Add(provider);
                    else
                        indexFlags.Add(Properties.Settings.Default.filePaths.IndexOf(fn)); //if it's a duplicate entry flag it for removal

                    //TagCollection.LoadTags(fn);
                }
                catch (Exception)
                {
                    if (MessageBox.Show("File failed to load:  " + fn + System.Environment.NewLine + " Remove from file List?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        indexFlags.Add(Properties.Settings.Default.filePaths.IndexOf(fn));
                     
                }
                               
            }

            foreach(int index in indexFlags)
            {
                Properties.Settings.Default.filePaths.RemoveAt(index);
            }

            Properties.Settings.Default.Save();
            
        }
    }
}
