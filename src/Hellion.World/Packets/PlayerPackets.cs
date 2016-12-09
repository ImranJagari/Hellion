using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.World
{
    public partial class WorldClient
    {
        public void SendPlayerSpawn()
        {
            using (var packet = new FFPacket())
            {
            packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.WeatherAll, 0x0000FF00);
            packet.Write(0); // Get weather by season


            packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.WorldInformation);
            packet.Write(this.Player.MapId);
            packet.Write(this.Player.Position.X);
            packet.Write(this.Player.Position.Y);
            packet.Write(this.Player.Position.Z);

            packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.ObjectSpawn);

            // Object properties
            packet.Write((byte)this.Player.Type);
            packet.Write(this.Player.ModelId);
            packet.Write((byte)this.Player.Type);
            packet.Write(this.Player.ModelId);
            packet.Write(this.Player.Size);
            packet.Write(this.Player.Position.X);
            packet.Write(this.Player.Position.Y);
            packet.Write(this.Player.Position.Z);
            packet.Write<short>(0); // angle
            packet.Write(this.Player.ObjectId);

            packet.Write<short>(0);
            packet.Write<byte>(1); // is player ?
            packet.Write(this.Player.Hp);
            packet.Write(0);
            packet.Write(0);
            packet.Write<byte>(1);

            // baby buffer
            packet.Write(-1);

            packet.Write(this.Player.Name);
            packet.Write(this.Player.Gender);
            packet.Write((byte)this.Player.SkinSetId);
            packet.Write((byte)this.Player.HairId);
            packet.Write((int)this.Player.HairColor);
            packet.Write((byte)this.Player.FaceId);
            packet.Write(this.Player.Id);
            packet.Write((byte)this.Player.ClassId);
            packet.Write((short)this.Player.Strength);
            packet.Write((short)this.Player.Stamina);
            packet.Write((short)this.Player.Dexterity);
            packet.Write((short)this.Player.Intelligence);
            packet.Write((short)this.Player.Level);
            packet.Write(-1); // Fuel
            packet.Write(0); // Actuel fuel

            // Guilds

            packet.Write<byte>(0); // have guild or not
            packet.Write(0); // guild cloak

            // Party

            packet.Write<byte>(0); // have party or not

            packet.Write((byte)this.currentUser.Authority);
            packet.Write(0); // mode
            packet.Write(0); // state mode
            packet.Write(0x000001F6); // item used ??
            packet.Write(0); // last pk time.
            packet.Write(0); // karma
            packet.Write(0); // pk propensity
            packet.Write(0); // pk exp
            packet.Write(0); // fame
            packet.Write<byte>(0); // duel
            packet.Write(-1); // titles

            for (int i = 0; i < 31; i++)
                packet.Write(0);

            packet.Write(0); // guild war state

            for (int i = 0; i < 26; ++i)
                packet.Write(0);

            packet.Write((short)this.Player.Mp);
            packet.Write((short)this.Player.Fp);
            packet.Write(0); // tutorial state
            packet.Write(0); // fly experience
            packet.Write(this.Player.Gold);
            packet.Write(this.Player.Experience);
            packet.Write(0); // skill level
            packet.Write(0); // skill points
            packet.Write<long>(0); // death exp
            packet.Write(0); // death level

            for (int i = 0; i < 32; ++i)
                packet.Write(0); // job in each level

            packet.Write(0); // marking world id
            packet.Write(this.Player.Position.X);
            packet.Write(this.Player.Position.Y);
            packet.Write(this.Player.Position.Z);

            // Quests
            packet.Write<byte>(0);
            packet.Write<byte>(0);
            packet.Write<byte>(0);

            packet.Write(42); // murderer id
            packet.Write<short>(0); // stat points
            packet.Write<short>(0); // always 0

            // item mask
            for (int i = 0; i < 31; i++)
                packet.Write(0);

            // skills
            for (int i = 0; i < 45; ++i)
            {
                packet.Write(0); // skill id
                packet.Write(0); // skill level
            }

            packet.Write<byte>(41); // cheer point
            packet.Write(42); // next cheer point ?

            // Bank
            packet.Write((byte)this.Player.Slot);
            for (int i = 0; i < 3; ++i)
                packet.Write(0); // gold
            for (int i = 0; i < 3; ++i)
                packet.Write(0); // player bank ?

            packet.Write(0x00000001); //ar << m_nPlusMaxHitPoint;
            packet.Write<byte>(0);  //ar << m_nAttackResistLeft;				
            packet.Write<byte>(0);  //ar << m_nAttackResistRight;				
            packet.Write<byte>(0);  //ar << m_nDefenseResist;
            packet.Write<long>(0x00000002); //ar << m_nAngelExp;
            packet.Write(42); //ar << m_nAngelLevel;

            // Inventory

            for (int i = 0; i < 73; ++i)
                packet.Write(i);

            packet.Write<byte>(0); // item count

            for (int i = 0; i < 73; ++i)
                packet.Write(i);

            // Bank

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 0x2A; ++j)
                    packet.Write(j);
                packet.Write<byte>(0); // count
                for (int j = 0; j < 0x2A; ++j)
                    packet.Write(j);
            }

            packet.Write(int.MaxValue); // pet id

            // Bag

            packet.Write<byte>(1);
            for (int i = 0; i < 6; i++)
            {
                packet.Write(i);
            }
            packet.Write<byte>(0);                 // Base bag item count
            for (int i = 0; i < 0; i++)
            {
                packet.Write((byte)i);             // Slot
                packet.Write(i);            // Slot
            }
            for (int i = 0; i < 6; i++)
            {
                packet.Write(i);
            }
            packet.Write(0);
            packet.Write(0);

            // premium bag
            packet.Write<byte>(0);

            packet.Write(0); // muted

            // Honor titles
            for (Int32 i = 0; i < 150; ++i)
                packet.Write(0);

            packet.Write(0); // id campus
            packet.Write(0); // campus points

            // buffs
            packet.Write(0); // count

            // Game time packet
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GameTime);
            //packet.Write<short>(0);
            //packet.Write<byte>(0);
            //packet.Write<byte>(0);
            //packet.Write<float>(0);


            //// Wheater
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.WeatherClear);
            //packet.Write(1);
            //packet.Write(0);

            //// party
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.PartyDefaultName);
            //packet.Write(0);

            //// total play time
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.AddGameJoin);
            //packet.Write(0);  //total play time

            //// server settings
            //packet.StartNewMergedPacket(-1, WorldHeaders.Outgoing.ServerSettings);
            //packet.Write<byte>(0x00);
            //packet.Write<byte>(0x01);  // duel enabled ?
            //packet.Write<byte>(0x01);  // Enable guild warehouse
            //packet.Write<byte>(0x01);  // Enable guild war
            //packet.Write<byte>(0); //Configuration.ClientSettings.EnabledFriendsList ? 0 : 1);  // school
            //packet.Write<byte>(0x00);  // school_battle
            //packet.Write<byte>(0x00); //no flymonster
            //packet.Write<byte>(0x00);  //no darkon
            //packet.Write<byte>(0x00);  //no guild
            //packet.Write<byte>(0x01);  // no wormon
            //packet.Write<byte>(0x00);  // despawn
            //packet.Write<byte>(0x01);  // PK
            //packet.Write<byte>(0x01);  // PKcost
            //packet.Write<byte>(0x00); //steal
            //packet.Write<byte>(0x00); //event0913
            //packet.Write<byte>(0x01);  // guildcombat
            //packet.Write<byte>(0x00); //dropitemremove
            //packet.Write<byte>(0x00); //event1206
            //packet.Write<byte>(0x00); //event1219
            //packet.Write<byte>(0x00); //event0127
            //packet.Write<byte>(0x00); //event0214
            //packet.Write<byte>(0x01);
            //packet.Write<byte>(0x01);  // combat war 1vs 1
            //packet.Write<byte>(0x01);  //arene
            //packet.Write<byte>(0x01);  //secret room
            //packet.Write<byte>(0x01);  //rainbow race

            //for (int i = 0; i < 998; ++i)
            //    packet.Write<byte>(0);

            //#region unknow end packet
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.Unknow3E);
            //packet.Write<Int16>(0);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GameRates);
            //packet.Write<Single>(0f);//Configuration.Rates.ShopCostRate);
            //packet.Write<Byte>(0);//(byte)RateType.ShopCost);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GameRates);
            //packet.Write<Single>(0f);//Configuration.Rates.ExpRate);
            //packet.Write<Byte>(0);//(byte)RateType.MonsterExpRate);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GameRates);
            //packet.Write<Single>(0f);//Configuration.Rates.DropRate);
            //packet.Write<Byte>(0);//(byte)RateType.ItemDropRate);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GameRates);
            //packet.Write<Single>(0f);//Configuration.Rates.GoldRate);
            //packet.Write<Byte>(0);//(byte)RateType.GoldDropRate);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GameRates);
            //packet.Write<Single>(0f);//Configuration.Rates.MonsterHitRate);
            //packet.Write<Byte>(0);//(byte)RateType.MonsterHitRate);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GameRates);
            //packet.Write<Single>(0f);//Configuration.Rates.MonsterHPRate);
            //packet.Write<Byte>(0);//(byte)RateType.MonsterHitpoint);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.MonsterProp);
            //packet.Write<Int64>(0);
            //#region new unknow from v14
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.AllAction);
            //packet.Write<Int64>(1);
            //#region GUILD COMBAT INFOS
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GuildCombat);
            //packet.Write<Byte>(0x08);
            //packet.Write<Int32>(0);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GuildCombat);
            //packet.Write<Byte>(0x30);
            //packet.Write<Int32>(1);
            //packet.Write<Int32>(0x69);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GuildCombat);
            //packet.Write<Byte>(0x31);
            //packet.Write<Int32>(Int32.MaxValue);//0xEDDE880F);
            //packet.Write<Int32>(0x00);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GuildCombat);
            //packet.Write<Byte>(0);
            //packet.Write<Int32>(1);
            //packet.Write<Int32>(0);
            //packet.Write<Int32>(0);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GuildCombat);
            //packet.Write<Byte>(7);
            //packet.Write<Int32>(1);
            //#endregion
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.SECRETROOM_MNG_STATE);
            //packet.Write<Int64>(1);
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.TaxAllInformations);
            //packet.Write<Int32>(3);
            //packet.Write<Byte>(0x0F);
            //packet.Write<Int32>(-1);
            //packet.Write<Int32>(3);
            //packet.Write<Byte>(0);
            //packet.Write<Int32>(0);
            //packet.Write<Byte>(1);
            //packet.Write<Int32>(0);
            //packet.Write<Byte>(2);
            //packet.Write<Int32>(0);
            //packet.Write<Byte>(0x0F);
            //packet.Write<Int32>(-1);
            //packet.Write<Int32>(2);
            //packet.Write<Byte>(0);
            //packet.Write<Int32>(0);
            //packet.Write<Byte>(1);
            //packet.Write<Int32>(0);
            //packet.Write<Byte>(0xFF);
            //packet.Write<Int32>(-1);
            //packet.Write<Int32>(3);
            //packet.Write<Byte>(0);
            //packet.Write<Int32>(0);
            //packet.Write<Byte>(1);
            //packet.Write<Int32>(0);
            //packet.Write<Byte>(2);
            //packet.Write<Int32>(0);
            //#region Maisons de guildes
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.GuildHouseAllInformations);
            //packet.Write<Int32>(1); //bSetFurnitureChannel
            //packet.Write<Int32>(0); //bHaveGuildHouse
            //                        /*
            //                        {
            //                            packet.Write<Int32>(0); //m_dwGuildId
            //                            packet.Write<Int32>(0); //worldID
            //                            packet.Write<Int32>(0); //m_tUpkeepTime
            //                            packet.Write<Int32>(0); //nSize
            //                        }*/
            //#endregion
            //#region Maisons
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.HousingAllInformations);
            //packet.Write<Int32>(0); //size infos housing
            //packet.Write<Int32>(0); //size info ami autorisé
            //#endregion

            //#endregion
            //#endregion

            //// Message hud
            //packet.StartNewMergedPacket(this.Player.ObjectId, WorldHeaders.Outgoing.MessageHud);
            //packet.Write("Welcome to Hellion Emulator!");

            this.Send(packet);
            }
        }
    }
}
