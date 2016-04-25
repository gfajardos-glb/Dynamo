using Dynamo.ViewModels;
using Dynamo.Wpf.ViewModels.Watch3D;
using System;
using System.ComponentModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Dynamo.Core;
using Dynamo.Models;
using Dynamo.Nodes;
using Dynamo.Nodes.Prompts;
using Dynamo.PackageManager;
using Dynamo.PackageManager.UI;
using Dynamo.Search;
using Dynamo.Selection;
using Dynamo.Utilities;
using Dynamo.Wpf;
using Dynamo.Wpf.Authentication;
using Dynamo.Wpf.Controls;

namespace DynamoPackageManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DynamoViewModel dynamoViewModel;
        public MainWindow()
        {
            InitializeComponent();

            //Register object in JS
            cefBrowser.RegisterJsObject("pkgMgrContext", new PackageManagerContext()
            {
                Message = "Hello world!",
                Packages = new PackageData[] { new PackageData() { Name = "Sample" } }
            });
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            this.cefBrowser.FrameLoadEnd += CefBrowser_FrameLoadEnd;
        }

        private void CefBrowser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {

            var model = Dynamo.Applications.StartupUtils.MakeModel(false);
            dynamoViewModel = DynamoViewModel.Start(
                    new DynamoViewModel.StartConfiguration()
                    {
                        CommandFilePath = "",
                        DynamoModel = model,
                        Watch3DViewModel = HelixWatch3DViewModel.TryCreateHelixWatch3DViewModel(new Watch3DViewModelStartupParams(model), model.Logger),
                        ShowLogin = true
                    });
            
            var pmExtension = dynamoViewModel.Model.GetPackageManagerExtension();
            var x = new InstalledPackagesViewModel(dynamoViewModel,pmExtension.PackageLoader);

            //Get installed packages
            var packages = x.LocalPackages.Select(pkg => pkg.Model.Name).ToArray();            

            if (e.Frame.IsMain)
            {
                var message = "'" + string.Join(",", packages) + "'";

                //Use previously registered object
                //message = "pkgMgrContext.message";
                
                e.Frame.ExecuteJavaScriptAsync("alert(" + message + ");");
            }
        }
    }

    public class PackageManagerContext
    {
        public string Message { get; set; }
        public PackageData[] Packages { get; set; }
    }

    public class PackageData
    {
        public string Name { get; set; }
    }
}
