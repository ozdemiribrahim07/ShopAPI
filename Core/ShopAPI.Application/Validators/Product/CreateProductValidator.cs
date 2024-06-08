using FluentValidation;
using ShopAPI.Application.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Validators.Product
{
    public class CreateProductValidator :AbstractValidator<ProductCreateVM>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().NotNull().WithMessage("Ürün adı bos olamaz").MaximumLength(300).MinimumLength(2).WithMessage("Urun adı 2 ile 300 karakter arasında olmalıdır");
            RuleFor(x => x.ProductPrice).NotEmpty().NotNull().WithMessage("Ürün fiyatı bos olamaz").Must(x => x >= 0).WithMessage("Urun fiyatı 0'dan büyük olmalıdır");
            RuleFor(x => x.ProductStock).NotEmpty().NotNull().WithMessage("Ürün stok bos olamaz").Must(x => x >= 0).WithMessage("Urun stok adı 0'dan büyük olmalıdır");
        }

    }
}
