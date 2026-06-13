using Cysharp.Threading.Tasks;

namespace Neonalig.GameEvents.UniTask
{
    public static class GameEventSystemTasks
    {
        /// <summary>
        /// Asynchronously wait for a specific event to be sent before continuing execution.
        /// </summary>
        /// <typeparam name="T"> The type of event to wait for. Must implement <see cref="GameEvent"/>. </typeparam>
        /// <param name="condition"> Optional condition to evaluate on received events. If provided, the wait will only end when an event satisfying the condition is received. </param>
        /// <returns> A UniTask that completes when the specified event is received (and satisfies the condition, if provided). </returns>
        public static async UniTask<T> WaitForEventAsync<T>(EventCondition<T>? condition = null) where T : GameEvent
        {
            var waiter = new WaitForEvent<T>(condition);
            await waiter;
            return waiter.Event!;
        }

        /// <summary>
        /// Asynchronously wait for a specific event to be sent before continuing execution, but if the event has already been fired, it will return immediately with the most recent instance of that event.
        /// </summary>
        /// <typeparam name="T"> The type of event to wait for. Must implement <see cref="GameEvent"/>. </typeparam>
        /// <returns> A UniTask that completes when the specified event is received (or immediately if it has already been published at least once). The result is the instance of the event that was received or the most recent instance if it was already published. </returns>
        public static async UniTask<T> WaitForEventIfNotYetFiredAsync<T>()
            where T : GameEvent
        {
            var waiter = new WaitForEventIfNotYetPublished<T>();
            await waiter;
            return waiter.Event!;
        }
    }
}