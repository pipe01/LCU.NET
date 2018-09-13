using LCU.NET.Plugins;
using LCU.NET.Plugins.LoL;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET
{
    public interface ILoL
    {
        ILeagueClient Client { get; }

        IChampions Champions { get; }
        IChampSelect ChampSelect { get; }
        IItemsSets ItemSets { get; }
        ILobby Lobby { get; }
        ILogin Login { get; }
        IMatchmaking Matchmaking { get; }
        IPerks Perks { get; }
    }

    public class LoL : ILoL
    {
        public ILeagueClient Client { get; }

        public LoL(ILeagueClient client)
        {
            this.Client = client;
        }

        public static ILoL CreateNew()
            => CreateNew(LeagueClient.CreateNew());

        public static ILoL CreateNew(ILeagueClient client)
        {
            KernelBase kernel = new StandardKernel();

            kernel.Bind<ILoL>().To<LoL>();
            kernel.Bind<IPluginManager>().To<PluginManager>();
            kernel.Bind<IRiotClient>().To<RiotClient>();
            kernel.Bind<ILeagueClient>().ToConstant(client);
            kernel.Bind<IChampions>().To<Champions>();
            kernel.Bind<IChampSelect>().To<ChampSelect>();
            kernel.Bind<IItemsSets>().To<ItemSets>();
            kernel.Bind<ILobby>().To<Lobby>();
            kernel.Bind<ILogin>().To<Login>();
            kernel.Bind<IMatchmaking>().To<Matchmaking>();
            kernel.Bind<IPerks>().To<Perks>();

            return kernel.Get<ILoL>();
        }

        [Inject]
        public IPluginManager PluginManager { get; set; }

        [Inject]
        public IRiotClient RiotClient { get; set; }


        [Inject]
        public IChampions Champions { get; set; }

        [Inject]
        public IChampSelect ChampSelect { get; set; }

        [Inject]
        public IItemsSets ItemSets { get; set; }

        [Inject]
        public ILobby Lobby { get; set; }

        [Inject]
        public ILogin Login { get; set; }

        [Inject]
        public IMatchmaking Matchmaking { get; set; }

        [Inject]
        public IPerks Perks { get; set; }
    }
}
