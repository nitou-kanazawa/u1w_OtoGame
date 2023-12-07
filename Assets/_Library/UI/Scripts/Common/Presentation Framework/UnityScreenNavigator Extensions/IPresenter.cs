using System;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Sheet;


namespace nitou.UI.PresentationFramework {
    
    /// <summary>
    /// プレゼンターを表すインターフェース
    /// </summary>
    public interface IPresenter : IDisposable {
        bool IsDisposed { get; }
        bool IsInitialized { get; }
        void Initialize();
    }

    /// <summary>
    /// Pageプレゼンター
    /// </summary>
    public interface IPagePresenter : IPresenter, IPageLifecycleEvent { }

    /// <summary>
    /// Sheetプレゼンター
    /// </summary>
    public interface ISheetPresenter : IPresenter, ISheetLifecycleEvent { }

    /// <summary>
    /// Modalプレゼンター
    /// </summary>
    public interface IModalPresenter : IPresenter, IModalLifecycleEvent { }
}
