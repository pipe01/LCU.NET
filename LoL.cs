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
        private static KernelBase Kernel;
        
        public static ILoL Instance(ILeagueClient client)
        {
            if (Kernel == null)
            {
                Kernel = new StandardKernel();
                Kernel.Bind<ILoL>().To<LoL>();
                Kernel.Bind<ILeagueClient>().ToConstant(client);
                Kernel.Bind<IChampions>().To<Champions>();
                Kernel.Bind<IChampSelect>().To<ChampSelect>();
                Kernel.Bind<IItemsSets>().To<ItemSets>();
                Kernel.Bind<ILobby>().To<Lobby>();
                Kernel.Bind<ILogin>().To<Login>();
                Kernel.Bind<IMatchmaking>().To<Matchmaking>();
                Kernel.Bind<IPerks>().To<Perks>();
            }

            return Kernel.Get<ILoL>();
        }

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
