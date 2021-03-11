using Dust.MinCore.Models.Dtos.Input;
using FluentValidation;

namespace Dust.MinCore.Models.Validators
{
    public class LoginInputValidator : AbstractValidator<LoginInput>
    {
        public LoginInputValidator()
        {
            RuleFor(x => x.LoginName).NotEmpty().WithMessage("请填写用户名称");
            RuleFor(x => x.AvatarUrl).NotEmpty().WithMessage("请填写头像url");
            RuleFor(x => x.Id).NotEmpty().WithMessage("用户编号必须传递");
        }
    }
}
