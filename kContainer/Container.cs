using System;
using System.Collections.Generic;
using System.Linq;

namespace kContainer
{
    public class Container<T>
    {
        private readonly string TYPE_DOES_NOT_IMPLEMENT = "{0} does not implement {1}";
        private readonly string NICKNAME_NOT_RESGISTERED = "{0} has not been registered";
        // private Dictionary<string,Dictionary<Type,IKRepo>> _map;
        private readonly List<KMapping<T>> regists;
        internal Container()
        {
            regists = new List<KMapping<T>>();        
        }
       // private Dictionary<string, KMapping<T>> _map;

        public void Register(string nickName, Type repoType, T repo)
        {

            Type validType = typeof(T);

            if (!repoType.IsAssignableFrom(validType))
            {
                throw new Exception(string.Format(TYPE_DOES_NOT_IMPLEMENT, repoType.Name, validType.Name));
            }

            //  _map.Values

            KMapping<T> found = regists.FirstOrDefault(t => t.Name == nickName && t.RepoType == repoType);

            if (found == null)
            {
                regists.Add(new KMapping<T> { Name = nickName, RepoType = repoType, Repo = repo });
                return;
            }

            int index = regists.IndexOf(found);

            regists.RemoveAt(index);

            regists.Add(new KMapping<T> { Name = nickName, RepoType = repoType, Repo = repo });
            /*
           if(_map.ContainsKey(nickName) )
            {
                if (_map[nickName].ContainsKey(repoType))
                {
                    _map[nickName][repoType] = repo;
                    return;
                }

                _map[nickName].Add(repoType, repo);
                return;
            }

            _map.Add(nickName, repo);
            */
        }


        public T Get(string nickName)
        {
            KMapping<T> result = regists.FirstOrDefault(r => r.Name == nickName);// this._map[nickName];

            if (result == null)
                throw new Exception(string.Format(NICKNAME_NOT_RESGISTERED, nickName));


            return result.Repo;
        }
    }

    internal class KMapping<T>
    {
        public string Name { get; internal set; }
        public Type RepoType { get; internal set; }
        public T Repo { get; internal set; }
    }
}
