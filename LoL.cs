using LCU.NET.Plugins;
using LCU.NET.Plugins.LoL;
using Ninject;

namespace LCU.NET
{
    public interface ILoL
    {
        ILeagueClient Client { get; }
        ILeagueSocket Socket { get; }
        
        IPluginManager PluginManager { get; }
        IRiotClient RiotClient { get; }
        IProcessControl ProcessControl { get; }

        IChampions Champions { get; }
        IChampSelect ChampSelect { get; }
        IChat Chat { get; }
        IGameQueues GameQueues { get; }
        IItemsSets ItemSets { get; }
        ILobby Lobby { get; }
        ILogin Login { get; }
        IMaps Maps { get; }
        IMatchmaking Matchmaking { get; }
        IPerks Perks { get; }
        ISummoner Summoner { get; }
    }

    public class LoL : ILoL
    {
        public ILeagueClient Client { get; }
        public ILeagueSocket Socket => Client?.Socket;

        public LoL(ILeagueClient client)
        {
            this.Client = client;
        }

        public static ILoL CreateNew()
            => CreateNew(LeagueClient.CreateNew());

        public static ILoL CreateNew(ILeagueClient client)
        {
            KernelBase kernel = new StandardKernel();

            BindNinject(kernel, client);

            return kernel.Get<ILoL>();
        }

        public static void BindNinject(IKernel kernel)
            => BindNinject(kernel, LeagueClient.CreateNew());

        public static void BindNinject(IKernel kernel, ILeagueClient client)
        {
            kernel.Bind<ILoL>().To<LoL>();
            kernel.Bind<IPluginManager>().To<PluginManager>();
            kernel.Bind<IRiotClient>().To<RiotClient>();
            kernel.Bind<IProcessControl>().To<ProcessControl>();
            kernel.Bind<ILeagueClient>().ToConstant(client);
            kernel.Bind<IChampions>().To<Champions>();
            kernel.Bind<IChampSelect>().To<ChampSelect>();
            kernel.Bind<IChat>().To<Chat>();
            kernel.Bind<IGameQueues>().To<GameQueues>();
            kernel.Bind<IItemsSets>().To<ItemSets>();
            kernel.Bind<ILobby>().To<Lobby>();
            kernel.Bind<ILogin>().To<Login>();
            kernel.Bind<IMaps>().To<Maps>();
            kernel.Bind<IMatchmaking>().To<Matchmaking>();
            kernel.Bind<IPerks>().To<Perks>();
            kernel.Bind<ISummoner>().To<Summoner>();
        }

        [Inject]
        public IPluginManager PluginManager { get; set; }

        [Inject]
        public IRiotClient RiotClient { get; set; }

        [Inject]
        public IProcessControl ProcessControl { get; set; }


        [Inject]
        public IChampions Champions { get; set; }

        [Inject]
        public IChampSelect ChampSelect { get; set; }

        [Inject]
        public IChat Chat { get; set; }

        [Inject]
        public IGameQueues GameQueues { get; set; }

        [Inject]
        public IItemsSets ItemSets { get; set; }

        [Inject]
        public ILobby Lobby { get; set; }

        [Inject]
        public ILogin Login { get; set; }

        [Inject]
        public IMaps Maps { get; set; }

        [Inject]
        public IMatchmaking Matchmaking { get; set; }

        [Inject]
        public IPerks Perks { get; set; }

        [Inject]
        public ISummoner Summoner { get; set; }
    }
}
