using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ProtoHelper
{
    public static T Deserialize<T>(this byte[] msg)
    {
        try
        {
            return Serializer.Deserialize<T>(new MemoryStream(msg));
        }
        catch (Exception)
        {
            //modCommon.Show("Error intentando deserializar " + typeof(T));
            return Serializer.Deserialize<T>(new MemoryStream(msg));
        }
    }
}
