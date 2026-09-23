using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    //L3 - Небольшой нюанс с подписками
    public interface ISubscribedPresenter : IPresenter
    {
        void Subscribe();
        void Unsubscribe();
    }
}
