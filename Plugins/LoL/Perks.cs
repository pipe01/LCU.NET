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
    public static class Perks
    {
        /// <summary>
        /// Returns the current runes page.
        /// </summary>
        [APIMethod("/lol-perks/v1/currentpage", Method.GET)]
        public static Task<LolPerksPerkPageResource> GetCurrentPageAsync()
            => MakeRequestAsync<LolPerksPerkPageResource>();

        /// <summary>
        /// Sets the current rune page.
        /// </summary>
        /// <param name="id">The new page's ID.</param>
        [APIMethod("/lol-perks/v1/currentpage", Method.PUT)]
        public static Task PutCurrentPageAsync(int id)
            => MakeRequestAsync(id);

        /// <summary>
        /// Gets all the user's rune pages, including default ones.
        /// </summary>
        [APIMethod("/lol-perks/v1/pages", Method.GET)]
        public static async Task<LolPerksPerkPageResource[]> GetPagesAsync()
            => (await MakeRequestAsync<List<LolPerksPerkPageResource>>().ConfigureAwait(false)).ToArray();

        /// <summary>
        /// Gets a rune page by ID.
        /// </summary>
        /// <param name="id">The page's ID.</param>
        public static Task<LolPerksPerkPageResource> GetPageAsync(int id)
            => Default.MakeRequestAsync<LolPerksPerkPageResource>("/lol-perks/v1/pages/" + id, Method.GET);

        /// <summary>
        /// Updates a rune page.
        /// </summary>
        /// <param name="id">The page's ID.</param>
        /// <param name="page">The new page.</param>
        public static Task<LolPerksPerkPageResource> PutPageAsync(int id, LolPerksPerkPageResource page)
            => Default.MakeRequestAsync<LolPerksPerkPageResource>("/lol-perks/v1/pages/" + id, Method.PUT, page);

        /// <summary>
        /// Creates a new rune page.
        /// </summary>
        /// <param name="page">The new page.</param>
        [APIMethod("/lol-perks/v1/pages", Method.POST)]
        public static Task<LolPerksPerkPageResource> PostPageAsync(LolPerksPerkPageResource page)
            => MakeRequestAsync<LolPerksPerkPageResource>(page);

        /// <summary>
        /// Gets a list of all the runes in LoL.
        /// </summary>
        [APIMethod("/lol-perks/v1/perks", Method.GET, Cache = true)]
        public static async Task<LolPerksPerkUIPerk[]> GetPerksAsync()
            => (await MakeRequestAsync<List<LolPerksPerkUIPerk>>().ConfigureAwait(false)).ToArray();

        /// <summary>
        /// Gets a rune's icon.
        /// </summary>
        /// <param name="perk">The rune. See <see cref="GetPerks"/>.</param>
        public static Task<Image> GetPerkImageAsync(LolPerksPerkUIPerk perk) => Cache(async () =>
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

        [APIMethod("/lol-perks/v1/inventory", Method.GET)]
        public static Task<LolPerksPlayerInventory> GetInventoryAsync() => MakeRequestAsync<LolPerksPlayerInventory>();
    }
}
