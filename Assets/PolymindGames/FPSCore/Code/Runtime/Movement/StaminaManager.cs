using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine;
using System;
using PolymindGames.SaveSystem;

// Add this line for NetworkPlayerState
using PolymindGames.Networking;

namespace PolymindGames
{
    [RequireCharacterComponent(typeof(IMovementControllerCC))]
    public sealed class StaminaManager : CharacterBehaviour, IStaminaManagerCC, ISaveableComponent
    {
        // Rest of the file stays exactly the same