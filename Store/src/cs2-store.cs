using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using StoreApi;
using VipCoreApi;
using static VipCoreApi.IVipCoreApi;
using static StoreApi.Store;

namespace Store;

public class Store : BasePlugin, IPluginConfig<Item_Config>
{
	public override string ModuleName => "Store";
	public override string ModuleVersion => "1.4";
	public override string ModuleAuthor => "schwarper";

	public Item_Config Config { get; set; } = new Item_Config();
	public List<Store_Player> GlobalStorePlayers { get; set; } = [];
	public List<Store_Item> GlobalStorePlayerItems { get; set; } = [];
	public List<Store_Equipment> GlobalStorePlayerEquipments { get; set; } = [];
	public List<Store_Item_Types> GlobalStoreItemTypes { get; set; } = [];
	public Dictionary<CCSPlayerController, Player> GlobalDictionaryPlayer { get; set; } = [];
	public int GlobalTickrate { get; set; } = 0;
	private StoreAPI _storeApi = new();
	public VipStore? _store { get; set; } = null;
    private IVipCoreApi? _api;
	public static Store Instance { get; private set; } = new();
    private PluginCapability<IVipCoreApi> PluginCapability { get; } = new("vipcore:core");
	public override void OnAllPluginsLoaded(bool hotReload)
    {
        _api = PluginCapability.Get();
        if (_api == null) return;

        _store = new VipStore( _api);
        _api.RegisterFeature(_store, FeatureType.Hide);
    }

	public Random Random { get; set; } = new();
	public Dictionary<CCSPlayerController, float> GlobalGiftTimeout { get; set; } = [];

	public override void Load(bool hotReload)
	{
		// Create a single instance of StoreAPI
		_storeApi = new StoreAPI();

		// Register the capability with the existing instance
		Capabilities.RegisterPluginCapability(IStoreApi.Capability, () => _storeApi);

		Instance = this;
		// Instance._storeApi = _storeApi;

		Event.Load();
		Command.Load();
		Menu.SetSettings(hotReload);

		Item_Armor.OnPluginStart();
		Item_Bunnyhop.OnPluginStart();
		Item_ColoredSkin.OnPluginStart();
		Item_CustomWeapon.OnPluginStart();
		Item_Godmode.OnPluginStart();
		Item_Gravity.OnPluginStart();
		Item_GrenadeTrail.OnPluginStart();
		Item_Health.OnPluginStart();
		Item_Open.OnPluginStart();
		Item_PlayerSkin.OnPluginStart();
		Item_Respawn.OnPluginStart();
		Item_Smoke.OnPluginStart();
		Item_Sound.OnPluginStart();
		Item_Speed.OnPluginStart();
		Item_Tracer.OnPluginStart();
		Item_Trail.OnPluginStart();
		Item_Weapon.OnPluginStart();
		Item_Equipment.OnPluginStart();

		if (hotReload)
		{
			foreach (CCSPlayerController player in Utilities.GetPlayers())
			{
				Database.LoadPlayer(player);
			}
		}
	}

	public override void Unload(bool hotReload)
	{
        _api?.UnRegisterFeature(_store!);
		Event.Unload();
	}

	public void OnConfigParsed(Item_Config config)
	{
		Config_Config.Load();

		Config = config;
	}
}
