using System;

namespace Utilities.MVP
{
    /// <summary>
    /// Base class for presenter in MVP design. This class is responsible of processing events
    /// received from the View with information received from ScriptableData (Model) classes. Then it returns
    /// commands to the view to affect the visual of the game.
    /// </summary>
    public abstract class Presenter<TV> : IDisposable
    {
        protected readonly TV View;
            
        protected Presenter (TV view)
        {
            this.View = view;
            
            // Possible problem based on order of operations (This will be called AFTER base class constructors).
            // TODO possible rework.
            AddViewListeners();
        }

        public void Dispose ()
        {
            RemoveViewListeners();
        }

        protected abstract void AddViewListeners ();
        protected abstract void RemoveViewListeners ();
    }
}

