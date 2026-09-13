using System;
using Unity.VisualScripting;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    //L2 - Фиксим проблему с деинициализацией презентера кошелька
    public interface IPresenter : IInitializable, IDisposable
    {

    }
}
