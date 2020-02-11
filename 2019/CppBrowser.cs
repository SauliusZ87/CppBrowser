using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace CppBrowser
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CppBrowser
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("3b2e08f9-6eb2-4eff-b86a-5dc392e07047");
        private readonly AsyncPackage package;

        private Core.Main main = null;

        private CppBrowser(AsyncPackage package, OleMenuCommandService commandService, EnvDTE80.DTE2 dte)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);

            if (null == main)
                main = new Core.Main(dte);
        }

        public static CppBrowser Instance { get; private set; }

        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider { get { return this.package; } }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            EnvDTE80.DTE2 dte = await package.GetServiceAsync((typeof(EnvDTE.DTE))) as EnvDTE80.DTE2;
            Instance = new CppBrowser(package, commandService, dte);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            main.ShowElements();
        }
    }
}
