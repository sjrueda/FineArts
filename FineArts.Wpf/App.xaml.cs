using FineArts.Bll;
using System.Windows;

namespace FineArts.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            TeacherService.SeedData();
            base.OnStartup(e);
        }
    }
}
