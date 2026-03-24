namespace PipBoy2000.Behaviors;

public class ScanlineScrollBehavior : Behavior<VisualElement>
{
    private const string AnimationName = "ScanlineScroll";

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);

        bindable.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () =>
        {
            StartAnimation(bindable);
        });
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        bindable.AbortAnimation(AnimationName);

        base.OnDetachingFrom(bindable);
    }

    private void StartAnimation(VisualElement element)
    {
        if (Application.Current?.Resources.TryGetValue("ScanlineBrush", out var resource) != true ||
            resource is not LinearGradientBrush brush)
        {
            System.Diagnostics.Debug.WriteLine("ScanlineBrush not found or wrong type.");
            return;
        }

        var animation = new Animation(v =>
        {
            foreach (var stop in brush.GradientStops)
            {
                stop.Offset = (float)((stop.Offset + v * 0.005) % 1.0);
            }
        }, 0, 1);

        animation.Commit(element, AnimationName,
            length: 16000,
            easing: Easing.Linear,
            finished: null,
            repeat: () => true);
    }
}