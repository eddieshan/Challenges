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

using Drills.TennisMatch.Desktop.ViewModels;
using Drills.TennisMatch.Desktop.Services;

namespace Drills.TennisMatch.Desktop
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // TODO: this must be done with a proper dependency injection container.
            // Prism or Caliburn would be typical choices to set up dependencies, navigation, etc.
            DataContext = new TennisMatchViewModel(new MatchService());
        }
    }
}
