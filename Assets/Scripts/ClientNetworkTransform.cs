using Unity.Netcode.Components;
using UnityEngine;

namespace Unity.Multiplayer.Sameples.Utilities.ClientAuthority
{
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }

}
