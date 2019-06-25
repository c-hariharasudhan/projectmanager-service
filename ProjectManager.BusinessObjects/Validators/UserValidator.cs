using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessObjects.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.FirstName).NotEmpty().Length(5, 100);
            RuleFor(x => x.LastName).NotEmpty().Length(5, 100);
            RuleFor(x => x.EmployeeId).Length(0, 10);
        }
    }
}
