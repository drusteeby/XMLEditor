namespace PrismSandbox
{
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;
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
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("MainRegion", () => this.container.Resolve<XMLDocumentView.View>());
        }




    }
}
