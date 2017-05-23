using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.BLL
{
    public class StTypeBLL
    {
        private StTypeRepository st;
        public bool EditStType(StType sttype)
        {
            st = new StTypeRepository();
            var _str = st.StType(sttype.StTypeId);
            if (_str == null)
            {
                return false;
            }
            else
            {
                _str.Name = sttype.Name;

                if (st.Update(_str))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
