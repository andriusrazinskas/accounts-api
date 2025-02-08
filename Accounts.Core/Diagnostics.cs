using System.Diagnostics;

namespace Accounts.Core
{
    // Recommended practice to have ActivitySource per library and store it in static variable
    // https://learn.microsoft.com/en-us/dotnet/core/diagnostics/distributed-tracing-instrumentation-walkthroughs#best-practices-1
    internal static class Diagnostics
    {
        internal static ActivitySource ActivitySource = new("Accounts.Core");
    }
}
