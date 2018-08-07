using System.IO;
using Nancy;

namespace SharpStone.Server.BattleNet.Webservice
{
    public sealed class TestModule : NancyModule
    {
        public TestModule()
        {
            Get("/test", (arg) => View[Path.Combine("Modules", "TestHtml.html")]);
        }
    }
}
