using LCU.NET.API_Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public interface IPerks
    {
        Task<LolPerksPerkPageResource> GetCurrentPageAsync();
        Task PutCurrentPageAsync(int id);
        Task<LolPerksPerkPageResource[]> GetPagesAsync();
        Task DeletePageAsync(int id);
        Task<LolPerksPerkPageResource> GetPageAsync(int id);
        Task<LolPerksPerkPageResource> PutPageAsync(int id, LolPerksPerkPageResource page);
        Task<LolPerksPerkPageResource> PostPageAsync(LolPerksPerkPageResource page);
        Task<LolPerksPerkUIPerk[]> GetPerksAsync();
        Task<LolPerksPlayerInventory> GetInventoryAsync();
        Task<Image> GetPerkImageAsync(LolPerksPerkUIPerk perk);
    }

    public class Perks : IPerks
    {
        private ILeagueClient Client;
        private IPluginManager PluginManager;

        public Perks(ILeagueClient client, IPluginManager pluginManager)
        {
            this.Client = client;
            this.PluginManager = pluginManager;
        }

        /// <summary>
        /// Returns the current runes page.
        /// </summary>
        public Task<LolPerksPerkPageResource> GetCurrentPageAsync()
            => Client.MakeRequestAsync<LolPerksPerkPageResource>("/lol-perks/v1/currentpage", Method.GET);

        /// <summary>
        /// Sets the current rune page.
        /// </summary>
        /// <param name="id">The new page's ID.</param>
        public Task PutCurrentPageAsync(int id)
            => Client.MakeRequestAsync("/lol-perks/v1/currentpage", Method.PUT, id);

        /// <summary>
        /// Gets all the user's rune pages, including default ones.
        /// </summary>
        public async Task<LolPerksPerkPageResource[]> GetPagesAsync()
            => (await Client.MakeRequestAsync<List<LolPerksPerkPageResource>>("/lol-perks/v1/pages", Method.GET).ConfigureAwait(false)).ToArray();

        /// <summary>
        /// Deletes a rune page by ID.
        /// </summary>
        /// <param name="id">The page's ID.</param>
        public Task DeletePageAsync(int id)
            => Client.MakeRequestAsync($"/lol-perks/v1/pages/{id}", Method.DELETE);

        /// <summary>
        /// Gets a rune page by ID.
        /// </summary>
        /// <param name="id">The page's ID.</param>
        public Task<LolPerksPerkPageResource> GetPageAsync(int id)
            => Client.MakeRequestAsync<LolPerksPerkPageResource>($"/lol-perks/v1/pages/{id}", Method.GET);

        /// <summary>
        /// Updates a rune page.
        /// </summary>
        /// <param name="id">The page's ID.</param>
        /// <param name="page">The new page.</param>
        public Task<LolPerksPerkPageResource> PutPageAsync(int id, LolPerksPerkPageResource page)
            => Client.MakeRequestAsync<LolPerksPerkPageResource>($"/lol-perks/v1/pages/{id}", Method.PUT, page);

        /// <summary>
        /// Creates a new rune page.
        /// </summary>
        /// <param name="page">The new page.</param>
        public Task<LolPerksPerkPageResource> PostPageAsync(LolPerksPerkPageResource page)
            => Client.MakeRequestAsync<LolPerksPerkPageResource>("/lol-perks/v1/pages", Method.POST, page);

        /// <summary>
        /// Gets a list of all the runes in LoL.
        /// </summary>
        public async Task<LolPerksPerkUIPerk[]> GetPerksAsync()
            => (await Client.MakeRequestAsync<List<LolPerksPerkUIPerk>>("/lol-perks/v1/perks", Method.GET)).ToArray();
        
        public Task<LolPerksPlayerInventory> GetInventoryAsync()
            => Client.MakeRequestAsync<LolPerksPlayerInventory>("/lol-perks/v1/inventory", Method.GET);

        /// <summary>
        /// Gets a rune's icon.
        /// </summary>
        /// <param name="perk">The rune. See <see cref="GetPerks"/>.</param>
        public Task<Image> GetPerkImageAsync(LolPerksPerkUIPerk perk) => Cache(async () =>
        {
            string[] split = perk.iconPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string plugin = split[0];
            string path = string.Join("/", split.Skip(2));

            byte[] b = await PluginManager.GetAssetAsync(plugin, path).ConfigureAwait(false);

            using (var mem = new MemoryStream(b))
            {
                return Image.FromStream(mem);
            }
        });
    }
}
