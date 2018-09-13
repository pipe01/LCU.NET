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
        private KernelBase Kernel = new StandardKernel();

        public LoL(ILeagueClient client)
        {
            Kernel.Bind<ILeagueClient>().ToConstant(client);
            Kernel.Bind<IChampions>().To<Champions>();
            Kernel.Bind<IChampSelect>().To<ChampSelect>();
            Kernel.Bind<IItemsSets>().To<ItemSets>();
            Kernel.Bind<ILobby>().To<Lobby>();
            Kernel.Bind<ILogin>().To<Login>();
            Kernel.Bind<IMatchmaking>().To<Matchmaking>();
            Kernel.Bind<IPerks>().To<Perks>();
        }

        [Inject]
        public IChampions Champions { get; }
        [Inject]
        public IChampSelect ChampSelect { get; }
        [Inject]
        public IItemsSets ItemSets { get; }
        [Inject]
        public ILobby Lobby { get; }
        [Inject]
        public ILogin Login { get; }
        [Inject]
        public IMatchmaking Matchmaking { get; }
        [Inject]
        public IPerks Perks { get; }
    }
}
