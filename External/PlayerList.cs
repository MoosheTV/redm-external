using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public sealed class PlayerList : IEnumerable<Player>
    {
        private const int MaxPlayers = 256;

        protected internal PlayerList()
        {

        }

        public IEnumerator<Player> GetEnumerator()
        {
            for (var i = 0; i < MaxPlayers; i++)
            {
                if (Function.Call<bool>(Hash.NETWORK_IS_PLAYER_CONNECTED, i))
                {
                    yield return new Player(i);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Player this[int netId] => this.FirstOrDefault(player => player.ServerId == netId);
    }
}
