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
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }
    }
}
