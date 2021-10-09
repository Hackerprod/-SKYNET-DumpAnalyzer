using ProtoBuf;
using System;

[Serializable]
[ProtoContract(Name = "CChatRoom_AddRoleToUser_Response")]
public class Extensible_base : IExtensible
{
    private IExtension extensionObject;
    IExtension IExtensible.GetExtensionObject(bool createIfMissing)
    {
        return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
    }
}
