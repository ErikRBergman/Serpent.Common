namespace Serpent.ServiceFabric.Actors.Common.StateManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.ServiceFabric.Actors.Runtime;
    using Microsoft.ServiceFabric.Data;

    public class PrefixedStateManager : IActorStateManager
    {
        private readonly IActorStateManager innerActorStateManager;

        private readonly string prefix;

        public PrefixedStateManager(IActorStateManager innerActorStateManager, string prefix)
        {
            this.innerActorStateManager = innerActorStateManager;
            this.prefix = prefix + "_";
        }

        public Task<T> AddOrUpdateStateAsync<T>(string stateName, T addValue, Func<string, T, T> updateValueFactory, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.AddOrUpdateStateAsync(this.GetPrefixedName(stateName), addValue, updateValueFactory, cancellationToken);
        }

        public Task AddStateAsync<T>(string stateName, T value, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.AddStateAsync(this.GetPrefixedName(stateName), value, cancellationToken);
        }

        public Task ClearCacheAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.ClearCacheAsync(cancellationToken);
        }

        public Task<bool> ContainsStateAsync(string stateName, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.ContainsStateAsync(this.GetPrefixedName(stateName), cancellationToken);
        }

        public Task<T> GetOrAddStateAsync<T>(string stateName, T value, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.GetOrAddStateAsync(this.GetPrefixedName(stateName), value, cancellationToken);
        }

        public Task<T> GetStateAsync<T>(string stateName, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.GetStateAsync<T>(this.GetPrefixedName(stateName), cancellationToken);
        }

        public async Task<IEnumerable<string>> GetStateNamesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var names = await this.innerActorStateManager.GetStateNamesAsync(cancellationToken);
            return names.Where(name => name.StartsWith(this.prefix, StringComparison.Ordinal));
        }

        public Task RemoveStateAsync(string stateName, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.RemoveStateAsync(this.GetPrefixedName(stateName), cancellationToken);
        }

        public Task SaveStateAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.SaveStateAsync(cancellationToken);
        }

        public Task SetStateAsync<T>(string stateName, T value, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.SetStateAsync(this.GetPrefixedName(stateName), value, cancellationToken);
        }

        public Task<bool> TryAddStateAsync<T>(string stateName, T value, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.TryAddStateAsync(this.GetPrefixedName(stateName), value, cancellationToken);
        }

        public Task<ConditionalValue<T>> TryGetStateAsync<T>(string stateName, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.TryGetStateAsync<T>(this.GetPrefixedName(stateName), cancellationToken);
        }

        public Task<bool> TryRemoveStateAsync(string stateName, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.innerActorStateManager.TryRemoveStateAsync(this.GetPrefixedName(stateName), cancellationToken);
        }

        private string GetPrefixedName(string stateName)
        {
            return this.prefix + stateName;
        }
    }
}