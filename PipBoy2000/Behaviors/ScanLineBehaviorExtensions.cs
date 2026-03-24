using Microsoft.Maui.Controls;

namespace PipBoy2000.Behaviors;

public static class ScanlineBehaviorExtensions
{
    public static readonly BindableProperty AutoScanlineScrollProperty =
        BindableProperty.CreateAttached(
            "AutoScanlineScroll",
            typeof(bool),
            typeof(ScanlineBehaviorExtensions),
            false,
            propertyChanged: OnAutoScanlineScrollChanged);

    public static bool GetAutoScanlineScroll(BindableObject view)
        => (bool)view.GetValue(AutoScanlineScrollProperty);

    public static void SetAutoScanlineScroll(BindableObject view, bool value)
        => view.SetValue(AutoScanlineScrollProperty, value);

    private static void OnAutoScanlineScrollChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not VisualElement element) return;

        if ((bool)newValue)
        {
            element.Behaviors.Add(new ScanlineScrollBehavior());
        }
        else
        {
            var behavior = element.Behaviors.OfType<ScanlineScrollBehavior>().FirstOrDefault();
            if (behavior != null)
                element.Behaviors.Remove(behavior);
        }
    }
}