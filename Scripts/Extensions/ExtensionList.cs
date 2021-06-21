using System;
using System.Collections.Generic;

public static class ExtensionList
{
    public static void SingleOperation<T>(this List<T> list, Action<T> function)
    {
        foreach (var element in list)
        {
            function(element);
        }
    }
}
