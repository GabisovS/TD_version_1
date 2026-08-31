using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializes
{
    //L2 - Реализация json сериализатора
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);

        TData Deserialize<TData>(string serializedData);
    }
}
