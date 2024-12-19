﻿using FluentValidation;
using Microservices.Web.Models.Catalog.Course;

namespace Microservices.Web.Validators
{
    public class CreateCourseInputValidator:AbstractValidator<CreateCourseInput>
    {
        public CreateCourseInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş bırakılamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş bırakılamaz.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat boş bırakılamaz.").ScalePrecision(2,6).WithMessage("Hatalı format");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre boş bırakılamaz");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Lütfen kategori seçiniz.");
        }
    }
}
