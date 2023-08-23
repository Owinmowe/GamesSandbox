using System;

namespace Utilities.MVP
{
    /// <summary>
    /// Base class for presenter in MVP design. This class is responsible of processing events
    /// received from the View with information received from ScriptableData (Model) classes.
    /// Then, if necessary, calls public methods in the view to affect the visual of the game.
    /// <param name="TV">The View associated to this Presenter.</param>
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

        /// <summary>Method for subscribing to events from the View</summary>
        protected abstract void AddViewListeners ();
        
        /// <summary>Method for unsubscribing to events from the View</summary>
        protected abstract void RemoveViewListeners ();
    }
}

