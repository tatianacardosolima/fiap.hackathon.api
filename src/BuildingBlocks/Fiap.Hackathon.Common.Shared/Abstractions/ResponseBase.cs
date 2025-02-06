using Fiap.Hackathon.Common.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Hackathon.Common.Shared.Abstractions
{
    public abstract class ResponseBase<T> where T: EntityBase
    {
        protected ResponseBase()
        {            
        }
        protected ResponseBase(EntityBase entity)
        {            
        }

        public abstract ResponseBase<T> GetResponse(T entity);
        
    }
}
