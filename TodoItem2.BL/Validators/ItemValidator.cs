using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoItem2.BL.Dtos;

namespace ToDoItem2.BL.Validators
{
    public class ItemValidator : AbstractValidator<ItemDto>
    {
        public ItemValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("This field is required");
            RuleFor(item => item.Description)
                .MaximumLength(50).WithMessage("This field just accept 50 characters as maximun")
                .NotEmpty().WithMessage("This field is required");
        }
    }
}
