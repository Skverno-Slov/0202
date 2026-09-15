using LabWork8.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork8.halpers
{
    public class PriceCalculator
    {
        double _vat;
        double _discount;
        double _minDiscountPrice;
        double _discountPercent;
        double _total;

        public PriceCalculator(double vat,
            double discount,
            double minDiscountPrice,
            double discountPercent,
            double total)
        {
            _vat = vat;
            _discount = discount;
            _minDiscountPrice = minDiscountPrice;
            _discountPercent = discountPercent;
            _total = total;
        }

        public double CalculatePrice() 
            => _total - _discount + (_total * _vat);

        public double CalculateDiscount() 
            => _total > _minDiscountPrice
                ? _total * _discountPercent : 0;
    }
}
