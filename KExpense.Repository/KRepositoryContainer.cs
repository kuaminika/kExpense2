using KExpense.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KExpense.Repository
{
    public class KRepositoryContainer
    {
        private readonly string  TYPE_DOES_NOT_IMPLEMENT = "{0} does not implement {1}";
        private readonly string NICKNAME_NOT_RESGISTERED = "{0} has not been registered";
        // private Dictionary<string,Dictionary<Type,IKRepo>> _map;
        private List<KRepoMapping> regists;

        private Dictionary<string, KRepoMapping> _map;

        public void Register(string nickName, Type repoType,IKRepo repo)
        {
            
            Type validType = typeof(IKRepo);

           if ( !repoType.IsAssignableFrom(validType))
            {
                throw new Exception(string.Format(TYPE_DOES_NOT_IMPLEMENT,repoType.Name,validType.Name));
            }

         //  _map.Values
         
            KRepoMapping found = regists.FirstOrDefault(t => t.Name == nickName && t.RepoType == repoType);
            
            if(found== null)
            {
                regists.Add(new KRepoMapping { Name = nickName, RepoType = repoType, Repo = repo });
                return;
            }

             int index = regists.IndexOf(found);

            regists.RemoveAt(index);

            regists.Add(new KRepoMapping { Name = nickName, RepoType = repoType, Repo = repo });
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


        public IKRepo Get(string nickName)
        {
            KRepoMapping result = regists.FirstOrDefault(r=>r.Name == nickName);// this._map[nickName];

            if (result == null)
                throw new Exception(string.Format(NICKNAME_NOT_RESGISTERED,nickName));


            return result.Repo;
        }

        private class KRepoMapping
        {
            public string Name { get; set; }
            public IKRepo Repo { get; set; }

            public Type RepoType { get; set; }
        }
    }
}
