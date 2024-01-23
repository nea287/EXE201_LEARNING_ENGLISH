using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers.Validate
{
    public class BasicValidate
    {
        public virtual bool CheckNumberValidate(object entity) => (decimal)entity > 0;
        public virtual bool CheckListNumberValidate(params decimal[] nums) => nums.Where(x => x <= 0).Any();
         
    }
}
