using System;

public static class InputEvents
{
    public static event Action OnSubmit;
    public static event Action OnCancel;
    public static event Action<float> OnScroll;
    public static event Action<string> OnInputDeviceChange;

    public static void Submit() 
    {
        OnSubmit?.Invoke();
    }

    public static void Cancel()
    {
        OnCancel?.Invoke();
    }

    public static void Scroll(float direction)
    {
        OnScroll?.Invoke(direction);
    }

    public static void InputDeviceChange(string scheme)
    {
        OnInputDeviceChange?.Invoke(scheme);
    }
}
