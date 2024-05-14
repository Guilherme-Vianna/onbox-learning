using Autodesk.Revit.UI;
using Onbox.Abstractions.V9;
using Onbox.Core.V9;
using Onbox.Mvc.Revit.V9;
using Onbox.Mvc.V9.Messaging;
using Onbox.Revit.V9;
using Onbox.Revit.V9.Applications;
using Onbox.Revit.V9.UI;
using OnBoxTeste.Revit.Commands;
using OnBoxTeste.Revit.Commands.Availability;
using OnBoxTeste.Views;

namespace OnBoxTeste.Revit
{
    [ContainerProvider("1e515b08-3b46-4235-84d2-45301b22d26f")]
    public class App : RevitApp
    {
        public override void OnCreateRibbon(IRibbonManager ribbonManager)
        {
            // Here you can create Ribbon tabs, panels and buttons

            var br = ribbonManager.GetLineBreak();

            // Adds a Ribbon Panel to the Addins tab
            var addinPanelManager = ribbonManager.CreatePanel("OnBoxTeste");
            addinPanelManager.AddPushButton<HelloCommand, AvailableOnProject>($"Hello{br}Framework", "onbox_logo");

            // Adds a new Ribbon Tab with a new Panel
            var panelManager = ribbonManager.CreatePanel("OnBoxTeste", "Hello Panel");
            panelManager.AddPushButton<HelloCommand, AvailableOnProject>($"Hello{br}Framework", "onbox_logo");
            panelManager.AddPushButton<WPFViewCommand, AvailableOnProject>($"Hello{br}WPF", "autodesk_logo");
        }

        public override Result OnStartup(IContainer container, UIControlledApplication application)
        {
            // Dependencias para criar o container *OBRIGATORIO
            container.AddOnboxCore();
            container.AddRevitMvc();

            // Registers IWPFView to the container
            // Views should ALWAYS be added as Transients
            container.AddTransient<IHelloWpfView, HelloWpfView>();

            // ADICAO DOS SERVICOS
            container.AddSingleton<IMessageService, MessageBoxService>();

            return Result.Succeeded;
        }

        public override Result OnShutdown(IContainerResolver container, UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }

}