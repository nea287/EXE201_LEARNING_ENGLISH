using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XAct;
using static QRCoder.PayloadGenerator;

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
                var getValue = entity.GetType().GetProperty(getName).GetValue(entity, null);

                if (getValue.IsNull()) continue;

                if (getName.Equals("Email"))
                {
                    string value = (string)getValue;

                    if (!Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                    {
                        return Constraints.EMAIL_INVALIDATE;
                    }

                }
                else if (getName.Equals("Birthdate"))
                {
                    DateTime value = (DateTime)getValue;

                    if (DateTime.Today.Year - value.Year < 18)
                    {
                        return Constraints.BIRTHDATE_INVALIDATE;
                    }

                }
                else if (getName.Equals("PhoneNumber"))
                {
                    string value = (string)getValue;

                    if (Regex.IsMatch(value, @"[^0-9]")
                        && Regex.IsMatch(value, @"^\d{10}$"))
                    {
                        return Constraints.NUMBER_PHONE_INVALIDATE;
                    }
                }
                else if (getName.Equals("Password"))
                {
                    string value = (string)getValue;

                    if (!Regex.IsMatch(value, @"^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9]).{6,}$"))
                    {
                        return Constraints.PASSWORD_INVALIDATE;
                    }
                }
            }

            return Constraints.VALIDATE;
        }
    }
}
