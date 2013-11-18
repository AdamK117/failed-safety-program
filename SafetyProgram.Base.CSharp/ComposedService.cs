using System;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines a service that is composed of its individual parts.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComposedService<T> : IService<T>
    {
        public ComposedService(
            Func<IService<T>, Func<T>> newTFunc,
            Func<IService<T>, Func<bool>> canNewFunc,
            Func<IService<T>, Func<T>> loadFunc,
            Func<IService<T>, Func<bool>> canLoadFunc,
            Func<IService<T>, Action<T>> saveFunc,
            Func<IService<T>, Func<T, bool>> canSaveFunc,
            Func<IService<T>, Action<T>> saveAsFunc,
            Func<IService<T>, Func<T, bool>> canSaveAsFunc,
            Func<IService<T>, Action> closeFunc
            )
        {
            this.newFunc = newTFunc(this);
            this.canNewFunc = canNewFunc(this);
            this.loadFunc = loadFunc(this);
            this.canLoadFunc = canLoadFunc(this);
            this.saveFunc = saveFunc(this);
            this.canSaveFunc = canSaveFunc(this);
            this.saveAsFunc = saveAsFunc(this);
            this.canSaveAsFunc = canSaveAsFunc(this);
            this.closeFunc = closeFunc(this);
        }

        private readonly Func<T> newFunc;
        public T New()
        {
            return newFunc();
        }

        private Func<bool> canNewFunc;
        public bool CanNew()
        {
            return canNewFunc();
        }

        private readonly Func<T> loadFunc;
        public T Load()
        {
            return loadFunc();
        }

        private readonly Func<bool> canLoadFunc;
        public bool CanLoad()
        {
            return canLoadFunc();
        }

        private readonly Action<T> saveFunc;
        public void Save(T data)
        {
            saveFunc(data);
        }

        private readonly Func<T, bool> canSaveFunc;
        public bool CanSave(T data)
        {
            return canSaveFunc(data);
        }

        private readonly Action<T> saveAsFunc;
        public void SaveAs(T data)
        {
            saveAsFunc(data);
        }

        private readonly Func<T, bool> canSaveAsFunc;
        public bool CanSaveAs(T data)
        {
            return canSaveAsFunc(data);
        }

        private readonly Action closeFunc;
        public void Disconnect()
        {
            closeFunc();
        }
    }
}
