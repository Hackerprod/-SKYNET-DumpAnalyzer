using NetHookAnalyzer2;
using NetHookAnalyzer2.Implementations;
using ProtoBuf;
using ProtoBuf.Meta;
using SKYNET.Steam;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SKYNET
{
    public class SKYNET_Helper
    {
        public static uint LastID { get; set; }
        public string gameid { get; set; }
        public static string GetSpace(int space)
        {
            string blankSpace = "    ";
            string Space = "";

            for (int i = 0; i < space; i++)
            {
                Space += blankSpace;
            }
            return Space;
        }
        public object GetInterface(byte[] bytes)
        {
            object Interface = null;

            try { Interface = bytes.Deserialize<CSOEconItemEquipped>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAGameAccountClient>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAGameAccountPlus>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAGameHeroFavorites>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAMapLocationState>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAParty>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAParty.State>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAPartyInvite>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAPartyMember>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTAPlayerChallenge>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconClaimCode>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconGameAccountClient>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconItem>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconItemAttribute>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconItemDropRateBonus>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconItemEventTicket>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconItemLeagueViewPass>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOEconItemTournamentPassport>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOItemCriteria>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOItemCriteriaCondition>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSOItemRecipe>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSourceTVGameSmall>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgSOCacheHaveVersion>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgDOTAWelcome.CExtraMsg>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgGCTopCustomGamesList>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgGCToClientMatchGroupsVersion>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgDOTAChatRegionsEnabled>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgDOTAChatRegionsEnabled.Region>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgWeekendTourneySchedule>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgWeekendTourneySchedule.Division>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgClientWelcome>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgClientWelcome.Location>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgSOCacheSubscribed>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgSOCacheSubscribed.SubscribedType>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgSOCacheSubscriptionCheck>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgSOIDOwner>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgDOTAWelcome>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CMsgSOCacheSubscribed>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }
            try { Interface = bytes.Deserialize<CSODOTALobbyInvite>(); if (HaveSpecified(Interface, bytes)) return Interface; } catch { }

            return Interface;
        }
        private static bool HaveSpecified(object @interface, byte[] bytes)
        {
            try
            {
                List<PropertyInfo> properties = GetPropertyInfos(@interface);
                List<object> objects = GetFieldsValues(new MemoryStream(bytes));

                properties = properties.FindAll((PropertyInfo y) => y.GetValue(@interface, null) is Int64);

                if (properties.Count != objects.Count) return false;
                modCommon.Show("lol");

                foreach (object item in objects)
                {
                    //modCommon.Show(item.GetType() + ": " + item);
                    var propertyInfo = properties.Find((PropertyInfo x) => x.GetValue(@interface, null) == item);
                    if (propertyInfo == null) return false;
                    //modCommon.Show(propertyInfo.Name);
                }
            }
            catch (Exception)
            {
                //modCommon.Show("Error");
                return false;
            }

            return true;
        }
        public static List<PropertyInfo> GetPropertyInfos(object body, bool WithSpecified = false)
        {
            List<PropertyInfo> properties = body.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

            if (WithSpecified)
                return properties;

            bool flag = body is IExtensible;
            if (flag)
            {
                properties = properties.Where(delegate (PropertyInfo x)
                {
                    if (x.Name.EndsWith("Specified"))
                    {
                        return properties.FirstOrDefault((PropertyInfo y) => y.Name == x.Name.Remove(x.Name.Length - 9)) == null;
                    }
                    return true;
                }).ToList();
            }
            return properties;
        }
        private static List<object> GetFieldsValues(MemoryStream memoryStream)
        {
            using (ProtoReader protoReader = new ProtoReader(memoryStream, null, null))
            {
                List<object> objects = new List<object>();
                while (true)
                {
                    int num = protoReader.ReadFieldHeader();
                    if (num == 0)
                    {
                        break;
                    }
                    object obj = null;
                    switch (protoReader.WireType)
                    {
                        case WireType.Variant:
                        case WireType.Fixed64:
                        case WireType.Fixed32:
                        case WireType.SignedVariant:
                            try { obj = protoReader.ReadInt64(); } catch { }
                            break;
                        case WireType.String:
                            //try { obj = protoReader.ReadString(); } catch { }
                            break;
                        default:
                            //obj = $"{protoReader.WireType} is not implemented";
                            break;
                    }
                    objects.Add(obj);
                }
                return objects;
            }

        }


        public static string GetHexFromBytes(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes);
            string[] Hexs = hex.Split('-');
            hex = "";
            for (int i = 0; i < Hexs.Length; i++)
            {
                if (i == Hexs.Length - 1)
                    hex += "0x" + Hexs[i];
                else
                    hex += "0x" + Hexs[i] + ", ";

            }
            return hex;
        }
        public static string GetExtentionName(object v)
        {
            string[] parts = v.ToString().Split('.');
            return parts[parts.Count() - 1].Replace("+", ".").Replace("[", "").Replace("]", "");
        }
        public static string GetClassName(object v)
        {
            string[] parts = v.ToString().Split('.');
            return parts[parts.Count() - 1];
        }

        public static object GetType(byte[] bytes)
        {
            object Interface = null;

            switch (LastID)
            {
                case 8024:  try { Interface = bytes.Deserialize<CMsgGCTopCustomGamesList>(); } catch { } break;
                case 8075:  try { Interface = bytes.Deserialize<CMsgGCToClientMatchGroupsVersion>(); } catch { } break;
                case 8067:  try { Interface = bytes.Deserialize<CMsgDOTAChatRegionsEnabled>(); } catch { } break;
                case 7465:  try { Interface = bytes.Deserialize<CMsgWeekendTourneySchedule>(); } catch { } break;
                case 2002:  try { Interface = bytes.Deserialize<CSODOTAGameAccountClient>(); } catch { } break;
                case 2012:  try { Interface = bytes.Deserialize<CSODOTAGameAccountPlus>(); } catch { } break;
                case 2591:  try { Interface = bytes.Deserialize<CMsgItemAges>(); } catch { } break;
                case 2010:  try { Interface = bytes.Deserialize<CSODOTAPlayerChallenge>(); } catch { } break;
                case 1:     try { Interface = bytes.Deserialize<CSOEconItem>(); } catch { } break;
                case 2011:  try { Interface = bytes.Deserialize<CSODOTALobbyInvite>(); } catch { } break;
                case 2004:  try { Interface = bytes.Deserialize<CSODOTALobby>(); } catch { } break;
                case 2003:  try { Interface = bytes.Deserialize<CSODOTAParty>(); } catch { } break;
                case 2006:  try { Interface = bytes.Deserialize<CSODOTAPartyInvite>(); } catch { } break;
                case 7:     try { Interface = bytes.Deserialize<CSOEconGameAccountClient>(); } catch { } break;


            }

            return Interface;
        }






    }
}