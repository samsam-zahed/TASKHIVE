using System.Configuration;
using System.Data;
using System.Windows;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace TaskHive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() {
            QuestPDF.Settings.License = LicenseType.Community;
        }
    }

}
