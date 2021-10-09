using NetHookAnalyzer2;
using NetHookAnalyzer2.Implementations;
using ProtoBuf;
using ProtoBuf.Meta;
using SKYNET.Steam.GC;
using SteamKit2;
using SteamKit2.GC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SKYNET.Steam
{
	public static class MsgHelpers_Formated
    {
        private static uint LastID;
        private static uint MsgType;

        public static string GetReflectedMsg(object body, int indent = 2)
        {
            if (body is CMsgGCClient)
            {
                try
                {
                    CMsgGCClient client = body as CMsgGCClient;
                    CoordinatorMessage message = new CoordinatorMessage(client);
                    if (message.Body != null)
                    {
                        MsgType = message.MsgType;
                        return GetProcessedMessage(message.Body);
                    }
                    else
                    {
                        return PrintProperties(body);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "";
            }

        }
        public static string GetProcessedMessage(IExtensible msg, int indent = 2)
		{
            Type type = msg.GetType();
            StringBuilder stringBuilder = new StringBuilder();

            object obj2 = msg;
            if (obj2 != null)
            {
                string value = new string(' ', 4);
                string classname = GetClassName(obj2.ToString());
                stringBuilder.AppendLine("<div style=\"color:Black;background-color:white;\"><pre>");
                stringBuilder.AppendLine($"{GetWithHTMLColor("ClientGCMsgProtobuf", "2b91af")}<{ GetWithHTMLColor(classname, "2b91af") }> { classname.Replace("CMsg", "") } = { GetWithHTMLColor("new", "0000ff") } {GetWithHTMLColor("ClientGCMsgProtobuf", "2b91af")}<{GetWithHTMLColor(classname, "2b91af") }>({ MsgType }U)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine(value + "Body = ");
                stringBuilder.AppendLine(value + "{");
                string code = PrintProperties(obj2, 8);
                stringBuilder.AppendLine(code);
                stringBuilder.AppendLine(value + "}");
                stringBuilder.AppendLine("};");
                stringBuilder.AppendLine("</pre>");
                stringBuilder.AppendLine("</div>");


            }
            return stringBuilder.ToString();
		}



        private static string PrintProperties(object obj, int indent = 1)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            string value = new string(' ', indent);
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            int len = (properties.Length != 0) ? properties.Max((PropertyInfo p) => p.Name.Length) : 15;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PropertyInfo property in properties)
            {
                if (!property.Name.EndsWith("Specified"))
                {
                    object obj2 = null;
                    try
                    {
                        obj2 = property.GetValue(obj, null);
                    }
                    catch { }

                    PropertyInfo propertyInfo = properties.FirstOrDefault((PropertyInfo p) => p.Name.Equals(property.Name + "Specified"));
                    bool flag = (bool)(propertyInfo?.GetValue(obj, null) ?? ((object)false));
                    if (obj2 == null)
                    {
                        stringBuilder.Append(value);
                        stringBuilder.Append(property.Name + " ");
                        stringBuilder.Append($"= {GetWithHTMLColor("null", "0000ff")},");
                        stringBuilder.AppendLine();
                    }
                    else if (!(obj2 is Stream))
                    {
                        //Done
                        if (obj2.GetType().IsEnum)
                        {
                            stringBuilder.Append(value);

                            if (int.TryParse(obj2.ToString(), out int enumCode))
                            {
                                stringBuilder.Append(property.Name + " ");
                                stringBuilder.Append("= ");
                                stringBuilder.Append("(" + GetWithHTMLColor(GetExtentionName(obj2.GetType().ToString()), "2b91af") + ")" + enumCode.ToString() + ",");
                            }
                            else
                            {
                                stringBuilder.Append(property.Name + " ");
                                stringBuilder.Append("= ");
                                stringBuilder.Append(GetWithHTMLColor(GetExtentionName(obj2.GetType().ToString()), "2b91af") + "." + obj2.ToString() + ",");
                            }
                            stringBuilder.AppendLine();
                        }
                        else if (obj2.GetType() == typeof(byte) || obj2.GetType() == typeof(int) || obj2.GetType() == typeof(uint) || obj2.GetType() == typeof(long) || obj2.GetType() == typeof(ulong) || obj2.GetType() == typeof(float) || obj2.GetType() == typeof(double))
                        {
                            if (property.Name.ToLower() == "id" || property.Name.ToLower() == "type_id")
                            {
                                LastID = (uint)Convert.ToInt64(obj2);
                            }

                            var objValue = obj2.ToString().Replace(",", ".");
                            objValue = obj2.GetType() == typeof(float) ? objValue += "f" : objValue += "";

                            stringBuilder.Append(value);
                            stringBuilder.Append(property.Name + " ");
                            stringBuilder.Append("= ");
                            stringBuilder.Append(objValue);
                            stringBuilder.Append(",");
                            stringBuilder.AppendLine();
                        }
                        else if (obj2.GetType() == typeof(string))
                        {
                            stringBuilder.Append(value);
                            stringBuilder.Append(property.Name);
                            stringBuilder.Append($" = { GetWithHTMLColor("\"", "a31515") }");
                            stringBuilder.Append(GetWithHTMLColor(obj2, "a31515"));
                            stringBuilder.Append(GetWithHTMLColor("\"", "a31515"));
                            stringBuilder.Append(",");

                            stringBuilder.AppendLine();
                        }
                        else if (property.PropertyType.Assembly == type.Assembly)
                        {
                            stringBuilder.Append(value);
                            stringBuilder.Append(property.Name);
                            stringBuilder.Append($" = { GetWithHTMLColor("new", "0000ff") } { GetWithHTMLColor(GetExtentionName(obj2.GetType().ToString()), "2b91af") }()"); 
                            stringBuilder.AppendLine();
                            stringBuilder.Append(value);
                            stringBuilder.Append("{");
                            stringBuilder.AppendLine();
                            stringBuilder.Append(PrintProperties(obj2, indent + 4));
                            stringBuilder.Append(value);
                            stringBuilder.Append("},");
                            stringBuilder.AppendLine();
                        }
                        else if (obj2.GetType() == typeof(bool))
                        {
                            stringBuilder.Append(value);
                            stringBuilder.Append(property.Name);
                            stringBuilder.Append(" = ");
                            stringBuilder.Append( GetWithHTMLColor(obj2.ToString().ToLower(), "0000ff") );
                            stringBuilder.Append(",");
                            stringBuilder.AppendLine();
                        }
                        else if (obj2.GetType() == typeof(byte[]))
                        {
                            byte[] item = obj2 as byte[];
                            stringBuilder.Append(value);
                            stringBuilder.Append(property.Name);
                            stringBuilder.Append(" = ");

                            if (TryDeserializeBinary(item, property.Name, out object protoObj))
                            {
                                stringBuilder.Append($"{ GetWithHTMLColor("new", "0000ff") } { GetWithHTMLColor(GetExtentionName(protoObj.GetType().ToString()), "2b91af") }()");
                                stringBuilder.AppendLine();
                                stringBuilder.Append(value);
                                stringBuilder.Append("{");
                                stringBuilder.AppendLine();
                                stringBuilder.Append(PrintProperties(protoObj, indent + 4));
                                stringBuilder.Append(value);
                                stringBuilder.Append("}.Serialize(),");

                            }
                            else
                            {
                                stringBuilder.Append($"{ GetWithHTMLColor("new byte", "0000ff")}[] ");
                                stringBuilder.Append("{ ");

                                for (int j = 0; j < item.Length; j++)
                                {
                                    byte b = item[j];
                                    stringBuilder.AppendFormat("0x{0:X2}", b);
                                    if (j < item.Length - 1)
                                    {
                                        stringBuilder.Append(", ");
                                    }
                                    else
                                        stringBuilder.Append(" ");
                                }
                                stringBuilder.Append("},");
                            }
                            stringBuilder.AppendLine();
                        }
                        else if (obj2 is IList)
                        {
                            IList list = obj2 as IList;
                            if (list != null)
                            {
                                if (list is IList<byte[]>)
                                {
                                    foreach (byte[] item in list as IList<byte[]>)
                                    {
                                        stringBuilder.Append(value);
                                        stringBuilder.Append(property.Name + " ");
                                        stringBuilder.AppendLine($"= { GetWithHTMLColor("new", "0000ff") } { GetWithHTMLColor("List", "2b91af")}<{ GetWithHTMLColor("byte", "0000ff") }[]>()"); 

                                        stringBuilder.Append(value);
                                        stringBuilder.Append("{");

                                        if (TryDeserializeBinary(item, property.Name, out object protoObj))
                                        {
                                            string value2 = new string(' ', indent + 4);
                                            stringBuilder.AppendLine();
                                            stringBuilder.Append(value2);
                                            stringBuilder.Append($"{ GetWithHTMLColor("new", "0000ff") } { GetWithHTMLColor(GetExtentionName(protoObj.GetType().ToString()), "2b91af")}()");
                                            stringBuilder.AppendLine();
                                            stringBuilder.Append(value2);
                                            stringBuilder.Append("{");
                                            stringBuilder.AppendLine();
                                            stringBuilder.Append(PrintProperties(protoObj, indent + 8));
                                            stringBuilder.Append(value2);
                                            stringBuilder.Append("}.Serialize(),");
                                            stringBuilder.AppendLine();
                                            stringBuilder.Append(value);
                                            stringBuilder.Append("},");
                                        }
                                        else
                                        {
                                            for (int j = 0; j < item.Length; j++)
                                            {
                                                byte b = item[j];
                                                stringBuilder.AppendFormat("0x{0:X2}", b);
                                                if (j < item.Length - 1)
                                                {
                                                    stringBuilder.Append(", ");
                                                }
                                                else
                                                    stringBuilder.Append(" ");
                                            }
                                            stringBuilder.Append("},");
                                        }
                                        stringBuilder.AppendLine();
                                    }
                                }
                                else
                                {
                                    stringBuilder.Append(value);
                                    stringBuilder.Append(property.Name);
                                    //stringBuilder.Append($" = { GetWithHTMLColor("new", "0000ff") } { GetWithHTMLColor("List", "2b91af") }<{GetExtentionNameFromList(obj2?.GetType().ToString())}>()");
                                    stringBuilder.Append($" = ");
                                    stringBuilder.AppendLine();
                                    stringBuilder.Append(value);
                                    stringBuilder.Append("{");
                                    stringBuilder.AppendLine();

                                    if (list is IList<string>)
                                    {
                                        stringBuilder.Append(PrintIList<string>(list, indent + 4));
                                    }
                                    else if (list is IList<uint>)
                                    {
                                        stringBuilder.Append(PrintIList<uint>(list, indent + 4));
                                    }
                                    else if (list is IList<int>)
                                    {
                                        stringBuilder.Append(PrintIList<int>(list, indent + 4));
                                    }
                                    else if (list is IList<byte>)
                                    {
                                        stringBuilder.Append(PrintIList<byte>(list, indent + 4));
                                    }
                                    else if (list is IList<ulong>)
                                    {
                                        stringBuilder.Append(PrintIList<ulong>(list, indent + 4));
                                    }
                                    else if (list is IList<long>)
                                    {
                                        stringBuilder.Append(PrintIList<long>(list, indent + 4));
                                    }
                                    else
                                    {
                                        foreach (var item in list)
                                        {
                                            stringBuilder.AppendLine(new string(' ', indent + 4) + $"{ GetWithHTMLColor("new", "0000ff") } { GetWithHTMLColor(GetClassName(item), "2b91af") }()");
                                            stringBuilder.Append(new string(' ', indent + 4));
                                            stringBuilder.Append("{");
                                            stringBuilder.AppendLine();
                                            stringBuilder.Append(PrintProperties(item, indent + 8));
                                            stringBuilder.Append(new string(' ', indent + 4));
                                            stringBuilder.Append("},");
                                            stringBuilder.AppendLine();
                                        }
                                    }
                                    stringBuilder.Append(value);
                                    stringBuilder.Append("},");
                                    stringBuilder.AppendLine();
                                }
                            }
                        }
                        else
                        {
                            stringBuilder.Append("Other");
                            stringBuilder.Append(value);
                            stringBuilder.Append(property.Name + " ");
                            stringBuilder.Append("= ");
                            stringBuilder.Append(obj2);
                            stringBuilder.Append(",");
                            stringBuilder.AppendLine();
                        }
                    }
                }
            }

            string result = stringBuilder.ToString();
            stringBuilder.Clear();
            return result;
        }
        private static string PrintIList<T>(object list, int indent)
        {
            Type typeFromHandle = typeof(T);
            Func<T, string> func = (T s) => s + ",";
            if (typeFromHandle.IsEquivalentTo(typeof(string)))
            {
                func = ((T s) => GetWithHTMLColor("\"" + s + "\"", "a31515") + ", ");
            }
            else if (typeFromHandle.IsEquivalentTo(typeof(byte)))
            {
                func = ((T s) => $"{s:X2}" + " ");
            }
            else if (typeFromHandle.IsEquivalentTo(typeof(int)) || typeFromHandle.IsEquivalentTo(typeof(uint)) || typeFromHandle.IsEquivalentTo(typeof(float)) || typeFromHandle.IsEquivalentTo(typeof(double)) || typeFromHandle.IsEquivalentTo(typeof(long)) || typeFromHandle.IsEquivalentTo(typeof(ulong)))
            {
                func = ((T s) => s + ", ");
            }
            StringBuilder stringBuilder = new StringBuilder();
            IList<T> list2 = list as IList<T>;
            string text = new string(' ', indent);

            stringBuilder.Append(text);
            if (list2 != null)
            {
                for (int i = 0; i < Math.Min(400, list2.Count); i++)
                {
                    T arg = list2[i];
                    stringBuilder.Append(func(arg));
                }
                if (list2.Count > 400)
                {
                    stringBuilder.Append("...");
                }
            }
            stringBuilder.Append("\r\n");
            return stringBuilder.ToString();
        }
        private static bool TryDeserializeBinary(byte[] bytes, string Name, out object obj)
        {
            obj = null;

            if (bytes.Length == 0)
            {
                return false;
            }

            switch (MsgType)
            {
                case 26:
                case 24:
                    {
                        obj = GetType(bytes);
                        return obj != null;
                    }
                case 4007:
                case 4004:

                    if (Name == "game_data")
                    {
                        obj = bytes.Deserialize<CMsgDOTAWelcome>();
                        return obj != null;
                    }
                    else
                    {
                        obj = GetType(bytes);
                        return obj != null;
            }
                default:
                    obj = GetType(bytes);
                    return obj != null;
            }
        }
        private static object GetType(byte[] bytes)
        {
            object Interface = null;
            Console.WriteLine(LastID);
            switch (LastID)
            {
                case 8024: try { Interface = bytes.Deserialize<CMsgGCTopCustomGamesList>(); } catch { } break;
                case 8075: try { Interface = bytes.Deserialize<CMsgGCToClientMatchGroupsVersion>(); } catch { } break;
                case 8067: try { Interface = bytes.Deserialize<CMsgDOTAChatRegionsEnabled>(); } catch { } break;
                case 7465: try { Interface = bytes.Deserialize<CMsgWeekendTourneySchedule>(); } catch { } break;
                case 2002: try { Interface = bytes.Deserialize<CSODOTAGameAccountClient>(); } catch { } break;
                case 2012: try { Interface = bytes.Deserialize<CSODOTAGameAccountPlus>(); } catch { } break;
                case 2591: try { Interface = bytes.Deserialize<CMsgItemAges>(); } catch { } break;
                case 2010: try { Interface = bytes.Deserialize<CSODOTAPlayerChallenge>(); } catch { } break;
                case 1: try { Interface = bytes.Deserialize<CSOEconItem>(); } catch { } break;
                case 2011: try { Interface = bytes.Deserialize<CSODOTALobbyInvite>(); } catch { } break;
                case 2004: try { Interface = bytes.Deserialize<CSODOTALobby>(); } catch { } break;
                case 2003: try { Interface = bytes.Deserialize<CSODOTAParty>(); } catch { } break;
                case 2006: try { Interface = bytes.Deserialize<CSODOTAPartyInvite>(); } catch { } break;
                case 7: try { Interface = bytes.Deserialize<CSOEconGameAccountClient>(); } catch { } break;

            }

            return Interface;
        }
        private static string GetExtentionName(object v)
        {
            string[] parts = v.ToString().Split('.');
            return parts[parts.Count() - 1].Replace("+", ".").Replace("[", "").Replace("]", "");
        }
        private static string GetExtentionNameFromList(object v)
        {
            string[] parts = v.ToString().Split('.');
            string name = parts[parts.Count() - 1].Replace("+", ".");
            return name.Remove(name.Count() - 1);
        }
        private static string GetClassName(object v)
        {
            string[] parts = v.ToString().Split('.');
            string name = parts[parts.Count() - 1];
            return name.Replace("+", ".");
        }
        private static string GetWithHTMLColor(object text, string color)
        {
            return $"<span style=\"color:#{color};\">{text}</span>";
        }

    }
}
