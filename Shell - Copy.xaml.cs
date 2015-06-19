namespace PrismSandbox
{
    using FirstFloor.ModernUI.Windows.Controls;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Xml;

    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell(ShellViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
           
            
        }

        private void xmlTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ((sender as TreeView).Items.GetItemAt(0) as TreeViewItem).ExpandSubtree();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }



    }
}
