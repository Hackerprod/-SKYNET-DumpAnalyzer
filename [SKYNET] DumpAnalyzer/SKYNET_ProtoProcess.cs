using NetHookAnalyzer2;
using NetHookAnalyzer2.Implementations;
using ProtoBuf;
using SKYNET.Steam;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SKYNET
{
    //public class SKYNET_ProtoProcess : SKYNET_Helper
    //{
        //    public SKYNET_ProtoProcess()
        //    {
        //    }

        //    public byte[] GCpayload { get; set; }

    //    public void ProcessBody(object body)
    //{
    //    //body is CMsgGCClient
    //    Game_Coordinator_Message message = new Game_Coordinator_Message();

    //    GCGenericSpecialization gCGeneric = new GCGenericSpecialization();
    //    List<KeyValuePair<string, object>> source2 = gCGeneric.ReadExtraObjects(body).ToList();

    //    object value = source2[0].Value;

    //    List<PropertyInfo> properties = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

    //    foreach (PropertyInfo item2 in properties)
    //    {
    //        if (item2.Name == "emsg")
    //        {
    //            message.emsg = item2.GetValue(value, null);
    //        }
    //        if (item2.Name == "header")
    //        {
    //            message.header = item2.GetValue(value, null);
    //        }
    //        if (item2.Name == "body")
    //        {
    //            message.body = item2.GetValue(value, null);
    //        }
    //    }

    //    if (body is CMsgGCClient)
    //    {
    //        CMsgGCClient msgGCClient = (CMsgGCClient)body;

    //        if (message.body != null)
    //        {
    //            GetExportableCode(MsgUtil.GetGCMsg(msgGCClient.msgtype), message.body);
    //        }

    //        return;
    //    }
    //}

    //    private void Write(object v)
    //    {
    //        frmMain.Write(v);
    //    }

    //    public void GetExportableCode(uint msgType, object Body)
    //    {
    //        MsgType = msgType;
    //        Type type = Body.GetType();
    //        StringBuilder stringBuilder = new StringBuilder();

    //        object obj2 = type.GetProperty("Body")?.GetValue(Body, null);
    //        if (obj2 != null)
    //        {
    //            string value = new string(' ', 4);
    //            string classname = GetClassName(Body.ToString());
    //            stringBuilder.AppendLine($"ClientGCMsgProtobuf<{ classname }> { classname.Replace("CMsg", "") } = new ClientGCMsgProtobuf<{ classname }>({ msgType }U)");
    //            stringBuilder.AppendLine("{");
    //            stringBuilder.AppendLine(value + "Body = ");
    //            stringBuilder.AppendLine(value + "{");
    //            stringBuilder.AppendLine(PrintPropertiesPlain(obj2, 8));
    //            stringBuilder.AppendLine(value + "}");
    //            stringBuilder.AppendLine("};");
    //        }

    //        StringBuilder code = new StringBuilder();


    //        Write(code.ToString());


    //        string filename = GetClassName(Body.ToString());
    //        string filepath = Path.Combine(modCommon.GetPath(), "Exported Files", filename + ".cs");
    //        modCommon.EnsureDirectoryExists(filepath, true);
    //        File.WriteAllText(filepath, code.ToString());
    //        if (File.Exists(filepath))
    //        {
    //            try
    //            {
    //                Process.Start(Path.Combine(Application.StartupPath, filepath));
    //            }
    //            catch (Exception)
    //            { }
    //        }

    //    }

    //    private static uint LastID;
    //    private static uint MsgType;


    //    public static string PrintPropertiesPlain(object obj, int indent = 1)
    //    {
    //        if (obj == null)
    //        {
    //            return string.Empty;
    //        }
    //        string value = new string(' ', indent);
    //        Type type = obj.GetType();
    //        PropertyInfo[] properties = type.GetProperties();
    //        int len = (properties.Length != 0) ? properties.Max((PropertyInfo p) => p.Name.Length) : 15;
    //        StringBuilder stringBuilder = new StringBuilder();
    //        foreach (PropertyInfo property in properties)
    //        {
    //            if (!property.Name.EndsWith("Specified"))
    //            {
    //                object obj2 = null;
    //                try
    //                {
    //                    obj2 = property.GetValue(obj, null);
    //                }
    //                catch { }

    //                PropertyInfo propertyInfo = properties.FirstOrDefault((PropertyInfo p) => p.Name.Equals(property.Name + "Specified"));
    //                bool flag = (bool)(propertyInfo?.GetValue(obj, null) ?? ((object)false));
    //                if (obj2 == null)
    //                {
    //                    stringBuilder.Append(value);
    //                    stringBuilder.Append(property.Name + " ");
    //                    stringBuilder.Append("= null,");
    //                    stringBuilder.AppendLine();
    //                }
    //                else if (!(obj2 is Stream))
    //                {
    //                    //Done
    //                    if (obj2.GetType().IsEnum)
    //                    {
    //                        stringBuilder.Append(value);

    //                        if (int.TryParse(obj2.ToString(), out int enumCode))
    //                        {
    //                            stringBuilder.Append(property.Name + " ");
    //                            stringBuilder.Append("= ");
    //                            stringBuilder.Append("(" + GetExtentionName(obj2.GetType().ToString()) + ")" + enumCode.ToString() + ",");
    //                        }
    //                        else
    //                        {
    //                            stringBuilder.Append(property.Name + " ");
    //                            stringBuilder.Append("= ");
    //                            stringBuilder.Append(GetExtentionName(obj2.GetType().ToString()) + "." + obj2.ToString() + ",");
    //                        }
    //                        stringBuilder.AppendLine();
    //                    }
    //                    else if (obj2.GetType() == typeof(byte) || obj2.GetType() == typeof(int) || obj2.GetType() == typeof(uint) || obj2.GetType() == typeof(long) || obj2.GetType() == typeof(ulong) || obj2.GetType() == typeof(float) || obj2.GetType() == typeof(double))
    //                    {
    //                        if (property.Name.ToLower() == "id" || property.Name.ToLower() == "type_id")
    //                        {
    //                            LastID = (uint)Convert.ToInt64(obj2);
    //                        }

    //                        var objValue = obj2.ToString().Replace(",", ".");
    //                        objValue = obj2.GetType() == typeof(float) ? objValue += "f" : objValue += "";

    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append(property.Name + " ");
    //                        stringBuilder.Append("= ");
    //                        stringBuilder.Append(objValue);
    //                        stringBuilder.Append(",");
    //                        stringBuilder.AppendLine();
    //                    }
    //                    else if (obj2.GetType() == typeof(string))
    //                    {
    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append(property.Name + " ");
    //                        stringBuilder.Append("= \"");
    //                        stringBuilder.Append(obj2);
    //                        stringBuilder.Append("\"");
    //                        stringBuilder.Append(",");

    //                        stringBuilder.AppendLine();
    //                    }
    //                    else if (property.PropertyType.Assembly == type.Assembly)
    //                    {
    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append(property.Name + " ");
    //                        stringBuilder.Append($"= new {GetExtentionName(obj2.GetType().ToString())}()");
    //                        stringBuilder.AppendLine();
    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append("{");
    //                        stringBuilder.AppendLine();
    //                        stringBuilder.Append(PrintPropertiesPlain(obj2, indent + 4));
    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append("},");
    //                        stringBuilder.AppendLine();
    //                    }
    //                    else if (obj2.GetType() == typeof(bool))
    //                    {
    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append(property.Name + " ");
    //                        stringBuilder.Append("= ");
    //                        stringBuilder.Append(obj2.ToString().ToLower());
    //                        stringBuilder.Append(",");
    //                        stringBuilder.AppendLine();
    //                    }
    //                    else if (obj2.GetType() == typeof(byte[]))
    //                    {
    //                        byte[] item = obj2 as byte[];
    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append(property.Name + " ");
    //                        stringBuilder.Append("= ");

    //                        if (TryDeserializeBinary(item, property.Name, out object protoObj))
    //                        {
    //                            stringBuilder.Append($"new {GetExtentionName(protoObj.GetType().ToString())}()");
    //                            stringBuilder.AppendLine();
    //                            stringBuilder.Append(value);
    //                            stringBuilder.Append("{");
    //                            stringBuilder.AppendLine();
    //                            stringBuilder.Append(PrintPropertiesPlain(protoObj, indent + 4));
    //                            stringBuilder.Append(value);
    //                            stringBuilder.Append("}.Serialize(),");

    //                        }
    //                        else
    //                        {
    //                            stringBuilder.Append("new byte[] ");
    //                            stringBuilder.Append("{ ");

    //                            for (int j = 0; j < item.Length; j++)
    //                            {
    //                                byte b = item[j];
    //                                stringBuilder.AppendFormat("0x{0:X2}", b);
    //                                if (j < item.Length - 1)
    //                                {
    //                                    stringBuilder.Append(", ");
    //                                }
    //                                else
    //                                    stringBuilder.Append(" ");
    //                            }
    //                            stringBuilder.Append("},");
    //                        }
    //                        stringBuilder.AppendLine();
    //                    }
    //                    else if (obj2 is IList)
    //                    {
    //                        IList list = obj2 as IList;
    //                        if (list != null)
    //                        {
    //                            if (list is IList<byte[]>)
    //                            {
    //                                foreach (byte[] item in list as IList<byte[]>)
    //                                {
    //                                    stringBuilder.Append(value);
    //                                    stringBuilder.Append(property.Name + " ");
    //                                    stringBuilder.AppendLine("= new List<byte[]>()");

    //                                    stringBuilder.Append(value);
    //                                    stringBuilder.Append("{");

    //                                    if (TryDeserializeBinary(item, property.Name, out object protoObj))
    //                                    {
    //                                        string value2 = new string(' ', indent + 4);
    //                                        stringBuilder.AppendLine();
    //                                        stringBuilder.Append(value2);
    //                                        stringBuilder.Append($"new {GetExtentionName(protoObj.GetType().ToString())}()");
    //                                        stringBuilder.AppendLine();
    //                                        stringBuilder.Append(value2);
    //                                        stringBuilder.Append("{");
    //                                        stringBuilder.AppendLine();
    //                                        stringBuilder.Append(PrintPropertiesPlain(protoObj, indent + 8));
    //                                        stringBuilder.Append(value2);
    //                                        stringBuilder.Append("}.Serialize(),");
    //                                        stringBuilder.AppendLine();
    //                                        stringBuilder.Append(value);
    //                                        stringBuilder.Append("},");
    //                                    }
    //                                    else
    //                                    {
    //                                        for (int j = 0; j < item.Length; j++)
    //                                        {
    //                                            byte b = item[j];
    //                                            stringBuilder.AppendFormat("0x{0:X2}", b);
    //                                            if (j < item.Length - 1)
    //                                            {
    //                                                stringBuilder.Append(", ");
    //                                            }
    //                                            else
    //                                                stringBuilder.Append(" ");
    //                                        }
    //                                        stringBuilder.Append("},");
    //                                    }
    //                                    stringBuilder.AppendLine();
    //                                }
    //                            }
    //                            else
    //                            {
    //                                stringBuilder.Append(value);
    //                                stringBuilder.Append(property.Name);
    //                                stringBuilder.Append($"= new List<{GetExtentionNameFromList(obj2.GetType().ToString())}>()");
    //                                stringBuilder.AppendLine();
    //                                stringBuilder.Append(value);
    //                                stringBuilder.Append("{");
    //                                stringBuilder.AppendLine();

    //                                if (list is IList<string>)
    //                                {
    //                                    stringBuilder.Append(PrintIListPlain<string>(list, indent + 4));
    //                                }
    //                                else if (list is IList<uint>)
    //                                {
    //                                    stringBuilder.Append(PrintIListPlain<uint>(list, indent + 4));
    //                                }
    //                                else if (list is IList<int>)
    //                                {
    //                                    stringBuilder.Append(PrintIListPlain<int>(list, indent + 4));
    //                                }
    //                                else if (list is IList<byte>)
    //                                {
    //                                    stringBuilder.Append(PrintIListPlain<byte>(list, indent + 4));
    //                                }
    //                                else if (list is IList<ulong>)
    //                                {
    //                                    stringBuilder.Append(PrintIListPlain<ulong>(list, indent + 4));
    //                                }
    //                                else if (list is IList<long>)
    //                                {
    //                                    stringBuilder.Append(PrintIListPlain<long>(list, indent + 4));
    //                                }
    //                                else
    //                                {
    //                                    foreach (var item in list)
    //                                    {
    //                                        stringBuilder.AppendLine(new string(' ', indent + 4) + $"new {GetClassName(item)}()");
    //                                        stringBuilder.Append(new string(' ', indent + 4));
    //                                        stringBuilder.Append("{");
    //                                        stringBuilder.AppendLine();
    //                                        stringBuilder.Append(PrintPropertiesPlain(item, indent + 8));
    //                                        stringBuilder.Append(new string(' ', indent + 4));
    //                                        stringBuilder.Append("},");
    //                                        stringBuilder.AppendLine();
    //                                    }
    //                                }
    //                                stringBuilder.Append(value);
    //                                stringBuilder.Append("},");
    //                                stringBuilder.AppendLine();

    //                            }
    //                        }


    //                    }
    //                    else
    //                    {
    //                        stringBuilder.Append("Other");
    //                        stringBuilder.Append(value);
    //                        stringBuilder.Append(property.Name + " ");
    //                        stringBuilder.Append("= ");
    //                        stringBuilder.Append(obj2);
    //                        stringBuilder.Append(",");
    //                        stringBuilder.AppendLine();
    //                    }
    //                }

    //            }
    //        }

    //        string result = stringBuilder.ToString();
    //        File.WriteAllText("c:/dump.txt", stringBuilder.ToString());
    //        stringBuilder.Clear();
    //        return result;
    //    }
    //    private static string PrintIListPlain<T>(object list, int indent)
    //    {
    //        Type typeFromHandle = typeof(T);
    //        Func<T, string> func = (T s) => s + ",";
    //        if (typeFromHandle.IsEquivalentTo(typeof(string)))
    //        {
    //            func = ((T s) => "\"" + s + "\", ");
    //        }
    //        else if (typeFromHandle.IsEquivalentTo(typeof(byte)))
    //        {
    //            func = ((T s) => $"{s:X2}" + " ");
    //        }
    //        else if (typeFromHandle.IsEquivalentTo(typeof(int)) || typeFromHandle.IsEquivalentTo(typeof(uint)) || typeFromHandle.IsEquivalentTo(typeof(float)) || typeFromHandle.IsEquivalentTo(typeof(double)) || typeFromHandle.IsEquivalentTo(typeof(long)) || typeFromHandle.IsEquivalentTo(typeof(ulong)))
    //        {
    //            func = ((T s) => s + ", ");
    //        }
    //        StringBuilder stringBuilder = new StringBuilder();
    //        IList<T> list2 = list as IList<T>;
    //        string text = new string(' ', indent);

    //        stringBuilder.Append(text);
    //        if (list2 != null)
    //        {
    //            for (int i = 0; i < Math.Min(400, list2.Count); i++)
    //            {
    //                T arg = list2[i];
    //                stringBuilder.Append(func(arg));
    //            }
    //            if (list2.Count > 400)
    //            {
    //                stringBuilder.Append("...");
    //            }
    //        }
    //        stringBuilder.Append("\r\n");
    //        return stringBuilder.ToString();
    //    }

    //    private static bool TryDeserializeBinary(byte[] bytes, string Name, out object obj)
    //    {
    //        obj = null;

    //        if (bytes.Length == 0)
    //        {
    //            return false;
    //        }
    //        switch (MsgType)
    //        {
    //            case 26:
    //            case 24:
    //                {
    //                    obj = GetType(bytes);
    //                    return obj != null;
    //                }
    //            case 4007:
    //            case 4004:
    //                if (Name == "game_data")
    //                {
    //                    obj = bytes.Deserialize<CMsgDOTAWelcome>();
    //                    return obj != null;
    //                }
    //                else
    //                {
    //                    obj = GetType(bytes);
    //                    return obj != null;
    //                    //if (proto == null)
    //                    //{
    //                    //    switch (LastID)
    //                    //        //    {
    //                    //                case 8024: code.AppendLine($"//CMsgGCTopCustomGamesList"); break;
    //                    //case 8075: code.AppendLine($"//CMsgGCToClientMatchGroupsVersion"); break;
    //                    //case 8067: code.AppendLine($"//CMsgDOTAChatRegionsEnabled"); break;
    //                    //case 7465: code.AppendLine($"//CMsgWeekendTourneySchedule"); break;
    //                    //case 2002: code.AppendLine($"//CSODOTAGameAccountClient"); break;
    //                    //case 2012: code.AppendLine($"//CSODOTAGameAccountPlus"); break;
    //                    //    }

    //                    //    if (InList) code.AppendLine($"{Name}");
    //                    //    else code.AppendLine($"{Name} = ");
    //                    //    code.AppendLine($"new byte[] " + "{" + GetHexFromBytes(bytes) + " },");
    //                    //    return code.ToString();
    //                    //}
    //                }
    //            default:
    //                obj = GetType(bytes);
    //                return obj != null;
    //        }
    //    }
    //    public static object GetType(byte[] bytes)
    //    {
    //        object Interface = null;
    //        Console.WriteLine(LastID);
    //        switch (LastID)
    //        {
    //            case 8024: try { Interface = bytes.Deserialize<CMsgGCTopCustomGamesList>(); } catch { } break;
    //            case 8075: try { Interface = bytes.Deserialize<CMsgGCToClientMatchGroupsVersion>(); } catch { } break;
    //            case 8067: try { Interface = bytes.Deserialize<CMsgDOTAChatRegionsEnabled>(); } catch { } break;
    //            case 7465: try { Interface = bytes.Deserialize<CMsgWeekendTourneySchedule>(); } catch { } break;
    //            case 2002: try { Interface = bytes.Deserialize<CSODOTAGameAccountClient>(); } catch { } break;
    //            case 2012: try { Interface = bytes.Deserialize<CSODOTAGameAccountPlus>(); } catch { } break;
    //            case 2591: try { Interface = bytes.Deserialize<CMsgItemAges>(); } catch { } break;
    //            case 2010: try { Interface = bytes.Deserialize<CSODOTAPlayerChallenge>(); } catch { } break;
    //            case 1: try { Interface = bytes.Deserialize<CSOEconItem>(); } catch { } break;
    //            case 2011: try { Interface = bytes.Deserialize<CSODOTALobbyInvite>(); } catch { } break;
    //            case 2004: try { Interface = bytes.Deserialize<CSODOTALobby>(); } catch { } break;
    //            case 2003: try { Interface = bytes.Deserialize<CSODOTAParty>(); } catch { } break;
    //            case 2006: try { Interface = bytes.Deserialize<CSODOTAPartyInvite>(); } catch { } break;
    //            case 7: try { Interface = bytes.Deserialize<CSOEconGameAccountClient>(); } catch { } break;

    //        }

    //        return Interface;
    //    }

    //    public static string GetExtentionName(object v)
    //    {
    //        string[] parts = v.ToString().Split('.');
    //        return parts[parts.Count() - 1].Replace("+", ".").Replace("[", "").Replace("]", "");
    //    }
    //    public static string GetExtentionNameFromList(object v)
    //    {
    //        string[] parts = v.ToString().Split('.');
    //        string name = parts[parts.Count() - 1].Replace("+", ".");
    //        return name.Remove(name.Count() - 1);
    //    }
    //    public static string GetClassName(object v)
    //    {
    //        string[] parts = v.ToString().Split('.');
    //        string name = parts[parts.Count() - 1];
    //        return name.Replace("+", ".");
    //    }
    //    public static T Deserialize<T>(this byte[] msg)
    //    {
    //        try
    //        {
    //            return Serializer.Deserialize<T>(new MemoryStream(msg));
    //        }
    //        catch (Exception)
    //        {
    //            return Serializer.Deserialize<T>(new MemoryStream(msg));
    //        }
    //    }

    //}

}
