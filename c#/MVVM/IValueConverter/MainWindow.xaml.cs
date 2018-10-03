using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMdemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
/*
 * 
OneWay: Use this when you want the bound property to update the user interface.
TwoWay: This has the same behavior as OneWay and OneWayToSource combined. The bound property will update the user interface, and changes in the user interface will update the bound property (You would use this with a TextBox or a Checkbox for example.)
OneTime: This has the same behavior as OneWay except it will only update the user interface one time. This should be your default choice for binding (for various reasons I won't elaborate on here), you should only use other types of bindings if you actually need the extra functionality.
OneWayToSource: This is the opposite of OneWay -- user interface value changes update the bound property.
If you don't specify anything, then the behavior will depend on the control that you are using.
 * 
 * 
 */
