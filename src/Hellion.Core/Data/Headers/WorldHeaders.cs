using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Core.Data.Headers
{
    public class WorldHeaders
    {
        public enum Incoming : uint
        {
            Join = 0x0000FF00,
            ChangeNameScroll = 0x00000012,
            TeleportTown = 0x00000020,
            MailSend = 0x0000001A,
            MailDelete = 0x0000001B,
            MailTakeItem = 0x0000001C,
            MailShowList = 0x0000001D,
            MailTakeMoney = 0x0000001F,
            MailRead = 0x00000024,
            ChangeJobScroll = 0x00000F32,
            JoinWorldRequest = 0x0000FF00,
            UpdateSkills = 0x000F0003,
            Chat = 0x00FF0000,
            ItemMoveInIntentory = 0x00FF0006,
            ItemDrop = 0x00FF0007,
            ItemUnequip = 0x00FF000B,
            Attack = 0x00FF0010,
            BeginWandAttack = 0x00FF0011,
            BeginBowAttack = 0x00FF0012,
            SitDownbutton = 0x00FF0016,
            ItemDelete = 0x00FF0019,
            CastSkill = 0x00FF0020,
            ItemUsage = 0x00FF0021,
            FireProjectile = 0x00FF0022,
            TargetInformation = 0x00FF0023,
            QuestRemove = 0x00FF0026,
            QuestChecked = 0x88100110, // offi files
            TradeConfirmFinal = 0x00FF002F,
            TradeAccept = 0x00FF00A0,
            TradePutItem = 0x00FF00A1,
            TradeConfirmFirst = 0x00FF00A3,
            TradeWindowClosed = 0x00FF00A4,
            TradePutPenya = 0x00FF00A5,
            TradeRequest = 0x00FF00A7,
            TradeDecline = 0x00FF00A8,
            VendorShopOpen = 0x00FF00A9,
            VendorShopClose = 0x00FF00AA,
            VendorShopAddItem = 0x00FF00AB,
            VendorShopView = 0x00FF00AC,
            VendorShopBuyItem = 0x00FF00AD,
            VendorShopRemoveItem = 0x00FF00AE,
            Hairdesigner = 0x00FF00AF,
            NPCInteraction = 0x00FF00B0,
            NPCTradeStart = 0x00FF00B1,
            NPCTradeFinish = 0x00FF00B2,
            NPCTradeBuyItem = 0x00FF00B3,
            NPCTradeSellItem = 0x00FF00B4,
            ResurrectOriginal = 0x00FF00C0,
            ResurrectLodelight = 0x00FF00C1,
            ResurrectionSavedLodelight = 0x00ff00c2,
            ResurrectSelectLodelight = 0x00ff00c3, //old pang select lodelight function
            OnProjectileArrival = 0x00FF00D2,
            MakeupArtist = 0x00FF00EE,

            GuildCreateCloack = 0x00FF0FFD,

            ItemScrolling = 0x70000004,
            ItemScrollingRemoval = 0x70000005,
            ItemAwake = 0x70000008,
            EnterArena = 0x70000010,
            ExitArena = 0x70000011,

            /* Couple system */
            CouplePropose = 0x8FFFF000,
            CoupleRefuse = 0x8FFFF001,
            CoupleAccept = 0x8FFFF002, // SO MUCH LOVE !! <3
            CoupleDivorce = 0x8FFFF003,

            GuildSetLogo = 0xF000B010,
            GuildSetData = 0xF000B011,
            GuildSetNotice = 0xF000B012,
            GuildOpenBank = 0xF000B020,
            GuildPutItemBank = 0xF000B021,
            GuildRemoveItemBank = 0xF000B022,
            ItemOnItem = 0xF000B024,
            AddPiercingCard = 0xF000B025,
            GuildSetTitles = 0xF000B026,
            GuildSetSalary = 0xF000B027,
            GuildSetName = 0xF000B032,
            GuildDuelRequest = 0xF000B036,
            GuildDuelAccept = 0xF000B037,
            GuildDuelSurrender = 0xF000B047,
            GuildDuelTruceRequest = 0xF000B048,
            GuildDuelTruceAccept = 0xF000B049,
            EnterChatRoom = 0xF000B054, // by Shyro Morphy 1.2
            VendorOpenChat = 0xF000B058,
            VendorCloseChat = 0xF000B059,
            MoverFocus = 0xFFFFFF2D, // by Shyro Morphy 1.2 // waiting for now

            PerceItem = 0xF000D008,
            ViewEquipments = 0xF000D009,
            ItemRemoveElement = 0xF000D00B,
            CreateShiningOricalkum = 0xF000F110,
            CreateJewels = 0xF000F111,
            CreateUniqueWeapon = 0xF000F112,
            UpgradeUltimeWeapon = 0xF000F113,
            AddJewelEffect = 0xF000F114,
            RemoveJewel = 0xF000F115,
            CardExchange = 0xF000F116,
            NewStats = 0xF000F501,
            CreatePetFeed = 0xF000F605,
            FriendDataRequestSingle = 0xF000F802,
            QueryPlayerData2 = 0xF000F807,
            RemovePiercingCard = 0xF000F809,
            BuffPang = 0xF000F813,
            MapSecurity = 0xFFFFF000,
            MoveByMouse = 0xFFFFFF00,
            MoveByKeyboard = 0xFFFFFF01,
            Jump = 0xFFFFFF02,
            FlyMovement = 0xFFFFFF03,
            CorrectMovement = 0xFFFFFF05,
            CorrectFlyMovement = 0xFFFFFF06, // Offi files
            FollowObject = 0xFFFFFF07,
            CorrectMoverHeight = 0xFFFFFF08,//no no there is a use for this
            QuickslotDelete = 0xFFFFFF0A,
            QuickslotAdd = 0xFFFFFF0B,
            ShortkeyAdd = 0xFFFFFF0C,
            ShortkeyDelete = 0xFFFFFF0D,
            ActionSlotUpdate = 0xFFFFFF0E,
            PartySkillUsage = 0xFFFFFF1B,

            PartyAccept = 0xFFFFFF11,
            PartyKick = 0xFFFFFF12,
            PartyInvite = 0xFFFFFF17,
            PartyDecline = 0xFFFFFF18,
            PartyAdvanced = 0xFFFFFF19,
            PartyItemDistribution = 0xFFFFFF20,
            PartyEXPDistribution = 0xFFFFFF21,
            MovementFlyRotation = 0xFFFFFF29,
            PartySetLeader = 0xFFFFFF2F,
            GuildDisband = 0xFFFFFF32,
            GuildAccept = 0xFFFFFF33,
            GuildLeave = 0xFFFFFF34,
            GuildInvite = 0xFFFFFF35,
            GuildSetRank = 0xFFFFFF3A,
            GuildCloseBank = 0xFFFFFF3E,
            BankAccessRequest = 0xFFFFFF40,
            BankClose = 0xFFFFFF41,
            BankInsertItem = 0xFFFFFF42,
            BankInsertPenya = 0xFFFFFF43,
            BankWithdrawItem = 0xFFFFFF44,
            BankWithdrawPenya = 0xFFFFFF45,
            BankSetPassword = 0xFFFFFF47,
            BankCheckPassword = 0xFFFFFF48,
            BankSwitchItem = 0xFFFFFF49,
            FriendAccept = 0xFFFFFF60,
            FriendAddByMenu = 0xFFFFFF61,
            FriendDecline = 0xFFFFFF62,
            FriendUserdataRequest = 0xFFFFFF64,
            FriendSetState = 0xFFFFFF67,
            FriendTriggerBlock = 0xFFFFFF68,
            FriendDelete = 0xFFFFFF6A,
            FriendAdd = 0xFFFFFF6B,
            FollowerArrived = 0xFFFFFF72,
            GuildSetClass = 0xFFFFFF74,
            ResurrectionAccept = 0xFFFFFF78,
            ResurrectionDecline = 0xFFFFFF79,
            CancelSFXTimer = 0xFFFFFF7A,

            CollectStart = 0xf000f800,
            CollectStop = 0xf000f801,

            HeavenTower = 0x70001008, // By Shyro

            DoUseItemInput = 0x8FFFFF00, // By Aishiro

            #region Lord

            ELECTION_ADD_DEPOSIT = 0x8FFF0000,
            ELECTION_SET_PLEDGE = 0x8FFF0001,
            ELECTION_INC_VOTE = 0x8FFF0002,
            ELECTION_BEGIN_CANDIDACY = 0x8FFF0003,
            ELECTION_BEGIN_VOTE = 0x8FFF0004,
            ELECTION_END_VOTE = 0x8FFF0005,
            ELECTION_PROCESS = 0x8FFF0006,
            LORD = 0x8FFF0007,

            #endregion

            AchivementList = 0x70004000,
            SetAchivementTitle = 0x70004001,

            HouseLoadInfo = 0x70003000,
            HouseVisitRoom = 0x70003004,
            HouseQuitRoom = 0x70003007,
        }

        public enum Outgoing : uint
        {
            SessionKey = 0x00000000,
            MoverChat = 0x00000001,
            ItemCreate = 0x00000003,
            ItemMoveInInventory = 0x00000004,
            ItemChangeEquipState = 0x00000006,
            TradeAccept = 0x00000007,
            TradePutItem = 0x00000008,
            TradeConfirmFirst = 0x0000000A,
            TradeCancel = 0x0000000B,
            TradeComplete = 0x0000000C,
            Effect = 0x0000000F,

            MoverSetPosition = 0x00000010,
            SetLevel = 0x00000011,
            UpdateCombatData = 0x00000012,   // Using StartNewMergedPacket()
            ChangeName = 0x00000012,   // Using StartPacket()
            ShowDamage = 0x00000013,
            OpenNPCShop = 0x00000014,
            UpdateItem = 0x00000018,
            SkillMotion = 0x00000019,
            SkillCancel = 0x0000001A,

            UpdateFinished = 0x0000001B,
            AttributeIncrease = 0x0000001C,
            AttributeDecrease = 0x0000001D,
            AttributeSet = 0x0000001E,
            MoverDiePosition = 0x0000001F,      //allow to determin next spawn position of the mob replacing died mob (x,y,z,angle)

            TradePutPenya = 0x00000020,
            TradeRequest = 0x00000022,
            TradeDecline = 0x00000023,
            NPCDialog = 0x00000024,
            ResurrectionOffer = 0x00000027,
            TradeConfirmFinalShow = 0x0000002B,
            TradeConfirmFinalPress = 0x0000002C,
            GameRates = 0x0000002E,

            /// <summary>
            /// Used to set the Icon for timed scrolls, such as the Scroll of Full Shout..
            /// </summary>
            QuestRemove = 0x0000003A,
            PartySendPosition = 0x0000003C,
            Unknow3E = 0x0000003E,
            ScrollIcon = 0x0000003F,

            ReputationUpdate = 0x00000040,
            InstantMove = 0x00000041,
            VendorShopOpen = 0x00000042,
            VendorShopClose = 0x00000043,
            VendorShopAddItem = 0x00000044,
            VendorShopShow = 0x00000045,
            VendorShopSetQuantity = 0x00000046,
            VendorShopRemoveItem = 0x00000047,
            UpdateHairstyle = 0x00000048,
            Unknown49 = 0x00000049,
            FuelUpdate = 0x0000004B,
            Buff = 0x0000004C,
            UpdateFacemodel = 0x0000004D,
            MonsterProp = 0x0000004E,

            BankInsertItem = 0x00000050,
            BankWithdrawItem = 0x00000051,
            BankSetPenya = 0x00000052,
            BankUpdateItem = 0x00000054,
            BankGetPassword = 0x00000056,
            BankOpen = 0x00000058,

            AddStateTime = 0x00000059, // STUN
            InstantMove2 = 0x0000005F,

            WeatherClear = 0x00000060,
            WeatherSnow = 0x00000061,
            WeatherRain = 0x00000062,
            WeatherAll = 0x00000063,
            PKParameters = 0x00000065,
            PartyDefaultName = 0x00000068,
            PartyChat = 0x00000069,
            UpdateStats = 0x0000006A,

            FriendAdd = 0x00000070,
            FriendInvite = 0x00000071,
            FriendDecline = 0x00000072,
            FriendGetName = 0x00000073,
            AddFriendGameJoin = 0x00000074,
            FriendDelete = 0x00000075,
            FriendErrorAdding = 0x00000076,
            FriendChangeClass = 0x00000077,
            AddGameJoin = 0x00000078,
            PartySetLeader = 0x00000079,
            GuildDuelState = 0x0000007A,
            VendorShopChatCommand = 0x0000007B,
            UpdateSkills = 0x0000007D,

            PartyNoTarget = 0x00000081,
            PartyUpdate = 0x00000082,
            PartyInvite = 0x00000083,
            PartyDecline = 0x00000084,
            PartyLevel = 0x00000085,
            PartyOldName = 0x00000088,
            PartyAdvanced = 0x00000089,
            PartytySkillCall = 0x0000008A,
            PartytySkillBlitz = 0x0000008B,
            PartySkillRetreat = 0x0000008C,
            PartyBuff = 0x0000008D,
            PartySphereCircle = 0x0000008E,
            PartyItemDistrib = 0x0000008F,
            PartyExpDistrib = 0x00000090,
            PartyMemberNetstat = 0x00000091,
            MessageHud = 0x00000093,
            MessageItemUpgrade = 0x00000094,
            MessageInformation = 0x00000095,
            GameTime = 0x00000096,
            BindingsData = 0x00000097,
            MoverAction = 0x00000098,
            GuildInvite = 0x0000009A,
            GuildJoin = 0x0000009B,
            GuildCreate = 0x0000009C,
            GuildDisband = 0x0000009D,
            GuildDataSingle = 0x0000009E,
            GuildDataAll = 0x0000009F,

            // No idea about a name of this cause it contains both MessageBox and a green text!
            MessageNotice = 0x000000A0,
            MessageBox = 0x000000A1,
            MoverRevive = 0x000000A2,
            UpdateStatPoints = 0x000000A6,
            UpdateClass = 0x000000A7,
            ViewEquipments = 0x000000AC,

            DiaryUpdate = 0x000000B0,
            ServerSettings = 0x000000B2,
            UpdateCheerData = 0x000000B4,
            AllAction = 0x000000B7,
            GuildCombat = 0x000000B8,
            MessageEvent = 0x000000B9,

            MoveToPoint = 0x000000C1,
            MoveToMover = 0x000000C2,
            ActionBarGrey = 0x000000C5,
            Death = 0x000000C7,
            MoveByKeyboard = 0x000000CA,
            MoveFly = 0x000000CC,
            MoveFlyRotation = 0x000000CE,

            Shout = 0x000000D0,
            PlayBackgroundMusic = 0x000000D1,
            PlaySound = 0x000000D2,
            UpdatePlayerFlags = 0x000000D3,
            GuildBankRemoveItem = 0x000000D4, // remove bank
            AddState = 0x000000D7, // STUN
            SFXTimer = 0x000000DF,

            AttackMelee = 0x000000E0,
            AttackBow = 0x000000E2,
            AttackMonster = 0x000000E3,
            MailReadMessage = 0x000000E7,
            MailShowList = 0x000000E9,
            ResurrectScreenRemoval = 0x000000EB,

            GuildBankAddItem = 0x000000EF,


            ObjectSpawn = 0x000000F0,
            ObjectDespawn = 0x000000F1,
            MapTransfer = 0x000000F2,
            ChangeModel = 0x000000F5, // (dword)model ID
            MessageMails = 0x000000F7,
            PartyItemDistribution = 0x000000F8,
            RemoveState = 0x000000F8, // STUN
            GuildOpenBank = 0x000000FA, // Open guild bank
            GuildLogo = 0x000000FB,
            GuildUpdateData = 0x000000FC, //add penya xo or level to guild data
            GuildNotice = 0x000000FD,
            GuildAuthority = 0x000000FE,
            GuildSalary = 0x000000FF,

            NPCCreateResult = 0x00000100,
            NPCExchangeResult = 0x00000101,
            PetFeedCreationResult = 0x00000117,
            SpeedModifier = 0x00000118,
            SpecialEventMessage = 0x00000121,
            CloseConfirmWindows = 0x00000122,
            ItemAwakening = 0x00000140,
            FriendDataMerged = 0x00000141,
            HeavenTower = 0x70001008, // By Shyro

            SECRETROOM_MNG_STATE = 0x00000300,

            TaxAllInformations = 0x00000400,
            GuildHouseAllInformations = 0x00008812,
            HousingAllInformations = 0x00009200,
            WorldInformation = 0x00009910,

            AnnounceNotice = 0x00FF00EA,
        }
    }
}
