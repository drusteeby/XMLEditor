using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace PrismSandbox
{
    public class Node: DependencyObject
    {
        public string Name { get; set; }

        public Node()
        {
            Attributes = new Dictionary<string, string>();
            Children = new ObservableCollection<Node>();                 
        }
        public Dictionary<string, string> Attributes { get; set; }

        public bool hasChildren
        {
            get { return (bool)GetValue(hasChildrenProperty); }
            set { SetValue(hasChildrenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for hasChildren.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty hasChildrenProperty =
            DependencyProperty.Register("hasChildren", typeof(bool), typeof(Node), new UIPropertyMetadata(null));        


        public ObservableCollection<Node> Children
        {
            get { return (ObservableCollection<Node>)GetValue(ChildernProperty); }
            set { SetValue(ChildernProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Childern.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildernProperty =
            DependencyProperty.Register("Childern", typeof(ObservableCollection<Node>), typeof(Node), new UIPropertyMetadata(null));



        /*
        public string type { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public string text { get; set; }
         */ 


    }
}
