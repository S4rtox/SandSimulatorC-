//Code for ElementMenu
using GumRuntime;
using System.Linq;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using SandSimulator2.Components.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace SandSimulator2.Screens;
partial class ElementMenu : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("ElementMenu");
#if DEBUG
if(element == null) throw new System.InvalidOperationException("Could not find an element named ElementMenu - did you forget to load a Gum project?");
#endif
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new ElementMenu(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(ElementMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("ElementMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public StackPanel StackPanelInstance { get; protected set; }
    public TextBox Ip { get; protected set; }
    public TextBox Port { get; protected set; }
    public ButtonDeny Host { get; protected set; }
    public ButtonConfirm Join { get; protected set; }
    public StackPanel ButtonsStackPanels { get; protected set; }

    public ElementMenu(InteractiveGue visual) : base(visual)
    {
    }
    public ElementMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        StackPanelInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<StackPanel>(this.Visual,"StackPanelInstance");
        Ip = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TextBox>(this.Visual,"Ip");
        Port = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TextBox>(this.Visual,"Port");
        Host = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonDeny>(this.Visual,"Host");
        Join = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonConfirm>(this.Visual,"Join");
        ButtonsStackPanels = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<StackPanel>(this.Visual,"ButtonsStackPanels");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
