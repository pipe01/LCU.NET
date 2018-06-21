using LCU.NET.API_Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Perks
    {
        /// <summary>
        /// Returns the current runes page.
        /// </summary>
        public static LolPerksPerkPageResource GetCurrentPage()
            => MakeRequest<LolPerksPerkPageResource>("/lol-perks/v1/currentpage", Method.GET);

        /// <summary>
        /// Sets the current rune page.
        /// </summary>
        /// <param name="id">The new page's ID.</param>
        public static void PutCurrentPage(int id)
            => MakeRequest("/lol-perks/v1/currentpage", Method.PUT, id);

        /// <summary>
        /// Gets all the user's rune pages, including default ones.
        /// </summary>
        public static LolPerksPerkPageResource[] GetPages()
            => MakeRequest<List<LolPerksPerkPageResource>>("/lol-perks/v1/pages", Method.GET).ToArray();

        /// <summary>
        /// Gets a rune page by ID.
        /// </summary>
        /// <param name="id">The page's ID.</param>
        public static LolPerksPerkPageResource GetPage(int id)
            => MakeRequest<LolPerksPerkPageResource>("/lol-perks/v1/pages/" + id, Method.GET);

        /// <summary>
        /// Updates a rune page.
        /// </summary>
        /// <param name="id">The page's ID.</param>
        /// <param name="page">The new page.</param>
        public static LolPerksPerkPageResource PutPage(int id, LolPerksPerkPageResource page)
            => MakeRequest<LolPerksPerkPageResource>("/lol-perks/v1/pages/" + id, Method.PUT, page);

        /// <summary>
        /// Creates a new rune page.
        /// </summary>
        /// <param name="page">The new page.</param>
        public static LolPerksPerkPageResource PostPage(LolPerksPerkPageResource page)
            => MakeRequest<LolPerksPerkPageResource>("/lol-perks/v1/pages", Method.POST, page);

        /// <summary>
        /// Gets a list of all the runes in LoL.
        /// </summary>
        public static LolPerksPerkUIPerk[] GetPerks()
            => Cache(() => MakeRequest<List<LolPerksPerkUIPerk>>("/lol-perks/v1/perks", Method.GET)).ToArray();

        /// <summary>
        /// Gets a rune's icon.
        /// </summary>
        /// <param name="perk">The rune. See <see cref="GetPerks"/>.</param>
        public static Image GetPerkImage(LolPerksPerkUIPerk perk) => Cache(() =>
        {
            string[] split = perk.iconPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string plugin = split[0];
            string path = string.Join("/", split.Skip(2));

            byte[] b = PluginManager.GetAsset(plugin, path);

            using (var mem = new MemoryStream(b))
            {
                return Image.FromStream(mem);
            }
        });

        public static LolPerksPlayerInventory GetInventory()
            => MakeRequest<LolPerksPlayerInventory>("/lol-perks/v1/inventory", Method.GET);
    }
}
