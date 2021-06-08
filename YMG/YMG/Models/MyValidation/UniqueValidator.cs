using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YMG.Models.MyValidation
{
    public class UniqueValidator : ValidationAttribute
    {
        private bool UniqueForId(string name, int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            if(ctx.Actors.Where(a=>a.FullName == name).Count() == 1)
            {
                if(ctx.Actors.Where(a => a.FullName == name).FirstOrDefault().ActorId == id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(ctx.Actors.Where(a => a.FullName == name).Count() == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private ValidationResult ValidateName(string name, int id)
        {
            if (!UniqueForId(name, id))
                return new ValidationResult("Actor Name must be unique!");
            return ValidationResult.Success;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
                Actor actor = (Actor)validationContext.ObjectInstance;
                return ValidateName(actor.FullName, actor.ActorId);
        }
    }
}