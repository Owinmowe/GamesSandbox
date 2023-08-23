using System;

namespace General.MVP
{
    /// <summary>
    /// Base class for presenter in MVP design. This class is responsible of processing events
    /// received from the View with information received from ScriptableData (Model) classes.
    /// Then, if necessary, calls public methods in the view to affect the visual of the game.
    /// <typeparam name ="TV">The type of data to get.</typeparam>
    /// </summary>
    public abstract class Presenter<TV> : IDisposable
    {
        protected readonly TV View;
            
        /// <summary>
        /// Constructor made to cache or get necessary ScriptableData. There is no need to call
        /// AddViewListeners methods since its called on Presenter base class.
        /// </summary>
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

        /// <summary>
        /// Method for subscribing to events from the View. Note that this method will be called
        /// <b>AFTER</b> Presenters subclasses constructors.
        /// </summary>
        protected abstract void AddViewListeners ();
        
        /// <summary>Method for unsubscribing to events from the View.</summary>
        protected abstract void RemoveViewListeners ();
    }
}

