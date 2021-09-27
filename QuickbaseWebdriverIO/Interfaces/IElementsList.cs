using System;
using System.Collections.Generic;

namespace QuickbaseWebdriverIO.Interfaces
{
    public interface IElementsList : IEnumerable<IElement>
    {
        int Count { get; }

        IElement this[int i] { get; }

        void ForEach(Action<IElement> action);
    }
}
