using CounterStrikeSharp.API.Core;
using VipCoreApi;

namespace Store;

public class VipStore(IVipCoreApi api) : VipFeatureBase(api)
{
	public override string Feature => "Store";

	public float GetVipBonus(CCSPlayerController player)
	{
		if (!IsClientVip(player)) return 0;
		return GetFeatureValue<float>(player);
	}
}
