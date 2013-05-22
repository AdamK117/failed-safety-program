using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public sealed class Repository<TContent> : 
        IRepository<TContent>
    {
        private readonly IServiceMultiItem<TContent> contentService;
        private readonly IList<TContent> cachedContent = new List<TContent>();

        public Repository(IServiceMultiItem<TContent> contentService)
        {
            if (contentService == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                this.contentService = contentService;
            }
        }

        public IEnumerable<TContent> Get(Func<TContent, bool> filter, Action<TContent> callback)
        {
            //Filter the cache, return the filtered entries
            var filteredResults = new List<TContent>();
            foreach (TContent entry in GetAll())
            {
                if (filter(entry))
                {
                    callback(entry);
                    filteredResults.Add(entry);
                }
            }

            return filteredResults;
        }

        public IEnumerable<TContent> GetAll(Action<TContent> callback)
        {
            //Get all entries in the repositories. Two scenarios.
            //  -The entries haven't been loaded yet. Load them into a cache and return that.
            //  -The entries are already loaded (cached). Return the cache.

            if (cachedContent.Count == 0)
            {
                var loadedContent = contentService.Load(callback);

                foreach (TContent loadedEntry in loadedContent)
                {
                    cachedContent.Add(loadedEntry);
                }

                return cachedContent;
            }
            else
            {
                foreach (TContent cachedEntry in cachedContent)
                {
                    callback(cachedEntry);
                }
                return cachedContent;
            }            
        }

        public IEnumerable<TContent> GetAll()
        {
            return GetAll((content) => { });
        }
    }
}
