using MCM.Core.Objects;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismSandbox.AddAlarmView
{
    public class ViewModel: DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        public DelegateCommand SelectionChanged { get; set; }
        public DelegateCommand goBack { get; set; }

        public ViewModel()
        {
            SelectionChanged = new DelegateCommand(OnSelectionChanged);
            goBack = new DelegateCommand(OnGoBack);
            EnumerationValues = new ObservableCollection<EnumerationContainer>();
            enumNames = new ObservableCollection<string>();

            var dataTags = TagCollection.VirtualTags.Where(dt => (dt.DataType == MCM.Core.Enum.DataType.Bits || dt.DataType == MCM.Core.Enum.DataType.Enum));
         
            enumNames.Add("New Item...");
            foreach(DataTag dt in dataTags)
            {
                enumNames.Add(dt.Name);
            }
        }

        public void OnGoBack()
        {
            var view = regionManager.Regions["PopupRegion"].Views.Single(x => x.GetType() == typeof(View));
            regionManager.Regions["PopupRegion"].Remove(view);

        }


        public void OnSelectionChanged()
        {
            if (SelectedEnumName.Contains("New Item"))
            {
                newVisibility = Visibility.Visible;
                EnumerationValues.Clear();
                for (int i = 0; i < 32; i++)
                    EnumerationValues.Add(new EnumerationContainer(i.ToString()));
            }

            else
            {
                newVisibility = Visibility.Hidden;
                DataTag tag = TagCollection.VirtualTags.Single(dt => ((dt.DataType == MCM.Core.Enum.DataType.Bits || dt.DataType == MCM.Core.Enum.DataType.Enum) && dt.Name == SelectedEnumName));

                EnumerationValues = new ObservableCollection<EnumerationContainer>(TagHelper.getContainersfromEnum(TagHelper.getEnumfromTag(tag)));
            }
            

        }



        public Visibility newVisibility
        {
            get { return (Visibility)GetValue(newVisibilityProperty); }
            set { SetValue(newVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newVisibilityProperty =
            DependencyProperty.Register("newVisibility", typeof(Visibility), typeof(ViewModel), new UIPropertyMetadata(null));

        

        public ObservableCollection<string> enumNames
        {
            get { return (ObservableCollection<string>)GetValue(enumNamesProperty); }
            set { SetValue(enumNamesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for enumNames.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty enumNamesProperty =
            DependencyProperty.Register("enumNames", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));



        public string SelectedEnumName
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEnumName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedEnumName", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));

        
        public ObservableCollection<EnumerationContainer> EnumerationValues
        {
            get { return (ObservableCollection<EnumerationContainer>)GetValue(EnumerationValuesProperty); }
            set { SetValue(EnumerationValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnumerationValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnumerationValuesProperty =
            DependencyProperty.Register("EnumerationValues", typeof(ObservableCollection<EnumerationContainer>), typeof(ViewModel), new UIPropertyMetadata(null));

        
        
    }
}
