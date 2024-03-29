using System;

namespace Roblox.Web.ElevatedActions.Base;

public interface IRobloxElevatedActionPerformanceCounterFactory
{
	IRobloxElevatedActionPerformanceCounters GetRobloxElevatedActionPerformanceCounters(Type typeOfIRobloxElevatedAction);

	IRobloxElevatedActionAuthorizationCheckerPerformanceCounters GetRobloxElevatedActionAuthorizationCheckerPerformanceCounters();
}
