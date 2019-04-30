namespace Serpent.Common.Xml.UtilityClasses
{
    using System;

    internal abstract class ImmutableThing<TReadOnlyType>
    {
        private readonly object lockObject = new object();

        public ImmutableThing(TReadOnlyType data = default(TReadOnlyType))
        {
            this.Value = data;
        }

        public TReadOnlyType Value { get; private set; }

        protected void Replace(TReadOnlyType item)
        {
            lock (this.lockObject)
            {
                this.Value = item;
            }
        }

        /// <summary>
        ///     Replace the inner object. If the updateFunc updates the existing object, thread safety is no longer maintained
        /// </summary>
        /// <param name="updateFunc"></param>
        public void Update(Func<TReadOnlyType, TReadOnlyType> updateFunc)
        {
            lock (this.lockObject)
            {
                this.Value = updateFunc(this.Value);
            }
        }

        /// <summary>
        ///     Replace the inner object. If the updateFunc updates the existing object, thread safety is no longer maintained
        /// </summary>
        /// <param name="parameter1"></param>
        /// <param name="updateFunc"></param>
        public void Update<TParameter1>(TParameter1 parameter1, Func<TReadOnlyType, TParameter1, TReadOnlyType> updateFunc)
        {
            lock (this.lockObject)
            {
                this.Value = updateFunc(this.Value, parameter1);
            }
        }

        /// <summary>
        ///     Replace the inner object. If the updateFunc updates the existing object, thread safety is no longer maintained
        /// </summary>
        /// <param name="updateFunc"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        public void Update<TParameter1, TParameter2>(TParameter1 parameter1, TParameter2 parameter2, Func<TReadOnlyType, TParameter1, TParameter2, TReadOnlyType> updateFunc)
        {
            lock (this.lockObject)
            {
                this.Value = updateFunc(this.Value, parameter1, parameter2);
            }
        }
    }
}