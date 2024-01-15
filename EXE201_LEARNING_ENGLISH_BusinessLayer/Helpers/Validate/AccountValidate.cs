using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers.Validate
{
    public class AccountValidate
    {
        public string CheckValidate<TEntity>(TEntity entity)
        {
            var getNames = entity.GetType().GetProperties();

            foreach(var name in getNames)
            {
                var getName = name.Name;
                if (getName.Equals("Email"))
                {
                    if(entity.GetType() == typeof(TEntity))
                    {


                    }
                }
            }

            return "";
        }
    }
}
