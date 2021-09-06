using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace YMG.Models.MyValidation
{
    public class DateOfBirthValidator : ValidationAttribute
    {
        public bool isLeapYear(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            {
                return true;
            }
            else return false;
        }

        public ValidationResult dateIsValid(int year, int month, int day)
        {
            string errMessage = "Please enter a valid date of birth";

            int currentYear = DateTime.Now.Year;

            // the oldest person recorded lived for 126 years so i will assume the maximum age
            // to be 130.
            // This application is intended for users aged 12 and up, so the minimumm age is 12.

            int minYearAllowed = currentYear - 130;
            int maxYearAllowed = currentYear - 12;
            if (year > maxYearAllowed)
            {
                return new ValidationResult("You must be at least 12 years old to create an account.");
            }
            else
            {
                if (year < minYearAllowed)
                {
                    return new ValidationResult("Please enter a valid year(" + minYearAllowed.ToString() + " or higher");
                }
            }

            // validation for the month

            if(month < 1 || month > 12)
            {
                return new ValidationResult(errMessage);
            }

            // validation for the day

            // validation for February

            if (month == 2)
            {
                // if the year is leap year then Feb 29th is valid, else no

                if (isLeapYear(year))
                {
                    if(day < 1 || day > 29)
                    {
                        return new ValidationResult(errMessage);
                    }
                }

                else
                {
                    if (day < 1 || day > 28)
                    {
                        return new ValidationResult(errMessage);
                    }
                }
            }
            else
            {
                // validation for months with 30 days (April, June, September, November)

                if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    if (day < 1 || day > 30)
                    {
                        return new ValidationResult(errMessage);
                    }
                }
                else
                {
                    // validation for 31-day months (Jan, Mar, May, Jul, Aug, Oct, Dec)

                    if (day < 1 || day > 31)
                    {
                        return new ValidationResult(errMessage);
                    }
                }
            }

            return ValidationResult.Success;

            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                RegisterViewModel user = (RegisterViewModel)validationContext.ObjectInstance;
                return dateIsValid(user.Year, user.Month, user.Day);
            }
            catch
            {
                EditViewModel user = (EditViewModel)validationContext.ObjectInstance;
                return dateIsValid(user.Year, user.Month, user.Day);
            }
        }
    }
}